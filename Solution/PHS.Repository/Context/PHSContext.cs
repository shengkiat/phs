
using System.Data.Entity;
using PHS.DB;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;

namespace PHS.Repository.Context
{
    public class PHSContext : DbContext
    {
        public PHSContext()
            : base("name=Entities") // name of connection string
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<EventPatient> EventPatients { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<FormField> FormFields { get; set; }
        public virtual DbSet<FormFieldValue> FormFieldValues { get; set; }
        public virtual DbSet<MasterAddress> MasterAddresses { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PHSEvent> PHSEvents { get; set; }
    }
}
