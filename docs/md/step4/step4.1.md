# SinglePageApplicationの作成 - プロジェクトの作成

## プロジェクトの作成

*KnockoutTodo* という名前で ASP.NET のプロジェクトを作成します。

- テンプレートは `empty` とします。
- `MVC` にチェック
- `Web API` にチェック

<br><br>

## CSSおよびJavaScriptライブラリのインストール

`bootstrap` と `knockout.js` を *NuGet* で取得します。
`jQuery` は `bootstrap` が依存しているため、一緒にダウンロードされるはずです。

<br><br>

## ASP.NET MVCで JavaScript, CSS を minify する

昨今の Webアプリケーションでは
bootstrap や jQuery, knockout.js など、様々な JavaScript/CSSのライブラリを読み込む必要があります。

それぞれは小さなファイルですが、多数のファイルをリクエストすると
httpの通信に掛かる遅延が気になってきます。

Webアプリケーションのレスポンス改善のテクニックのひとつに
アイコンなどの画像ファイルを一つのファイルにまとめて、
表示する際にCSSで表示領域を切り取る *CSS Sprite* という方法があります。

同じように、CSSやJSを一つのファイルに結合する機能が *Web Optimization* です。

<br><br>

## Web Optimization のインストール

`Web Optimization` を *NuGet* で取得します。

`Microsoft.AspNet.Web.Optimization`

<br><br>

## LayoutView の作成

* `_LayoutPage1.cshtml` を作成

## HomeController の追加

* `Index` メソッドのみを作成

## Index View の追加

* `Index.cshtml` を作成

## 独自のCSS, JSを追加

* Contentフォルダに `base.css` を追加
* Scriptsフォルダに `app.js` を追加

## Global.asax.cs の修正

```cs
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;

namespace KnockoutTodo
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // アプリケーションのスタートアップで実行するコードです
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // css
            BundleTable.Bundles.Add(new StyleBundle("~/bundle/style").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/base.css"));
            // js
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/script").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/knockout-3.3.0.js",
                "~/Scripts/app.js"));
        }
    }
}
```

## `_LayoutPage1.cshtml` の修正

```cs
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
    @System.Web.Optimization.Styles.Render("~/bundle/style")
</head>
<body>
    <div>
        @RenderBody()
    </div>
    @System.Web.Optimization.Scripts.Render("~/bundle/script")
</body>
</html>
```

## Optimizationの確認

デバッグ実行してソースを表示

```html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <link href="/Content/bootstrap.css" rel="stylesheet"/>
<link href="/Content/bootstrap-theme.css" rel="stylesheet"/>
<link href="/Content/base.css" rel="stylesheet"/>

</head>
<body>
    <div>

<h2>Index</h2>

    </div>
    <script src="/Scripts/jquery-1.9.1.js"></script>
<script src="/Scripts/bootstrap.js"></script>
<script src="/Scripts/knockout-3.3.0.debug.js"></script>
<script src="/Scripts/app.js"></script>


<!-- Visual Studio Browser Link -->
<script type="application/json" id="__browserLink_initializationData">
    {"appName":"Microsoft エッジ","requestId":"099fe0d6ac9a43f7a5cccb0aaacebea7"}
</script>
<script type="text/javascript" src="http://localhost:49578/157766d9780c42159cf2a49ae41dd212/browserLink" async="async"></script>
<!-- End Browser Link -->

</body>
</html>
```

Web.configを書き換え

```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
  ASP.NET アプリケーションの構成方法の詳細については、
  http://go.microsoft.com/fwlink/?LinkId=169433 を参照してください
  -->
<configuration>
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="PreserveLoginUrl" value="true" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
  <system.web>
    <compilation debug="true" targetFramework="4.5.2" />
```

`compilation` の `debug` を *false* に変更して、再度デバッグ実行

-> デバッグ無しで実行する、を選択

再度、ソースを表示してみる

```html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <link href="/bundle/style?v=SckkXrq8HfNtLA3FZKd7hzAGgT9W7l4oPzHTUmZ-oa81" rel="stylesheet"/>

</head>
<body>
    <div>

<h2>Index</h2>

    </div>
    <script src="/bundle/script?v=XpigI2UJG_YaR_PiSSfaB-BStF3wCNqKejKrjK0a-UI1"></script>

</body>
</html>
```

* `link` タグ、`script` タグが一つだけになっていることを確認

* `?` 以降はファイルを変更してビルドした際に変わる。
  - 以前ビルドしたファイルをキャッシュしている場合に、必ず最新のスタイルシートやスクリプトを参照させるため。



参考: [
ASP.NET の Minify & Bundle 機能と HTML5オフラインキャッシュ](http://devadjust.exblog.jp/20061337/)
