using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelixLeisureDataAccess.Implementation;
using HelixLeisureRepository;
using System.Collections.Generic;

namespace HelixLeisureUnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetAllSales()
        {
            SaveSales();
            SalesDA salesDa = new SalesDA();
            Assert.IsTrue(salesDa.GetSales().Count > 0);
        }

        [TestMethod]
        public void GetProductsFromSales()
        {
            string saleId = SaveSales();
            SalesDA salesDa = new SalesDA();
            Assert.IsTrue(salesDa.GetProductsFromSaleId(saleId).Count > 0);
        }

        [TestMethod]
        public void AddSale()
        {
            string saleId = SaveSales();
            SalesDA salesDa = new SalesDA();
            Assert.IsTrue(!String.IsNullOrEmpty(saleId));
        }

        private string SaveSales()
        {
            Sale sale = new Sale();
            Products product = new Products();
            sale.products = new List<Products>();
            try
            {                
                sale.id = Guid.NewGuid().ToString();
                sale.timestamp = 1441611234;
                sale.location_name = "Helix Perth";
                sale.sales_person_name = "Dummy Sales Person";

                product.name = "Test Product";
                product.SaleId = sale.id;
                product.quantity = 10;
                product.sale_amount = 100;
                sale.products.Add(product);

                sale.total_sale_amount = 1000;
                sale.currency = "AUD";
                sale.sale_invoice_number = "INV - 001";
                SalesDA salesDa = new SalesDA();
                salesDa.SaveSale(sale);
            }
            catch(Exception)
            {
                sale.id = "";
            }
            

            return sale.id;
        }
    }
}
