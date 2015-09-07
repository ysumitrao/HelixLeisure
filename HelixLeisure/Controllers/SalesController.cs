using HelixLeisure.Filters;
using HelixLeisureDataAccess.Interface;
using HelixLeisureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelixLeisure.Controllers
{
    [CustomExceptions]
    public class SalesController : ApiController
    {
        private readonly ISalesDA _salesDA;

        public SalesController(ISalesDA salesDA)
        {
            _salesDA = salesDA;
        }

        // GET: api/sales
        public List<Sale> Get()
        {
            return _salesDA.GetSales();
        }

        [Route("api/Sales/GetProducts")]
        [HttpGet]
        public List<Products> GetProducts(string saleId)
        {
            return _salesDA.GetProductsFromSaleId(saleId);
        }

        public HttpResponseMessage Post([FromBody]Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _salesDA.SaveSale(sale);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
