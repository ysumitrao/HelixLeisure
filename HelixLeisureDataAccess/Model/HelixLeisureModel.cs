using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixLeisureRepository;

namespace HelixLeisureDataAccess.Model
{
    class HelixLeisureModel : DbContext
    {
        public HelixLeisureModel()
            : base("name=HelixLeisureDB")
            {
            }

        public virtual DbSet<Sale> MySales { get; set; }
        public virtual DbSet<Products> MyProducts { get; set; }
    }
}
