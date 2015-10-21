# Todoアプリを作成する

Login画面とHome画面の2画面構成とします。

## Login画面の作成

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

`_LayoutPage1.cshtml`

```html
<!DOCTYPE html>

<html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>@ViewBag.Title</title>
    @System.Web.Optimization.Styles.Render("~/bundle/style")
</head>
<body>
    @RenderBody()

    @System.Web.Optimization.Scripts.Render("~/bundle/script")
</body>
</html>
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


## Home画面の作成

まずは HTMLだけを先に作成し、雰囲気を確認します。



`Views/Home/Index.cshtml`

```html

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_LayoutPage1.cshtml";
}

<nav class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <a class="navbar-brand" href="#">KnockoutTodo</a>
        </div>
        <button class="btn btn-link navbar-btn navbar-right">ログアウト</button>
    </div>
</nav>

<div class="container">
    <div class="row">
        <div class="col-md-4">
            <div class="list-group">
                <a href="#" class="list-group-item active">
                    <h4 class="list-group-item-heading">
                        item title
                    </h4>
                    <p class="list-group-item-text">item text</p>
                </a>
                <a href="#" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        item title
                    </h4>
                    <p class="list-group-item-text">item text</p>
                </a>
                <a href="#" class="list-group-item">
                    <h4 class="list-group-item-heading">
                        item title
                    </h4>
                    <p class="list-group-item-text">item text</p>
                </a>
            </div>

            <div>
                <button class="btn btn-primary btn-info btn-lg btn-block">
                    <span class="glyphicon glyphicon-plus"></span> 新しいToDoを追加
                </button>
            </div>
        </div>

        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">#id</h3>
                </div>
                <div class="panel-body">
                    <form class="form-horizontal">
                        <div class="form-group">
                            <label for="txtSummary" class="col-sm-2 control-label">概要</label>
                            <div class="col-sm-10"><input type="text" class="form-control" id="txtSummary"></div>
                        </div>
                        <div class="form-group">
                            <label for="txtDetail" class="col-sm-2 control-label">詳細</label>
                            <div class="col-sm-10"><textarea id="txtDetail" rows="3" class="form-control"></textarea></div>
                        </div>
                        <div class="form-group">
                            <label for="txtLimit" class="col-sm-2 control-label">期限</label>
                            <div class="col-sm-10"><input type="date" class="form-control" id="txtLimit"></div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" id="chkDone"> 完了
                                    </label>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="panel-footer">
                    <button class="btn btn-primary">
                        <span class="glyphicon glyphicon-floppy-disk"></span>
                        登録
                    </button>
                    <button class="btn btn-danger">
                        <span class="glyphicon glyphicon-trash"></span>
                        削除
                    </button>
                    <button class="btn btn-default">
                        キャンセル
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>
```

デバッグ実行して画面のイメージを確認します。

<br><br>

## Knockoutの実装

## 1. ToDoリストを画面に表示する

### TodoModel

まず、モデルを定義します。

`app.js`

```js
/**
 * Todo Model
 * @param 初期値 {object}
 */
var ToDoModel = function (params) {
  var self = this;

  // 引数未指定なら空のobjectを生成
  if (!params) {
    params = {};
  }

  // 初期値
  var options = {
    id: 0,
    summary: '',
    detail: '',
    limit: '',
    done: false
  };

  // 初期値を引数で指定された値で上書き
  $.extend(options, params);

  // Id (number)
  self.id = ko.observable(options.id);
  // 概要
  self.summary = ko.observable(options.summary);
  // 詳細
  self.detail = ko.observable(options.detail);
  // 期限
  self.limit = ko.observable(options.limit);
  // 完了
  self.done = ko.observable(options.done);
};
```

`$.extend(obj1, obj2)` は jqueryのメソッドで、`obj2` の内容を `obj1` にマージします。

ここでは、初期値を引数で指定されたパラメータで上書きします。

その後、各値を `observable` の変数にセットします。

* observable

### ViewModelの作成

```js
/**
 * ViewModel
 */
var AppViewModel = function () {
  var self = this;

  // Todoリスト
  self.todoList = ko.observableArray([
    new ToDoModel({ id: 1, summary: 'hoge', detail: 'foobar1', limit: '', done:false }),
    new ToDoModel({ id: 2, summary: 'foo',  detail: 'foobar2', limit: '', done:false }),
    new ToDoModel({ id: 3, summary: 'bar',  detail: 'foobar3', limit: '', done:false })
  ]);
};
```

* observableArray

### エントリポイントの作成

```js
// エントリポイント
$(function(){
  ko.applyBindings(new AppViewModel());
});
```

HTMLが読み込まれたタイミングで、ViewModel を body に紐付けています。


```html
<div class="col-md-4">
    <div class="list-group" data-bind="foreach: todoList">
        <a href="#" class="list-group-item">
            <h4 class="list-group-item-heading"
                data-bind="text: summary"></h4>
            <p class="list-group-item-text"
               data-bind="text: detail"></p>
        </a>
    </div>
```

* data-bind
  - foreach
  - text

## 2. 選択したTodoを編集フォームに表示する

編集フォームのデータは、登録ボタンをクリックしたタイミングで反映させたいので
入力内容が即時反映されないように、選択されたTodoをコピーし、別のTodoModelのインスタンスに格納します。

登録時は、別のインスタンスから該当のインスタンスに値を戻します。

```js
var AppViewModel = function () {
  var self = this;

  // Todoリスト
  self.todoList = ko.observableArray([
    new ToDoModel({ id: 1, summary: 'hoge', detail: 'foobar1', limit: '', done:false }),
    new ToDoModel({ id: 2, summary: 'foo',  detail: 'foobar2', limit: '', done:false }),
    new ToDoModel({ id: 3, summary: 'bar',  detail: 'foobar3', limit: '', done:false })
  ]);

  // 選択されたTodoを格納
  self.selectedItem = ko.observable();

  /**
   * リストからTodoを選択する
   * @param item {ToDoModel} 選択された項目
   */
  self.selectTodo = function (item) {
    self.selectedItem(new ToDoModel({
      id: item.id(),
      summary: item.summary(),
      detail: item.detail(),
      limit: item.limit(),
      done: item.done()
    }));
  };
};
```

### リストのアイテムとメソッドを紐付ける

```html
<div class="list-group" data-bind="foreach: todoList">
    <a href="#" class="list-group-item"
       data-bind="click: $root.selectTodo">
        <h4 class="list-group-item-heading"
            data-bind="text: summary"></h4>
        <p class="list-group-item-text"
           data-bind="text: detail"></p>
    </a>
</div>
```

aタグに `data-bind="click: $root.selectedItem"` と追記します。

`$root` は body にバインドした AppViewModel を表します。

`foreach` の中では、h4タグやpタグでのデータバインドのように、
デフォルトでは配列要素のプロパティが選択されます。

aタグをクリックしたときには、配列要素では無く、
AppViewModelに定義したメソッドを呼び出したいのでこのような記述になります。

メソッドの引数には配列の各要素が渡されます。


### 編集フォームと selectedItem を紐付ける

```html
<div class="panel panel-default" data-bind="visible: selectedItem, with: selectedItem">
    <div class="panel-heading">
        <h3 class="panel-title">#<span data-bind="text: id"></span></h3>
    </div>
    <div class="panel-body">
        <form class="form-horizontal">
            <div class="form-group">
                <label for="txtSummary" class="col-sm-2 control-label">概要</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control"
                           id="txtSummary"
                           data-bind="value: summary">
                </div>
            </div>
            <div class="form-group">
                <label for="txtDetail" class="col-sm-2 control-label">詳細</label>
                <div class="col-sm-10">
                    <textarea id="txtDetail" rows="3" class="form-control"
                              data-bind="value: detail"></textarea>
                </div>
            </div>
            <div class="form-group">
                <label for="txtLimit" class="col-sm-2 control-label">期限</label>
                <div class="col-sm-10">
                    <input type="date" class="form-control"
                           id="txtLimit"
                           data-bind="value: limit">
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="chkDone"
                                   data-bind="checked: done"> 完了
                        </label>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <div class="panel-footer">
        <button class="btn btn-primary">
            <span class="glyphicon glyphicon-floppy-disk"></span>
            登録
        </button>
        <button class="btn btn-danger">
            <span class="glyphicon glyphicon-trash"></span>
            削除
        </button>
        <button class="btn btn-default">
            キャンセル
        </button>
    </div>
</div>
```

* visible

* with

* checked


## 選択中の項目を強調する

現在編集しているTodoがどれなのか分かりやすいように、選択されている要素の背景色を青にします。

bootstrap の active クラスを設定するだけで、青地に白文字で表示されるようになりますので
knockout では 選択要素の aタグに activeクラスをセットするように実装します。

### 要素が選択されているかどうかを判定するメソッドの追加

ViewModel に、その要素が選択されている要素かどうかを判定するメソッドを追加します。

```js
/**
 * リストの項目が選択されたTodoかどうか
 * @param target {TodoModel}
 * @return {boolean}
 */
self.isActive = function (target) {
  var item = self.selectedItem();
  if (item) {
    return target.id() == item.id();
  }
  return false;
};
```

### 判定メソッドをViewに紐付ける

aタグを以下のように修正します。

```html
<a href="#" class="list-group-item"
  data-bind="click: $root.selectTodo, css: { active: $root.isActive($data) }">
  <h4 class="list-group-item-heading">
    <span data-bind="text: summary"></span>
  </h4>
  <p class="list-group-item-text" data-bind="text: detail"></p>
</a>
```

* css バインディング

* $data
