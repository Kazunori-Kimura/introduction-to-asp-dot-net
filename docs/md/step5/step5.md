# `ASP.NET Web API`と`knockout.js`によるSingle Page Application開発 (2)

[前回](../step4/step4.3.html) は *Knockout.js* を使用して
ブラウザだけで動作するTodoアプリケーションを作成しました。

今回はサーバー側の処理を実装し、前回作成したフロントエンドと結合します。

<br><br>

## Web API の作成

[以前](../step3/step3.html) 取り上げた *ASP.NET Web API* の開発手順と全く同じです。

<br><br>

### (1) EntityFrameworkを追加

* プロジェクトを右クリック→「NuGet パッケージの管理」
* *EntityFramework* を選択して「インストール」

<br><br>

### (2) Modelクラスの追加

* `Models`を右クリック→「追加」→「クラス」を選択します。
* 名前を `Todo.cs` として「追加」します。

`Todo.cs`

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class Todo
    {
        public int id { get; set; }

        [Required]
        public string summary { get; set; }

        public string detail { get; set; }

        public DateTime limit { get; set; }

        public bool done { get; set; }
    }
}
```

* 続いて `TodoesContext.cs` を追加します。

`TodoesContext.cs`

```cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class TodoesContext : DbContext
    {
        public DbSet<Todo> Todoes { get; set; }
    }
}
```

ここまで作成したら、一旦ソリューション全体をビルドしてください。

<br><br>

### (3) Controllerクラスの追加

* Controllersを右クリック→「追加」→「コントローラー」を選択
* `Entity Framework を使用したアクションがある Web API 2 コントローラー` を選択して「追加」をクリック
* モデル クラスに `Todo`、データ コンテキスト クラスに `TodoesContext` を選択して「追加」をクリック
  - コントローラー名は自動で `TodoesController` となるはず

以下の様なメソッドが定義されます。

| 操作 | URL | Method | 説明 |
| ---- | ---- | ---- | ---- |
| Create | /api/Todoes/ | post | 1件作成 |
| Read | /api/Todoes/{id} | get | 指定したTodoを取得 |
| ReadAll | /api/Todoes/ | get | 全てのTodoを取得 |
| Update | /api/Todoes/{id} | put | 指定したTodoを更新 |
| Delete | /api/Todoes/{id} | delete | 指定したTodoを削除 |

<br>

これで、サーバーサイドの実装は完了しました。

<br><br>

## フロントエンドとの結合

今回作成したサーバーサイドの Web API と [前回](../step4/step4.3.html) 作成した
フロントエンドをくっつけます。

<br>

### (1) 初期表示時にTodoリストを表示する

画面の読み込み完了後、非同期にて、*GET* メソッドで `/api/Todoes/` に問い合わせを行います。

サーバーから取得したTodoのリストを *observableArray* にセットします。

`Scripts/app.js`

```js
// app.js

/**
 * Todo Model
 * @param 初期値 {object}
 */
var ToDoModel = function (params) { ... };

/**
 * modal dialog model
 */
var DialogModel = function () { ... };

/**
 * ViewModel
 */
var AppViewModel = function () {
    var self = this;

    /* ~~ 省略 ~~ */

    /**
     * Todoをすべて取得する
     */
    self.loadList = function () {
        $.ajax({
            url: "/api/Todoes",
            dataType: "json",
            method: "GET"
        })
            .done(function (data) {
                if (data) {
                    // 一旦全削除
                    self.todoList.removeAll();

                    // 取得したTodoをリストにセット
                    $.each(data, function (index, item) {
                        // yyyy-mm-ddThh:mm:ss という形式で送られてくるので、
                        // 不要な "T" 以降をカット
                        item.limit = item.limit.split("T")[0];
                        if (item.limit == "1960-01-01") {
                            // ダミーの日付の場合はブランクに置き換える
                            item.limit = "";
                        }
                        self.todoList.push(new ToDoModel(item));
                    });
                }
            });
    };
};

// エントリポイント
$(function () {
    var app = new AppViewModel();
    ko.applyBindings(app);

    app.loadList();
});
```

日付の処理に少し工夫が必要です。

サーバーから送られてくる値は `yyyy-MM-ddTHH:mm:ss` という形式になっています。
今回は日付のみが必要なので、 不要な部分をカットして表示しています。

<br><br>

### (2) 新しいTodoを追加する / Todoの更新を行う

追加の場合、登録ボタンクリック時に 非同期にて *POST* メソッドで `/api/Todoes/` に問い合わせを行います。

更新の場合、登録ボタンクリック時に 非同期にて *PUT* メソッドで `/api/Todoes/{id}` に問い合わせを行います。

登録に成功したら、フォームを非表示にして Todoリストを再取得します。

```js
/**
 * 登録ボタンのクリック
 */
self.registTodo = function () {
    var item = self.selectedItem();

    // limitが空だとサーバーサイドでエラーとなるため
    // ダミーの日付を登録する
    var model = ko.toJS(item);
    if (model.limit == "") {
        model.limit = "1960-01-01";
    }

    // ajaxのパラメータ
    var params = {
        dataType: "json",
        data: model
    };

    if (item.id() == 0) {
        // Create
        params.url = "/api/Todoes";
        params.method = "POST";
    } else {
        // Update
        params.url = "/api/Todoes/" + item.id();
        params.method = "PUT";
    }

    $.ajax(params)
        .done(function (data) {
            // 編集フォームを閉じる
            self.selectedItem(null);

            // Todoリストを再読み込み
            self.loadList();

            self.dialog.show({
                title: '登録完了',
                message: '登録が完了しました。'
            });
        });
};
```

<br>

*jQuery* の `ajax` メソッドで、入力されたデータを *POST/PUT* します。

*TodoModel* には *observable* のプロパティがいくつかありますが、
これは *Knockout.js* が使用する付加情報が含まれています。

これらの情報はサーバー側で登録処理を行うのに邪魔になるため、 `ko.toJS` メソッドを使用して
純粋な *JavaScriptのオブジェクト* に変換します。

> * `ko.toJS` - この関数はあなたのビューモデルのオブジェクトグラフに対して、各 observable を現在の値に置換した後で複製するため、Knockout に関連したアーティファクトが存在せず、あなたのデータのみを含んだプレーンなコピーを取得できます。

[JSON データの読み込みと保存](http://kojs.sukobuto.com/docs/json-data)

また、`limit` が未指定の場合、サーバーサイドで `DateTime` に変換する際に
エラーとなってしまいます。

空の場合はすごい古い年月をダミー値として登録しておき
表示時にダミー値と一致する場合はブランクに置き換えるようにしました。

<br>

`function addItem`, `function updateItem` はもう使用しないので、削除しておきます。

<br><br>

### (3) リストからTodoを選択すると、詳細を表示する

できるだけ新しい情報を表示するため、リストを選択すると
非同期にて *GET* メソッドで `/api/Todoes/{id}` に問い合わせを行うようにします。

取得したデータを入力フォームに表示します。

```js
/**
 * リストからTodoを選択する
 * @param item {ToDoModel} 選択された項目
 */
self.selectTodo = function (item) {
    $.ajax({
        url: "/api/Todoes/" + item.id(),
        dataType: "json",
        method: "GET"
    })
        .done(function (data) {
            if (data) {
                // yyyy-mm-ddThh:mm:ss という形式で送られてくるので、
                // 不要な "T" 以降をカット
                data.limit = data.limit.split("T")[0];
                if (data.limit == "1960-01-01") {
                    // ダミーの日付の場合はブランクに置き換える
                    data.limit = "";
                }
                self.selectedItem(new ToDoModel(data));
            }
        });
};
```

<br><br>

### (4) Todoの削除を行う

削除ボタンクリックで 非同期にて *DELETE* メソッドで `/api/Todoes/{id}` に問い合わせを行うようにします。

削除完了後、リストを再読み込みします。

```js
/**
 * 削除ボタンのクリック
 */
self.deleteTodo = function () {
    $.ajax({
        url: "/api/Todoes/" + self.selectedItem().id(),
        dataType: "json",
        method: "DELETE"
    })
        .done(function (data) {
            // 編集フォームを閉じる
            self.selectedItem(null);

            // リストを再読み込み
            self.loadList();

            self.dialog.show({
                title: '削除完了',
                message: '削除が完了しました。'
            });
        });
};
```

<br><br>

デバッグ実行して、以下を確認してください。

* Todoの表示・追加・変更・削除が行えること
* ブラウザを一度閉じ、再度アクセスしても前回登録した内容が表示されること

<br><br>

------

*ASP.NET Web API* と *Knockout.js* による *Single Page Application開発* について解説しました。

*ASP.NET* と直接は関係しないために取り上げませんでしたが
実際のアプリケーションには以下の様な機能が必要になってきます。

* クライアントサイドでの入力チェック
* エラーが発生した場合にエラー内容を画面に表示する
* サーバーとの通信中に操作できないように画面をロックする

<br>

*Single Page Application* では *Ajax* による非同期処理を駆使することで
UI/UX の向上が見込まれますが、以下の様な問題点もあります。

* ブラウザで動く *JavaScript* がまだまだ貧弱である
  - 多数のライブラリ間の依存関係を解決する仕組み
  - *MVVM* などのアーキテクチャにしたがってコーディングしても、クラスやモジュールを読み込む仕組み
* 新しい *JavaScript* ライブラリがどんどんリリース / バージョンアップ している
  - どれを使えば良いか...？
* そもそも *JavaScript* 自体が進化していっている
  - *ECMAScript5* -> *ECMAScript2015* -> *ECMAScript2016*


非常に進化のスピードが早い分野なので、
実際に開発する際には新しい情報を収集するように心がけてください。

<br><br>
