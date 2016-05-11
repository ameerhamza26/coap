using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class Products
    {
        public int? ID { get; set; }
        public string NAME { get; set; }

        public List<Products> GetProducts()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var Products = db.PRODUCTS.Select(c => new Products { ID = c.ID, NAME = c.NAME }).ToList();
                return Products;
            }
        }

        public override string ToString()
        {
            return NAME;
        }
    }
}
