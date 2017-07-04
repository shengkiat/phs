
using System.Data.Entity;
using PHS.DB;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;
using PHS.Repository.Interface.Core;
using System;
using System.Data.Common;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using PHS.DB.Attributes;

namespace PHS.Repository.Context
{
    public class PHSContext : DbContext, IContext
    {
        private Person person;
        private List<AuditLog> result = new List<AuditLog>();
        private List<DbEntityEntry> addEntries = new List<DbEntityEntry>();

        public PHSContext()
            : base("name=Entities") // name of connection string
        {
            Configuration.LazyLoadingEnabled = false;

            IsAuditEnabled = true;

           // ObjectContext.SavingChanges += OnSavingChanges;
        }

        public PHSContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;

            IsAuditEnabled = true;

            // ObjectContext.SavingChanges += OnSavingChanges;
        }

        public PHSContext(Person person) : this()
        {
            this.person = person;
        }

        public ObjectContext ObjectContext
        {
            get
            {
                return (this as IObjectContextAdapter).ObjectContext;
            }
        }

        public virtual DbSet<EventPatient> EventPatients { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TemplateField> TemplateFields { get; set; }
        public virtual DbSet<TemplateFieldValue> TemplateFieldValues { get; set; }
        public virtual DbSet<MasterAddress> MasterAddresses { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PHSEvent> PHSEvents { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }



        #region IContext Implementation

        public bool IsAuditEnabled
        {
            get;
            set;
        }

        public IDbSet<TEntity> GetEntitySet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void ChangeState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            Entry<TEntity>(entity).State = state;
        }

        public DbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            if (this.ChangeTracker.Entries().Any(IsChanged))
            {
                return this.SaveChanges();
            }
            return 0;
        }

        private static bool IsChanged(DbEntityEntry entity)
        {
            return IsStateEqual(entity, EntityState.Added) ||
                   IsStateEqual(entity, EntityState.Deleted) ||
                   IsStateEqual(entity, EntityState.Modified);
        }

        private static bool IsStateEqual(DbEntityEntry entity, EntityState state)
        {
            return (entity.State & state) == state;
        }

        #endregion

        public enum AuditState
        {
            Added,
            Modified,
            Deleted
        }

        #region Audit Imaplementation

        public override int SaveChanges()
        {
            OnSavingChanges(null, null);

            int rowsAffected =  base.SaveChanges();

            FinalizeAudits();

            base.SaveChanges();

            return rowsAffected;
        }

        private void FinalizeAudits()
        {
            if (addEntries == null && this.result == null)
            {
                return;
            }

            foreach (var audit in this.result)
            {
                if (audit.RecordID.Equals(""))
                {
                    foreach (var entry in this.addEntries)
                    {
                        foreach (string propertyName in entry.CurrentValues.PropertyNames)
                        {
                            if (entry.CurrentValues.GetValue<object>(propertyName) == null)
                            {
                                continue;
                            }

                            if (entry.CurrentValues.GetValue<object>(propertyName).ToString().Equals(audit.OriginalValue))
                            {
                                audit.RecordID = GetPrimaryKeyValue(entry).ToString();

                                continue;
                            }
                        }
                    }

                }
            }
        }

        void OnSavingChanges(object sender, EventArgs e)
        {
            if (IsAuditEnabled && person != null)
            {


                 var changeEntries = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added
                    || p.State == EntityState.Deleted
                    || p.State == EntityState.Modified);

                if (null != changeEntries)
                {
                    foreach (var entity in changeEntries)
                    {
                        this.addEntries.Add(entity);

                        foreach (var audit in CreateAuditRecordsForChanges(entity))
                        {
                            this.AuditLogs.Add(audit);
                        }
                    }
                }
            }
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            try
            {
                var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);

                if (objectStateEntry == null)
                {

                    return "";
                }

                return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
            }
            catch
            {
                return "";
            }
        }

        private List<AuditLog> CreateAuditRecordsForChanges(DbEntityEntry dbEntry)
        {
            Type entityType = dbEntry.Entity.GetType();

            if (!EntityTrackingConfiguration.IsTrackingEnabled(entityType))
            {
                 return new List<AuditLog>();
            }

            

            #region Generate Audit
            //determine audit time
            DateTime auditTime = DateTime.Now;

            // Get the Table name by attribute
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            // Find Primiray key.
            var keyValue = GetPrimaryKeyValue(dbEntry);
            // string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

            if (dbEntry.State == EntityState.Added)
            {
                //result.Add(new AuditLog()
                //{
                //    AuditDateTime = auditTime,
                //    AuditState = AuditState.Added.ToString(),
                //    TableName = tableName,
                //    RecordID = keyValue.ToString(),  // Again, adjust this if you have a multi-column key
                //    NewValue = (dbEntry.CurrentValues.ToObject().ToString())
                //}
                //    );

                
                    var columnsToLog = FieldsToLog(entityType);

                    foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                    {
                        if (!columnsToLog.Contains(propertyName))
                        {
                            continue;

                        }

                    if (dbEntry.CurrentValues.GetValue<object>(propertyName) == null)
                    {
                        continue;
                    }
                            result.Add(new AuditLog()
                            {
                                PersonID = person.PersonID,
                                AuditDateTime = auditTime,
                                AuditState = AuditState.Added.ToString(),
                                TableName = tableName,
                                RecordID = keyValue.ToString(),
                                ColumnName = propertyName,
                                OriginalValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ?
                                null
                                : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString(),

                                NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ?
                                null
                                : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                            }
                           );
                        
                    }
                




            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                result.Add(new AuditLog()
                {
                    AuditDateTime = auditTime,
                    AuditState = AuditState.Deleted.ToString(),
                    TableName = tableName,
                    RecordID = keyValue.ToString(),
                    NewValue = (dbEntry.OriginalValues.ToObject().ToString())
                }
                    );
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                var columnsToLog = FieldsToLog(entityType);

                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    if (!columnsToLog.Contains(propertyName))
                    {
                        continue;

                    }

                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        result.Add(new AuditLog()
                        {
                            PersonID = person.PersonID,
                            AuditDateTime = auditTime,
                            AuditState = AuditState.Modified.ToString(),
                            TableName = tableName,
                            RecordID = keyValue.ToString(),
                            ColumnName = propertyName,
                            OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ?
                            null
                            : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),

                            NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ?
                            null
                            : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                        }
                       );
                    }
                }
            }
            return result;

            #endregion
        }

        #endregion

        internal static bool PropertyConfigValueFactory(string propertyName,
           Type entityType)
        {
            SkipTrackingAttribute skipTrackingAttribute =
                entityType.GetProperty(propertyName)
                    .GetCustomAttributes(false)
                    .OfType<SkipTrackingAttribute>()
                    .SingleOrDefault();

            bool trackValue = skipTrackingAttribute == null;

            return trackValue;
        }


        public IEnumerable<string> FieldsToLog(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException();
            }

            var auditPropertyInfo = new List<string>();

            foreach (var property in entityType.GetProperties())
            {
               var s =  PropertyConfigValueFactory(property.Name, entityType);



               var v =  property.GetCustomAttributes(true);

                if (property.GetCustomAttributes(typeof(SkipTrackingAttribute ), false).FirstOrDefault() == null)
                {
                    auditPropertyInfo.Add(property.Name);
                }

            }

            return auditPropertyInfo;

        }
    }
}
