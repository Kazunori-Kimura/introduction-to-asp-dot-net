# `ASP.NET Web API`と`knockout.js`によるSingle Page Application開発 (2)




## おまけ: Login画面の作成

* LoginViewModelの追加

`Models/LoginViewModel.cs`

```cs
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KnockoutTodo.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("パスワード")]
        public string Password { get; set; }
    }
}
```

* LoginControllerの追加

`Controllers/LoginController.cs`

```cs
using KnockoutTodo.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace KnockoutTodo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="UserName,Password")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                // メイン画面に遷移
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
```

とりあえず、ログインボタンを押されたら何もチェックせず `Home/Index` に遷移する

<br><br>

* LoginViewの追加

`Views/Login/Index.cshtml`

```html
@model KnockoutTodo.Models.LoginViewModel

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_LayoutPage1.cshtml";
}

<div class="container">
    @using (Html.BeginForm("Index", "Login", FormMethod.Post,
                                                new { @class = "form-signin" }))
    {
        @Html.AntiForgeryToken()

        <h2 class="form-signin-heading">Please sign in</h2>

        <label for="UserName" class="sr-only">User Name</label>
        <input type="text" id="UserName" name="UserName" class="form-control"
               placeholder="User Name" required autofocus>
        <label for="Password" class="sr-only">Password</label>
        <input type="password" id="Password" name="Password" class="form-control"
               placeholder="Password" required>

        <button class="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
    }
</div>
```


* base.cssの修正

`base.css`

```css
body {
  margin-top: 70px;
}

/* -- login -- */
.form-signin {
  max-width: 330px;
  padding: 15px;
  margin: 0 auto;
}
.form-signin .form-signin-heading,
.form-signin .checkbox {
  margin-bottom: 10px;
}
.form-signin .checkbox {
  font-weight: normal;
}
.form-signin .form-control {
  position: relative;
  height: auto;
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
  padding: 10px;
  font-size: 16px;
}
.form-signin .form-control:focus {
  z-index: 2;
}
.form-signin input[type="email"] {
  margin-bottom: -1px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}
.form-signin input[type="password"] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
}
```

```xml
<?xml version="1.0"?>
<!--
  ASP.NET アプリケーションの構成方法の詳細については、
  http://go.microsoft.com/fwlink/?LinkId=169433 を参照してください
  -->
<configuration>
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0"/>
    <add key="webpages:Enabled" value="false"/>
    <add key="PreserveLoginUrl" value="true"/>
    <add key="ClientValidationEnabled" value="true"/>
    <add key="UnobtrusiveJavaScriptEnabled" value="true"/>
  </appSettings>
  <system.web>
    <compilation debug="true" targetFramework="4.5.2"/>
    <httpRuntime targetFramework="4.5.2"/>
    <authentication mode="Forms">
      <forms loginUrl="~/Login/Index"/>
    </authentication>
```
