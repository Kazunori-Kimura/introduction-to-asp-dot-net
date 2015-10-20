# Todoアプリを作成する

Login画面とHome画面の2画面構成とします。

## Login画面の作成

* LoginControllerの追加

* Indexメソッド

```cs

```

とりあえず、ログインボタンを押されたら何もチェックせず `Home/Index` に遷移する

<br><br>

* LoginViewの追加

```html
<!DOCTYPE html>
<html lang="ja">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>KnockoutTodo</title>
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css">
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap-theme.min.css">
  <link rel="stylesheet" href="base.css">
  <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
</head>
<body class="login">
  <div class="container">
    <form class="form-signin">
      <h2 class="form-signin-heading">Please sign in</h2>
      <label for="inputUser" class="sr-only">User Name</label>
      <input type="text" id="inputUser" class="form-control"
        placeholder="User Name" required autofocus>
      <label for="inputPassword" class="sr-only">Password</label>
      <input type="password" id="inputPassword" class="form-control"
        placeholder="Password" required>

      <button class="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
    </form>
  </div>
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
body.login {
  padding-top: 40px;
  padding-bottom: 40px;
  background-color: #eee;
}

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

## Home画面の作成

まずは HTMLだけを先に作成し、雰囲気を確認します。

`Layout`

```html
<!DOCTYPE html>
<html lang="ja">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>@ViewBag.Title</title>
  @System.Web.Optimization.Styles.Render("~/bundle/style")
  <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
</head>
<body>

  <nav class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
      <div class="navbar-header">
        <a class="navbar-brand" href="#">KnockoutTodo</a>
      </div>
      <button class="btn btn-link navbar-btn navbar-right">ログアウト</button>
    </div>
  </nav>

  <div class="container">
    @RenderBody()
  </div>
  @System.Web.Optimization.Scripts.Render("~/bundle/script")
</body>
</html>
```

画面上部のナビゲーションバーを設定します。

また、コンテンツ領域の幅を設定するために `class="container"` を設定します。

<br><br>

`Index`

```html

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_LayoutPage1.cshtml";
}

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
```

デバッグ実行して画面のイメージを確認します。

<br><br>

## Knockoutの実装

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

### ViewModelの作成

```js
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

### エントリポイントの作成

```js
// エントリポイント
$(function(){
  ko.applyBindings(new AppViewModel());
});
```
