
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

        public virtual DbSet<form> forms { get; set; }
        public virtual DbSet<form_field_values> form_field_values { get; set; }
        public virtual DbSet<form_fields> form_fields { get; set; }
        public virtual DbSet<MasterAddress> MasterAddresses { get; set; }
        public virtual DbSet<@event> events { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
    }
}
