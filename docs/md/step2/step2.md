# 2. `ASP.NET MVC`によるWebアプリケーション開発

## `ASP.NET MVC`の概要


---

## ToDoアプリの開発

参考: [Getting Started with Entity Framework 6 Code First using MVC 5](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)


### 仕様概要

* ToDoリストの一覧を表示 (index)
* ToDoをクリックすると詳細を表示 (edit)
  - 詳細ページで更新/削除

#### Todoリストの項目

* id
* summary
* detail
* limit
* done

### 環境について

* Visual Studio 2013
* SQL Server LocalDB
  - Visual Studioを普通にセットアップすると、合わせてインストールされます
* Entity Framework 6

---

### `ASP.NET MVC5` と `Entity Framework` によるアプリケーションの開発

#### プロジェクトの作成

#### Entity Framework のインストール

NuGetを使用して、最新の`Entity Framework`をインストールします。

##### NuGet

Visual Studio用のパッケージ管理システム。
rubyのgemやpythonのpipのようなツール。


#### Modelの作成

##### Entity Framework の コード ファースト開発

データ構造を表現するPOCO (Plain Old Clr Object: 特定のClassに依存しない
モデル クラス) と POCOを管理する Contextクラスを定義することで、
Entity Frameworkがデータベースを生成します。
(開発段階においてDatabaseの操作は全く必要ありません。)

    `Entity Framework 4.1` から提供された機能です。

詳細については [
Entity Framework (EF) の概要](http://msdn.microsoft.com/ja-jp/data/ee712907) を
参照してください。

##### Todoクラスの作成

* `Models`を右クリックし、「追加」→「クラス」を選択します。


##### TodoesContextクラスの作成

* `Models`を右クリックし、「追加」→「クラス」を選択します。


#### ControllerとViewの作成

##### 共通レイアウトの作成

* `Views`を右クリックし、「追加」→「MVC 5 レイアウト ページ (Razor)」 を選択します。

`_LayoutPage1.cshtml`

```html
<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>@ViewBag.Title</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">
</head>
<body>
    <div class="container">
        @RenderBody()
    </div>
</body>
</html>
```

`Bootstrap`を読み込むように指定します。
また、`div`に`class="container"`の指定を追加します。


##### Controllerの作成

スキャフォールディング(Scaffolding、「骨組み」「足場」という意味)によって、
Create（作成）、Read（参照）、Update（更新）、Delete（削除）のような定型的なコードの骨組みを自動生成できます。

* `Controllers`を右クリックし、「追加」→「コントローラー」を選択します。

以下のファイルが生成されます。

* Controllers/
  - TodoesController.cs
* Views/Todoes/
  - Create.cshtml
  - Delete.cshtml
  - Details.cshtml
  - Edit.cshtml
  - Index.cshtml

#### デフォルトページの設定

`App_Start/RouteConfig.cs`を修正し、デフォルトのページを
ToDoの一覧ページに変更します。

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Todoes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
```

#### デバッグ実行

F5キーを押して、デバッグ実行を行います。
ブラウザが起動し、一覧ページが表示されます。

ToDoの追加、変更、削除が行えることを確認します。


