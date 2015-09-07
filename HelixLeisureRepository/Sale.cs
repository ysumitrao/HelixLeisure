namespace HelixLeisureRepository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;



    public class Sale
    {
        [Key]
        public string id { get; set; }
        public DateTime DateOfSale { get; set; }
        public double timestamp { get; set; }
        public string location_name { get; set; }
        public string sales_person_name { get; set; }
        public List<Products> products { get; set; }
        public double total_sale_amount { get; set; }
        public string currency { get; set; }
        public string sale_invoice_number { get; set; }
    }

    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string SaleId { get; set; }        
        public string name { get; set; }
        public int quantity { get; set; }
        public double sale_amount { get; set; }
    }

    /*
      {
 "id": "(string) unique id of the event",
 "timestamp": "(timestamp) utc timestamp of the event",
 "location_name": "(string) name of the location (e.g. Helix Perth)",
 "sales_person_name": "(string) name of the sales person",
 "products": [
 {
 "name": "(string) name of the product",
 "quantity": "(integer) quantity of the product",
 "sale_amount": "(double) total sale amount"
 }
 ],
 "total_sale_amount": "(double) the total sale amount",
 "currency": "(string) currency code of the sale",
 "sale_invoice_number": "(string) unique sale invoice number",
 }
     */
}