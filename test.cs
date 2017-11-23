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
    public class UserController : ApiController
    {

        WebAppDevDBDataContext webAppDevDBDataContext;
        public UserController()
        {
            webAppDevDBDataContext = new WebAppDevDBDataContext();
        }
        // GET api/user
        public IEnumerable<User> Get()
        {
            List<TblUser> tblUsers = webAppDevDBDataContext.TblUsers.ToList();
            List<User> lstUser = new List<User>();
            foreach (TblUser tblUser in tblUsers)
            {
                User user = new User();
                user.Ord = tblUser._ord;
                user.Code = tblUser._code;
                user.Name = tblUser._name;
                user.RoleCode = tblUser._roleCode;
                user.Password = tblUser._password;
                user.Phone = tblUser._phone;
                lstUser.Add(user);
            }
            return lstUser;
        }

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        public Result Post([FromBody]User user)
        {
            Result result = new Result();
            if (user == null || user.Phone == null || user.Password == null || user.Name == null)
            {
                result.Status = false;
                return result;
            }
            if (user.RoleCode == null || user.RoleCode != "adm")
            {
                user.RoleCode = "ctm";
            }
            TblUser tblUserDB = webAppDevDBDataContext.TblUsers.OrderBy(c => c._ord).ToList().Last();
            TblUser tblUser = new TblUser();
            tblUser._code = "ctm" + (tblUserDB._ord + 1);
            tblUser._name = user.Name;
            tblUser._roleCode = user.RoleCode;
            tblUser._password = user.Password;
            tblUser._phone = user.Phone; 
            webAppDevDBDataContext.TblUsers.InsertOnSubmit(tblUser);
            try
            {
                webAppDevDBDataContext.SubmitChanges();
            }
            catch (Exception)
            {
                result.Status = false;
                return result;
            }
            result.Status = true;
            return result;
        }

        // PUT api/user/5
        public void Put([FromBody]User user)
        {
            var values = from items in webAppDevDBDataContext.TblUsers
                         where items._code == user.Code
                         select items;
            if (values.Count() < 1)
            {
                return;
            }
            values.First()._name = user.Name;
            values.First()._roleCode = user.RoleCode;
            values.First()._password = user.Password;
            values.First()._phone = user.Phone;

            webAppDevDBDataContext.SubmitChanges();
        }

        // DELETE api/user/5
        public bool Delete([FromBody]User user)
        {
            var values = from items in webAppDevDBDataContext.TblUsers
                         where items._code == user.Code
                         select items;
            if (values.Count() < 1)
            {
                return false;
            }
            webAppDevDBDataContext.TblUsers.DeleteOnSubmit(values.First());
            try
            {
                webAppDevDBDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
