using HelixLeisureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixLeisureDataAccess.Interface
{
    public interface ISalesDA
    {
        List<Sale> GetSales();
        List<Products> GetProductsFromSaleId(string saleId);
        void SaveSale(Sale objSale);
    }
}
