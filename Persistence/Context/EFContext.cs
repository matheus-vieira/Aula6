using Model.Registers;
using Model.Tables;
using System.Data.Entity;

namespace Persistence.Contexts
{
    public class EFContext : DbContext
    {

        #region [ DbSet's ]

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion [ DbSet's ]

        #region [ Constructor ]

        public EFContext()
            : base("Asp_Net_MVC_CS")
        {
            var dbInit = new DropCreateDatabaseIfModelChanges<EFContext>();
            Database.SetInitializer<EFContext>(dbInit);
        }

        #endregion [ Constructor ]

    }
}