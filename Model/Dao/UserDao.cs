using Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }

        public ResponseModel Login(string username, string password)
        {
            var result = new ResponseModel();
            var user = db.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                result = new ResponseModel()
                {
                    Code = ResCode.Failure,
                    Message = "Username does not exist"
                };
                return result;
            }
            else if(user.Status == false)
            {
                result = new ResponseModel()
                {
                    Code = ResCode.Failure,
                    Message = "Account has been disabled"
                };
                return result;
            }
            else if (user.Password != password)
            {
                result = new ResponseModel()
                {
                    Code = ResCode.Failure,
                    Message = "Password is not correct"
                };
                return result;
            }
            else
            {
                result = new ResponseModel()
                {
                    Code = ResCode.Success,
                    Message = "Login successful"
                };
                return result;
            }
        }
    }
}
