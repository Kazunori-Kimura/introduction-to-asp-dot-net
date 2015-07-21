# 3. `ASP.NET MVC`によるWebアプリケーション開発 - 応用編

## 認証と認可

### メンバーシップ フレームワーク

ASP.NET 2.0 以降から採用された認証ライブラリです。比較的シンプルに実装できるため、広く利用されています。

今回はメンバーシップフレームワークによる認証・認可機能の実装方法を解説しますが、
最新の認証ライブラリについてもご紹介いたします。

### 【解説】ASP.NET Identity

ASP.NET Identity は Visual Studio 2013 から新たに搭載された認証ライブラリです。

以下の様な特徴があります。

- Entity Framework を基板としているため、アカウント情報の管理に関する実装が容易である
- ActiveDirectoryによる認証に対応
- Twitter, Facebook, Googleなどのソーシャルアカウントによる認証に対応

Visual Studio 2013 で ASP.NETプロジェクトを作成するときに生成されるソースコードには、ASP.NET Identityによる認証機能が予め実装されています。

<br>

## 基本的なフォーム認証の実装

簡単なWebアプリケーションの作成を題材に、フォーム認証の実装方法について解説します。

### 概要

* 画面構成
  - ログイン画面
  - ホーム画面 (全ユーザー共通の画面)
  - 管理画面 (管理者アカウントのみ表示可能)

* 画面遷移

    ログイン画面 -(ログイン成功)-> ホーム画面 -(管理者のみ)-> 管理画面


### 1. Providerクラスの実装

(1) MembershipProviderの実装

`MembershipProvider` クラスを継承したクラスを実装します。

幾つか `override` しないといけないメソッドがありますが、
今回使用するのは `ValidateUser` メソッドだけなので、このメソッドのみ実装します。

- `ValidateUser` は `username` と `password` を受け取って認証の成否を返します。

このアプリケーションでは認証の動作を確認するため、 `username` 、 `password` は固定としています。

<br>

* `CustomMembershipProvider.cs`

```cs
namespace AuthTest.Models
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            // とりあえず固定で認証
            if ("administrator".Equals(username) && "password".Equals(password))
            {
                return true;
            }
            if ("user".Equals(username) && "password".Equals(password))
            {
                return true;
            }
            return false;
        }

        // 〜〜略〜〜
```


(2) RoleProviderの実装

つづいて、 `RoleProvider` クラスを継承したクラスを実装します。

`MembershipProvider` と同様、今回使用するメソッドのみ中身を実装します。

- `IsUserInRole` メソッドは 指定されたユーザーが、該当するロールに所属しているかどうかを返します。
- `GetRolesForUser` メソッドは 指定されたユーザーが所属するロールの配列を返します。

<br>

* CustomRoleProvider.cs

```cs
namespace AuthTest.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            if ("administrator".Equals(username) && "Administrators".Equals(roleName))
            {
                return true;
            }
            if ("user".Equals(username) && "Users".Equals(roleName))
            {
                return true;
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            if ("administrator".Equals(username))
            {
                return new string[] { "Administrators" };
            }
            return new string[] { "Users" };
        }

        // 〜〜略〜〜
```

### 2. ViewModelの実装

`ViewModel` は実際のModel (テーブルの形) と画面に表示する項目との違いを吸収するために使用するテクニックです。

画面とコントローラとのやりとりは `ViewModel` を介して行います。

画面からの入力内容をデータベースに反映する場合に コントローラにて `ViewModel` からデータを取得し、
`Model` にデータを格納して、データベースに保存します。


今回はログイン画面の入力項目を `LoginViewModel` として定義します。

* LoginViewModel.cs

```cs
namespace AuthTest.Models
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

<br>

### 3. Controllerの実装

(1) LoginControllerの実装

```cs
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
                FormsAuthentication.SetAuthCookie(model.UserName, false);
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
```

`[AllowAnonymous]` は `LoginController` へのアクセス時に認証が不要とします。

ログイン画面で入力された内容は `LoginViewModel` に格納されます。

送られてきたユーザー名、パスワードを `MembershipProvider` でチェックし、認証OKであれば
`FormsAuthentication.SetAuthCookie` メソッドを実行します。

第2引数の boolean は、認証クッキーを残すかどうかのフラグです。
ログイン画面によくある、「次回から自動的にログイン」とか「ログインしたままにする」といったチェックボックスの機能です。

認証後、Home画面にリダイレクトしています。


`SignOut` メソッドで認証クッキーが削除され、ログアウトした状態となります。

<br>

(2) HomeControllerの実装


* HomeController.cs

```cs
namespace AuthTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

`[Authorize]` は `HomeController` にアクセスするために認証が必要であることを示します。

認証されていない状態でアクセスすると、ログイン画面にリダイレクトされます。

<br>

(3) AdminControllerの実装

namespace AuthTest.Controllers
{
    [Authorize(Roles="Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}

`[Authorize(Roles="Administrators")]` は `AdminController` に `Administrators` ロールに属するユーザーのみアクセス可能であることを示します。
それ以外のユーザーが該当画面にアクセスした場合、ログイン画面にリダイレクトされます。

### 3. Viewの作成

(1) `/Views/Shared/_LayoutPage1.cshtml` の作成

  - `Shared` フォルダの作成
  - レイアウトページの作成

(2) `/Views/Login/Index.cshtml` の作成

  - `LoginController`を右クリック -> `Add View` を選択
  - `LoginViewModel` の `Create` として `Index.cshtml` を作成
  - 不要な項目の削除、ボタンのラベルを `SignIn` に変更

```html
@model AuthTest.Models.LoginViewModel

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_LayoutPage1.cshtml";
}

<h2>SignIn</h2>


@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    <div class="form-horizontal">
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.UserName, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.UserName, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.UserName, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Password, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Password, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="SignIn" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<script src="~/Scripts/jquery-1.10.2.min.js"></script>
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
```

(3) HomeViewの作成
(4) AdminViewの作成

  - `Title` に `Home`, `Admin` と表記 (どちらの画面を表示しているかわかりやすいように)
  - `Home` と `Admin` を相互に遷移できるようにリンクを追加

<br>

### 4. web.Configの設定

```xml
<system.web>
  <compilation debug="true" targetFramework="4.5.1"/>
  <httpRuntime targetFramework="4.5.1"/>
  <authentication mode="Forms">
    <forms loginUrl="~/Login/Index"></forms>
  </authentication>
  <membership defaultProvider="CustomMembershipProvider">
    <providers>
      <clear/>
      <add name="CustpmMembershipProvider" type="AuthTest.Models.CustomMembershipProvider"/>
    </providers>
  </membership>
  <roleManager enabled="true" defaultProvider="CustomRoleProvider">
    <providers>
      <clear/>
      <add name="CustomRoleProvider" type="AuthTest.Models.CustomRoleProvider"/>
    </providers>
  </roleManager>
</system.web>
```

* `authentication` タグの追加
  - `mode="Forms"` でフォーム認証を行うことを指定
  - `forms` タグでログイン画面を指定
* `membership` タグの追加
  - `CustomMembershipProvider` クラスを指定
* `roleManager` タグの追加
  - `CustomRoleProvider` クラスを指定


### 5. デバッグ実行して動作確認

- ログイン画面が表示されることを確認
- user / password でログインし ホーム画面に遷移することを確認
- Adminリンクをクリックするとログイン画面に戻されることを確認
- URLを指定して ~/Home/Index を指定してもログイン画面に戻されることを確認
- URLを指定して ~/Admin/Index を指定してもログイン画面に戻されることを確認
- administrator / password でログインし ホーム画面に遷移することを確認
- Adminリンクをクリックして管理画面に遷移することを確認
- SignOutリンクをクリックしてログイン画面に遷移することを確認
- URLを指定して ~/Home/Index を指定してもログイン画面に戻されることを確認
- URLを指定して ~/Admin/Index を指定してもログイン画面に戻されることを確認


<br>
<br>

## (まともな) Todo管理アプリケーションの作成

ユーザー管理機能のついた Todo管理アプリケーションを作成します。

* ユーザー毎に個別のTodoを管理
* 管理者のみユーザーアカウントの管理画面を表示



### データモデル作成

* Userモデル


### LINQ to Entities

* クエリ構文

* メソッド構文

* 遅延実行


### 初期データの登録

* イニシャライザー

### マイグレーション

## 認証と認可


## 部分View

### メニュー部分を別ファイルに切り出す

## Windows Azureで公開する


-------------


### partial class

クラスや構造体、インターフェイスやメソッドの定義を、複数のソース ファイルに分割できます。 各ソース ファイルには、型やメソッドの定義のセクションが含まれ、分割されたすべての部分はアプリケーションのコンパイル時に結合されます。

自動生成ソースを使用する際に、ソース ファイルを再作成せずにコードをクラスに追加できます。 Visual Studio では、Windows フォームや Web サービス ラッパー コードなどを作成するときにこのアプローチを使用します。 Visual Studio によって作成されたファイルを変更せずに、これらのクラスを使用するコードを作成できます。

https://msdn.microsoft.com/ja-jp/library/wa80x488.aspx

### [?? 演算子](https://msdn.microsoft.com/ja-jp/library/ms173224.aspx)

?? 演算子は、null 合体演算子と呼ばれます。左側のオペランドが null 値でない場合には左側のオペランドを返し、
null 値である場合には右側のオペランドを返します。

-------------

```ps
PM> Enable-Migrations -ContextTypeName TodoApp.Models.TodoesContext
Checking if the context targets an existing database...
Code First Migrations enabled for project TodoApp.
```
