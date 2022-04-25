using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class AccountController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/Accounts/GetAccount")]
        public IQueryable<Account> GetAccounts ()
        {
            var model = db.Accounts.Where(x => x.Status >= 0);
            return model;
        }
       
        [Route("api/Accounts/GetAccountbyUsername")]
        public Account GetAccount(String username)
        {
            return db.Accounts.Find(username);
        }
        [Route("api/Accounts/Postaccount")]
        public int Post([FromBody] Account account)
        {
            var check = db.Accounts.FirstOrDefault(x => x.Username == account.Username);
            if (check == null)
            {
                account.Status = byte.Parse(1 + "");
                db.Accounts.Add(account);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/Accounts/Putaccount")]
        public int Put([FromBody]Account account)
        {
            db.Accounts.Add(account);
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/Accounts/Deleteaccount")]
        public int Delete(String Username)
        {
            var check = db.Accounts.Find(Username);
            check.Status = byte.Parse(-1 + "");
            db.Accounts.Add(check);
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        
    }
}
