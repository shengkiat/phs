﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PHS.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<MasterAddress> MasterAddresses { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PHSEvent> PHSEvents { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TemplateField> TemplateFields { get; set; }
        public virtual DbSet<TemplateFieldValue> TemplateFieldValues { get; set; }
        public virtual DbSet<ReferenceRange> ReferenceRanges { get; set; }
        public virtual DbSet<StandardReference> StandardReferences { get; set; }
    }
}
