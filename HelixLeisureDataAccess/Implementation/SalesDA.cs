using HelixLeisureDataAccess.Interface;
using HelixLeisureDataAccess.Model;
using HelixLeisureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixLeisureDataAccess.Implementation
{
    public class SalesDA : ISalesDA, IDisposable
    {
        private readonly HelixLeisureModel repository;
        public SalesDA()
        {
            repository = new HelixLeisureModel();
        }

        public List<Sale> GetSales()
        {
            return repository.MySales
                .Include("Products").ToList();
        }

        public void SaveSale(Sale objSale)
        {
            if (objSale.products!=null)
            {
                foreach (Products product in objSale.products)
                {
                    product.SaleId = objSale.id;
                }
            }

            if (objSale.timestamp !=null)
            {
                var original = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                objSale.DateOfSale = original.AddSeconds(objSale.timestamp);                
            }

            repository.MySales.Add(objSale);
            repository.SaveChanges();
        }

        public List<Products> GetProductsFromSaleId(string saleId)
        {
            return repository.MyProducts
                .Where(products => products.SaleId == saleId)
                .ToList();
        }

        public void Dispose()
        {
            if(repository!=null)
            {
                repository.Dispose();
            }
        }


        
    }
}
