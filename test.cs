using Newtonsoft.Json;
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
    public class CategoryController : ApiController
    {
        WebAppDevDBDataContext webAppDevDBDataContext;
        public CategoryController()
        {
            webAppDevDBDataContext = new WebAppDevDBDataContext();
        }

        // GET api/category
        public IEnumerable<Category> Get()
        {
            List<TblCategory> tblCategorys = webAppDevDBDataContext.TblCategories.ToList();
            List<Category> lstCategory = new List<Category>();
            foreach (TblCategory tblCategory in tblCategorys)
            {
                Category category = new Category();
                category.Ord = tblCategory._ord;
                category.Code = tblCategory._code;
                category.Name = tblCategory._name;
                lstCategory.Add(category);
            }
            return lstCategory;
        }

        // GET api/category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/category
        public void Post([FromBody]Category category)
        {
            if (category != null && category.Code != null && category.Name != null)
            {
                TblCategory tblCategory = new TblCategory();
                tblCategory._name = category.Name;
                tblCategory._code = category.Code;
                webAppDevDBDataContext.TblCategories.InsertOnSubmit(tblCategory);
                try
                {
                    webAppDevDBDataContext.SubmitChanges();
                }
                catch (Exception) { }
            }
        }

        // PUT api/category/5
        public void Put([FromBody]Category category)
        {
            if (category != null && category.Ord != null && category.Code != null && category.Name != null)
            {
                var values = from items in webAppDevDBDataContext.TblCategories
                             where items._ord == category.Ord
                             select items;
                if (values.Count() < 1)
                {
                    return;
                }
                foreach (TblCategory tblCategory in values)
                {
                    tblCategory._name = category.Name;
                    break;
                }
                webAppDevDBDataContext.SubmitChanges();
            }
        }

        // DELETE api/category/5
        public bool Delete([FromBody]Category category)
        {
            var values = from items in webAppDevDBDataContext.TblCategories
                         where items._ord == category.Ord
                         select items;
            if (values.Count() < 1)
            {
                return false;
            }
            foreach (TblCategory tblCategory in values)
            {
                webAppDevDBDataContext.TblCategories.DeleteOnSubmit(tblCategory);
                break;
            }
            try
            {
                webAppDevDBDataContext.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
