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
    public class ProductController : ApiController
    {
        WebAppDevDBDataContext webAppDevDBDataContext;
        public ProductController()
        {
            webAppDevDBDataContext = new WebAppDevDBDataContext();
        }
        // GET api/product
        public IEnumerable<Product> Get()
        {
            List<TblProduct> tblProducts = webAppDevDBDataContext.TblProducts.ToList();
            List<Product> lstProduct = new List<Product>();
            foreach (TblProduct tblProduct in tblProducts)
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
            return lstProduct;
        }

        // GET api/product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/product
        public void Post([FromBody]Product product)
        {
            if(product == null 
                || product.Ord == null 
                || product.CodeCategory == null
                || product.Name == null
                || product.Price == null
                || product.Quantity == null 
                || product.Detail == null
                || product.UrlImage == null)
            {
                return;
            }
            if(product.Code == null){
                product.Code = "pro" + (webAppDevDBDataContext.TblProducts.OrderBy(c=>c._ord).ToList().Last()._ord + 1);
            }
            TblProduct tblProduct = new TblProduct();
            tblProduct._code = product.Code;
            tblProduct._name = product.Name;
            tblProduct._codeCategory = product.CodeCategory;
            tblProduct._quantity = product.Quantity;
            tblProduct._price = product.Price;
            tblProduct._quantity = product.Quantity;
            tblProduct._urlImage = product.UrlImage;
            tblProduct._isAds = false;
            webAppDevDBDataContext.TblProducts.InsertOnSubmit(tblProduct);
            try
            {
                webAppDevDBDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }

        // PUT api/product/5
        public void Put([FromBody]Product product)
        {
            var values = from items in webAppDevDBDataContext.TblProducts
                         where items._code == product.Code
                         select items;
            if (values.Count() < 1)
            {
                return;
            }
            foreach (TblProduct tblProduct in values)
            {
                tblProduct._name = product.Name;
                tblProduct._codeCategory = product.CodeCategory;
                tblProduct._quantity = product.Quantity;
                tblProduct._price = product.Price;
                tblProduct._quantity = product.Quantity;
                tblProduct._detail = product.Detail;
                tblProduct._urlImage = product.UrlImage;
                tblProduct._isAds = product.IsAds;
            }
            webAppDevDBDataContext.SubmitChanges();
        }

        // DELETE api/product/5
        public void Delete([FromBody]Product product)
        {
            var values = from items in webAppDevDBDataContext.TblProducts
                         where items._code == product.Code
                         select items;
            if (values.Count() < 1)
            {
                return;
            }
            webAppDevDBDataContext.TblProducts.DeleteOnSubmit(values.First());
            webAppDevDBDataContext.SubmitChanges();
        }
    }
}
