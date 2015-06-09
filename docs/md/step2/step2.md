# 2. `ASP.NET MVC`によるWebアプリケーション開発

## `ASP.NET MVC`の概要

### MVC とは？

> [Model View Controller](http://ja.wikipedia.org/wiki/Model_View_Controller)
>
> MVC（Model View Controller モデル・ビュー・コントローラ）は、ユーザーインタフェースをもつアプリケーションソフトウェアを実装するためのデザインパターンである。
> アプリケーションソフトウェアの内部データを、ユーザーが直接参照・編集する情報から分離する。そのためにアプリケーションソフトウェアを以下の3つの部分に分割する。
> 1. model: アプリケーションデータ、ビジネスルール、ロジック、関数
> 2. view: グラフや図などの任意の情報表現
> 3. controller: 入力を受け取りmodelとviewへの命令に変換する

元々は `Smalltalk` における ウィンドウプログラム開発の設計指針として生まれたものです。

`ASP.NET MVC`は MVCのデザインパターンで ASP.NET Webアプリケーション を開発するにあたって
必須であったり、便利な機能を提供するフレームワークです。

---

<br>

## ToDoアプリの開発

参考: [Getting Started with Entity Framework 6 Code First using MVC 5](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)

* 以降の手順は、ASP.NET MVC 5 と Entity Framework 6 を活用して、非常にシンプルなWebアプリケーションを、簡単・簡潔に作成する事を目的にしています。
必ずこの手順で開発しなければならない、ということではありません。

### 仕様概要

* ToDoリストの一覧を表示 (index)
* ToDoをクリックすると詳細を表示 (edit)
  - 詳細ページで更新/削除

#### Todoリストの項目

* id: Todoを一意に特定する数値。
* summary: 概要。文字列。
* detail: 詳細。文字列。
* limit: 期限。日時。
* done: 完了フラグ。真偽値。

### 環境について

* Visual Studio 2013
  - 最新のupdateを適用している前提です。
  - 設定によって画面の構成は異なります。(ソリューション エクスプローラーが右側にある、など)
* SQL Server LocalDB
  - Visual Studioを普通にセットアップすると、合わせてインストールされます。
* Entity Framework 6

---

### `ASP.NET MVC5` と `Entity Framework` によるアプリケーションの開発

#### プロジェクトの作成

* 「テンプレート」→「Visual C#」→「Web」を選択
* 「ASP.NET Webアプリケーション」を選択し、名前を適当に入力して「OK」をクリックします。

![新しいプロジェクト](./images/WS000000.JPG)


* 「テンプレートの選択」で「Empty」を選択
* 「以下にフォルダーおよびコア参照を追加：」で「MVC」にチェックしてください。
* 「OK」をクリックします。

![新規 ASP.NET プロジェクト](./images/WS000001.JPG)

* 以下の様なフォルダ構成になります。

![ASP.NET MVCプロジェクトのフォルダ構成](./images/WS000002.JPG)


#### Entity Framework のインストール

NuGetを使用して、最新の`Entity Framework`をインストールします。

##### Entity Framework

> [Entity Framework](https://msdn.microsoft.com/ja-jp/data/ef.aspx)
>
> Entity Framework (EF) は、.NET 開発者がドメイン固有のオブジェクトを使用してリレーショナル データを処理できるようにするオブジェクト リレーショナル マッパーです。
> 開発者が通常、記述する必要のあるデータ アクセス コードがほとんど不要になります。

------

##### O/Rマッパーとは

* O/Rマッピング ... オブジェクトの各プロパティを、RDBのテーブルの各フィールドに関連付けること。
* O/Rマッパー ... O/Rマッピングを行うライブラリのこと。

Microsoft謹製のものが `EntityFramework`。  
軽量で、最も利用されていると思われるのが [dapper](https://github.com/StackExchange/dapper-dot-net)。

------


##### NuGet

Visual Studio用のパッケージ管理システム。
rubyのgemやpythonのpipのようなツール。

コマンドラインやPowerShellからも利用可能ですが、今回は Visual Studio が用意している GUI で操作します。

##### 手順

* 「ソリューション エクスプローラー」でプロジェクト名を右クリック
* 「NuGet パッケージ マネージャー」→「ソリューションの NuGet パッケージの管理」を選択します。

![NuGetパッケージ マネージャーを開く](./images/WS000003.JPG)

* `EntityFramework`を検索し、「インストール」をクリックします。
  - NuGetパッケージをインストールする際は、名前、作成者、バージョンを確認するようにしてください。似たような名称のパッケージが多数あります。

![NuGetパッケージの管理](./images/WS000004.JPG)

* 画面の指示に従ってインストールを進めます。
* インストール完了後、`EntityFramework`にチェックが入っている事を確認します。
* 「閉じる」をクリックします。

![インストール完了](./images/WS000007.JPG)

#### Modelの作成

##### Entity Framework の コード ファースト開発

データ構造を表現するPOCO (Plain Old Clr Object: 特別なクラスやインターフェイスを継承していないクラス(のオブジェクト)) と
POCOを管理する Contextクラスを定義することで、Entity Frameworkが必要なテーブルを生成します。
(開発段階においてDatabaseの操作は全く必要ありません。)

    `Entity Framework 4.1` から提供された機能です。

詳細については [Entity Framework (EF) の概要](http://msdn.microsoft.com/ja-jp/data/ee712907) を参照してください。

##### Todoクラスの作成

* `Models`を右クリックし、「追加」→「クラス」を選択します。

![クラスの追加-1](./images/WS000012.JPG)

* 名前を`Todo.cs`とし、「追加」をクリックします。

![クラスの追加-2](./images/WS000013.JPG)

`Todo.cs`

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebApplication1.Models
{
    /// <summary>
    /// ToDoモデル
    /// </summary>
    public class Todo
    {
        public int id { get; set; }
        [DisplayName("概要")]
        public string summary { get; set; }
        [DisplayName("詳細")]
        public string detail { get; set; }
        [DisplayName("期限")]
        public DateTime limit { get; set; }
        [DisplayName("完了")]
        public bool done { get; set; }
    }
}
```

`DisplayName`に表示名を設定します。
Viewで項目が表示される時に、ここに設定した文言が使用されます。

------

`[DisplayName("概要")]`など、プロパティの上に記述されたカギカッコで括られた記述は
[属性(Attributes あるいは Annotation)](https://msdn.microsoft.com/ja-jp/library/z0w1kczw.aspx)と呼ばれるものです。

* 参考
  - [Code First のデータ注釈](https://msdn.microsoft.com/ja-jp/data/jj591583.aspx)
  - [System.ComponentModel.DataAnnotations 名前空間](https://msdn.microsoft.com/ja-jp/library/system.componentmodel.dataannotations(v=vs.110).aspx)

------


##### TodoesContextクラスの作成

* `Models`を右クリックし、「追加」→「クラス」を選択します。
* 名前を`TodoesContext.cs`とし、「追加」をクリックします。

`TodoesContext.cs`

```cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TodoesContext : DbContext
    {
        public DbSet<Todo> Todoes { get; set; }
    }
}
```

Contextクラスは先ほど作成した POCO とデータベースを繋げる役割を果たします。  
Todoのコレクションを管理するので、`Todoes`という`DbSet<Todo>`を定義します。

`<Todo>` は`DbSet`に格納するクラスを表します。  
`Todoes` にはデータベースから取得した `Todo` のコレクション (配列のようなもの) です。

* [DbSet<TEntity> クラス](https://msdn.microsoft.com/ja-jp/library/gg696460(v=vs.113).aspx)

------


#### ControllerとViewの作成

##### 共通レイアウトの作成

* `Views`を右クリックし、「追加」→「MVC 5 レイアウト ページ (Razor)」 を選択します。

![レイアウトページの追加](./images/WS000008.JPG)

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

------

##### Razorについて

`Razor` は HTMLにC#/VBのコードを埋め込むための仕組み (ビューエンジン) です。  
`@`で始まる箇所がサーバーサイドで実行され、クライアントに生成したHTMLが返されます。

------

##### Bootstrapについて

> BootstrapはWebサイトやWebアプリケーションを作成するフリーソフトウェアツール集である。 タイポグラフィ、フォーム、ボタン、ナビゲーション、その他構成要素やJavaScript用拡張などがHTML及びCSSベースのデザインテンプレートとして用意されている。

[Bootstrap](http://getbootstrap.com/)

------

##### Controllerの作成

スキャフォールディング(Scaffolding、「骨組み」「足場」という意味)によって、
Create（作成）、Read（参照）、Update（更新）、Delete（削除）のような定型的なコードの骨組みを自動生成できます。

* `Controllers`を右クリックし、「追加」→「コントローラー」を選択します。

![コントローラーの追加-1](./images/WS000009.JPG)

* 「Entity Framework を使用した、ビューがある MVC 5 コントローラー」を選択し、「追加」をクリックします。

![コントローラーの追加-2](./images/WS000010.JPG)

* 以下のように入力します。
  - モデル クラス: `Todo`
  - データ コンテキスト クラス: `TodoesContext`
  - レイアウトページの使用: `~/Views/_LayoutPage1.cshtml`
  - コントローラー名: `TodoesController`

![コントローラーの追加-3](./images/WS000011.JPG)


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

`App_Start/RouteConfig.cs` を修正し、デフォルトのページを
ToDoの一覧ページに変更します。

`routes.MapRoute`メソッドの`defaults`引数にデフォルトの設定を定義します。

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

##### ルーティング

ルーティングとは、リクエストURIに応じて処理を受け渡し先を決定することを言います。

`ASP.NET MVC`ではクライアントからの要求を受け取ると、`RouteConfig.cs`の内容を元に
呼び出すべきコントローラー/アクションを決定します。

`routes.MapRoute`メソッドの`url`引数がルーティングの定義です。

例えば `http://localhost/Todoes/Details/3` というリクエストが来た場合、
`url`の定義にしたがって`TodoesController`の`Details`メソッドに`id=3`を引数に与えて呼び出します。

また、`defaults`でデフォルトのコントローラー、アクションを指定しているので、`http://localhost/` という
リクエストが来た場合は `http://localhost/Todoes/Index`というリクエストとして処理されます。


#### デバッグ実行

F5キーを押して、デバッグ実行を行います。
ブラウザが起動し、一覧ページが表示されます。

![Index](./images/WS000014.JPG)

ToDoの追加、変更、削除が行えることを確認します。

![Details](./images/WS000015.JPG)

------

#### ソース解説

##### Controller

* ポイント
  - コントローラークラスの名前は必ず`Controller`で終わる必要があります。
  - コントローラーは`Controller`クラスを継承します。
  - 具体的な処理を記述するのは *アクションメソッド*
  - アクションメソッドの戻り値は `ActionResult`オブジェクト

コードの細部について、順に解説します。

* [TodoesController.cs](https://github.com/Kazunori-Kimura/introduction-to-asp-dot-net/blob/master/projects/step2/WebApplication1/WebApplication1/Controllers/TodoesController.cs)

```cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TodoesController : Controller
    {
        private TodoesContext db = new TodoesContext();
```

コントローラーのプライベートな変数として、Modelに作成した`TodoesContext`を保持しています。

データベースへのアクセスは`TodoesContext`を介して行います。

```cs
        // GET: Todoes
        public ActionResult Index()
        {
            return View(db.Todoes.ToList());
        }
```

`Index`メソッドの定義です。
[View](https://msdn.microsoft.com/ja-jp/library/dd492930(v=vs.118).aspx) メソッドは
アクションメソッドに対応した View を元に `ViewResult` ( `ActionResult` を継承した、Viewを表示するためのクラス)を返します。

ここでは、`views/Index.cshtml` に すべてのTodoをListに格納したオブジェクトを渡して生成される結果を返しています。


```cs
        // GET: Todoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }
```

`Details`メソッドの定義です。

`int?` は `int` の *Nullable型* です。  
通常の `int` は `null` を設定できませんが、Nullable型は `null` が許容されます。

`RouteConfig.cs` の `defaults` の定義により、`id`は省略可能です。  
`id` が省略された場合、`Details`の引数 `id` には `null` が設定されます。

`id` が `null` の場合は要求されたURLが正しくないので、
`BadRequest`を返しています。

`Todo todo = db.Todoes.Find(id);` はデータベースから `id` が一致するデータをひとつ取り出します。

一致するデータが存在しない場合は `Find` から `null` が返ってくるので、
`NotFound`を返しています。

一致するデータが存在すれば、それを`View`にセットして返します。


```cs
        // GET: Todoes/Create
        public ActionResult Create()
        {
            return View();
        }
```

新規登録時は `View` を表示するだけです。

```cs
        // POST: Todoes/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,summary,detail,limit,done")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }
```

`[HttpPost]`は `http` の `POST`メソッドでリクエストがあった場合に呼び出されるアクションメソッドを表す属性です。

`ValidateAntiForgeryToken` は *クロスサイト・リクエスト・フォージェリ攻撃* を防ぐための記述です。
* TODO: 詳細を記載する

[Bind](https://msdn.microsoft.com/ja-jp/library/system.web.mvc.bindattribute(v=vs.118).aspx) は POSTされたデータを `Todo`モデルに紐付けます。

`ModelState.IsValid` は入力チェックがOKかどうかを判定します。  
例えば、`DateTime`型の `limit`に日付以外の値がセットされている場合は `IsValid` が `false`となるため
登録処理が行われません。

更新処理の中身はそのままの意味ですが、

- `Add` でPOSTされたデータを `DbSet` に登録し
- `SaveChanges` で `DbSet` の変更をデータベースに反映します。
- `RedirectToAction` は 指定されたアクションメソッドに転送します。


```cs
        // GET: Todoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,summary,detail,limit,done")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Todoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Todo todo = db.Todoes.Find(id);
            db.Todoes.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
```

------

<br>
<br>

`ASP.NET MVC 5` と `Entity Framework 6` を活用して、非常にシンプルなWebアプリケーションを、簡単・簡潔に作成する手順について解説しました。

ほとんどコーディングを行わずに、基本的な CRUD を行うアプリケーションが作成できることに驚かれたと思います。

また、デバッグ実行したブラウザで `F12` キーを押し、開発者ツールを起動すると分かりますが
生成されるHTMLは見通しがよく、`JavaScript`や`CSS`での操作が容易です。

生成されるViewは`Bootstrap`や`jQuery`を使用することを想定した作りになっていますので
特別な対応を行わなくても、ある程度見栄えのするアプリケーションが作成できます。
