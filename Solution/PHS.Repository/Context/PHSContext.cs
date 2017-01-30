
using System.Data.Entity;
using PHS.DB;

namespace PHS.Repository.Context
{
    public class PHSContext : DbContext
    {
        public PHSContext()
            : base("name=Entities") // name of connection string
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<form> forms { get; set; }
        public virtual DbSet<form_field_values> form_field_values { get; set; }
        public virtual DbSet<form_fields> form_fields { get; set; }
    }
}
