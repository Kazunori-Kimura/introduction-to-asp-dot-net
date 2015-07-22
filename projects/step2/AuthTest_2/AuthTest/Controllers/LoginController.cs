using AuthTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthTest.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        readonly CustomMembershipProvider membershipProvider = new CustomMembershipProvider();

        // GET: Login
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include="UserName,Password")] LoginViewModel model)
        {
            if (this.membershipProvider.ValidateUser(model.UserName, model.Password))
            {
                // Sessionからユーザー情報を取得
                int userId = (int)Session["AuthUserId"];
                // 認証Cookieを登録
                FormsAuthentication.SetAuthCookie(userId.ToString(), false);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "ログインに失敗しました。";
            return View(model);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}