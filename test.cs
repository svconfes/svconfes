using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webappdev.Models;
using webappdev.entities;

namespace webappdev.Controllers
{
    public class OrderController : ApiController
    {
        WebAppDevDBDataContext webAppDevDBDataContext;
        public OrderController()
        {
            webAppDevDBDataContext = new WebAppDevDBDataContext();
        }
        // GET api/order
        public IEnumerable<Order> Get()
        {
            List<TblOrder> tblOrders = webAppDevDBDataContext.TblOrders.OrderByDescending(x => x._ord).ToList();
            List<Order> lstOrder = new List<Order>();
            foreach (TblOrder tblOrder in tblOrders)
            {
                Order order = new Order();
                order.Ord = tblOrder._ord;
                order.Code = tblOrder._code;
                order.CodeUser = tblOrder._codeUser;
                order.CodeStatus = tblOrder._codeStatus;
                order.Price = (long)tblOrder._price;
                order.Products = getLstProductDetailByCode(order.Code);
                lstOrder.Add(order);
            }
            return lstOrder;
        }

        // GET api/order/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/order
        public void Post([FromBody]Order order)
        {
            TblOrder tblOrder = new TblOrder();
            tblOrder._codeStatus = "working";
            tblOrder._codeUser = order.CodeUser;
            tblOrder._code = "ord" + (webAppDevDBDataContext.TblOrders.OrderBy(c => c._ord).ToList().Last()._ord + 1);
            long price = 0;
            foreach (Product product in order.Products)
            {
                TblOrderDetail tblOderDetail = new TblOrderDetail();
                tblOderDetail._codeOrder = tblOrder._code;
                tblOderDetail._codeProduct = product.Code;
                tblOderDetail._quantity = product.Quantity;
                price = product.Price * product.Quantity + price;
                webAppDevDBDataContext.TblOrderDetails.InsertOnSubmit(tblOderDetail);
            }
            tblOrder._price = price;
            webAppDevDBDataContext.TblOrders.InsertOnSubmit(tblOrder);
            webAppDevDBDataContext.SubmitChanges();
        }

        // PUT api/order/5
        public void Put([FromBody]Order order)
        {
            if (order != null)
            {
                var values = from items in webAppDevDBDataContext.TblOrders
                             where items._code == order.Code
                             select items;
                values.First()._codeStatus = order.CodeStatus;
                webAppDevDBDataContext.SubmitChanges();
                return;
            }
        }

        // DELETE api/order/5
        public void Delete([FromBody]Order order)
        {
            if (order != null && order.CodeStatus == "working")
            {
                var values = from items in webAppDevDBDataContext.TblOrders
                             where items._code == order.Code
                             select items;
                if (values.Count() < 1)
                {
                    return;
                }
                webAppDevDBDataContext.TblOrders.DeleteOnSubmit(values.First());

                var valueDetails = from items in webAppDevDBDataContext.TblOrderDetails
                         where items._codeOrder == order.Code
                         select items;

                foreach (TblOrderDetail tblOrderDetail in valueDetails)
                {
                    webAppDevDBDataContext.TblOrderDetails.DeleteOnSubmit(tblOrderDetail);
                }
                webAppDevDBDataContext.SubmitChanges();
            }
        }

        private List<Product> getLstProductDetailByCode(string codeOrder)
        {
            List<TblOrderDetail> tblOrderDetails = webAppDevDBDataContext.TblOrderDetails.ToList();
            List<TblProduct> tblProducts = webAppDevDBDataContext.TblProducts.ToList();
            List<Product> lstProduct = new List<Product>();

            foreach(TblOrderDetail tblOrderDetail in tblOrderDetails){
                if (tblOrderDetail._codeOrder == codeOrder)
                {
                    foreach (TblProduct tblProduct in tblProducts)
                    {
                        if (tblProduct._code == tblOrderDetail._codeProduct)
                        {
                            Product product = new Product();
                            product.Ord = tblProduct._ord;
                            product.Code = tblProduct._code;
                            product.Name = tblProduct._name;
                            product.CodeCategory = tblProduct._codeCategory;
                            product.Price = (long)tblProduct._price;
                            product.Quantity = (int)tblProduct._quantity;
                            product.Detail = tblProduct._detail;
                            product.UrlImage = tblProduct._urlImage;
                            product.IsAds = (bool)tblProduct._isAds;
                            lstProduct.Add(product);
                        }
                    }
                }
            }
            return lstProduct;
        }
    }
}
