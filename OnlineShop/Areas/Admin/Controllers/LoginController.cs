using Common;
using Model.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var resp = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (resp.Code == ResCode.Success)
                {
                    var user = dao.GetByUsername(model.Username);

                    var userSession = new UserLogin()
                    {
                        UserID = user.ID,
                        Username = user.UserName
                    };

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", resp.Message);
                }
            }
            return View("Index");
        }
    }
}