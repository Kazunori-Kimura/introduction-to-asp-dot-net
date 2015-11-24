# Todoアプリを作成する

ひとまず、クライアントサイドだけで動作する Todoアプリ を作成します。

## Home画面の作成

まずは HTMLだけを先に作成し、雰囲気を確認します。

*bootstrap* を使用するので、Layoutとスタイルシートを少し修正しておきます。


`Views/Shared/_LayoutPage1.cshtml`

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

* metaタグの追加
* 不要なdivタグの削除

<br>

`Content/base.css`

```css
body {
  margin-top: 70px;
}
```

* 上部にナビゲーションバーを配置するので、マージンを設けます。

<br>

つづいて、HTMLをゴリゴリ書いていきます。

ToDoの中身にはとりあえずダミーのデータを設定しておきます。

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

それでは、JavaScriptを実装して動きを付けていきましょう。

<br><br>

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

<br><br>

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

`observableArray` は配列を監視し、要素が追加/削除されると *View* に反映されます。

ここでは、Todoのリストを `observableArray` とし、追加・削除されると
画面左側のTodoリストに反映されるように実装していきます。

<br><br>

### エントリポイントの作成

```js
// エントリポイント
$(function(){
  ko.applyBindings(new AppViewModel());
});
```

HTMLが読み込まれたタイミングで、ViewModel を body に紐付けています。

<br>

### Todoリストの表示

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

* `foreach`

繰り返し処理の構文です。  
`foreach`の内側では、カレントのスコープが各アイテムになります。

ここでは、`foreach: todoList` としているので
`AppViewModel` の `todoList` の中身を一つづつ取り出し、処理します。

また、`todoList` は `observableArray` なので、要素が追加/削除されるたびに
`foreach` が呼び出され、`todoList` の内容がViewに反映されます。

<br><br>

## 2. 選択したTodoを編集フォームに表示する

編集フォームのデータは、登録ボタンをクリックしたタイミングで反映させたいので
入力内容が即時反映されないように、選択されたTodoをコピーし、別のTodoModelのインスタンスに格納します。

登録時は、別のインスタンスから該当のインスタンスに値を戻します。

ViewModelにプロパティとメソッドを追加します。

```js
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
```

<br>

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
AppViewModel に定義したメソッドを呼び出したいのでこのような記述になります。

`click` で指定したメソッドの引数には *バインディング・コンテキスト* が渡されます。

> [バインディング・コンテキスト](http://kojs.sukobuto.com/docs/binding-context)  
> バインディング・コンテキストは、バインディングから参照できるデータをもつオブジェクトです。 バインディングの適用にあたって、Knockout は自動的にバインディング・コンテキストの階層を作成・管理します。 階層のルートは、ko.applyBindings(viewModel) に渡した viewModel です。 また、with や foreach などのフロー制御バインディングを使う度に、 ネストされた ViewModel データを参照する子コンテキストが作成されます。

今回は `foreach` 内で `click` を定義したので、メソッドの引数には配列の各要素が渡されます。


<br>

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

指定された要素が `true` と判定されるような場合、該当要素が表示されます。  
逆に、指定された要素が `false` と判定されるような場合(`boolean`の`false`、`string`の`""`、`number`の`0`、`null`、`undefined`)、該当要素は非表示となります。

<br>

* with

with バインディングは新たな バインディング・コンテキスト を作成します。

ここでは、編集フォーム内の バインディング・コンテキスト を `selectedItem` に変更しています。

<br>

* value

関連付けられた DOM エレメントの値と ViewModel のプロパティーをリンクさせます。
`<input>` や `<select>`, `<textarea>` などのフォーム部品で使用します。

* checked

ViewModel のプロパティとチェックボックス や ラジオボタン などのチェックできるフォーム部品をリンクします。


<br><br>

## 3. 選択中の項目を強調する

現在編集しているTodoがどれなのか分かりやすいように、選択されている要素の背景色を青にします。

bootstrap の active クラスを設定するだけで、青地に白文字で表示されるようになりますので
knockout では 選択要素の aタグに activeクラスをセットするように実装します。

<br><br>

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

<br>

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

`css: { active: $root.isActive($data) }` とした場合、 `$root.isActive($data)` が
`true` となった場合のみ、DOM要素に `active` クラスをセットします。

<br>

* $data

`foreach` でループしている時の「現在のアイテム」になります。

`$data` に別名を付けることもできます。

`foreach: { data: items, as: 'item' }` とすると、`$data` の代わりに `item` で
各要素を参照できます。

`foreach` をネストする場合は別名を付けたほうが分かりやすいでしょう。

<br><br>

## 4. 追加ボタンクリック時の処理

編集フォームに空のTodoをセットします。

ViewModelに以下のメソッドを追加します。

```js
/**
 * 新しいTodoの入力フォームを表示する
 */
self.addTodo = function () {
  self.selectedItem(new ToDoModel());
};
```

<br>

追加ボタンにメソッドを紐付けます。

```html
<button class="btn btn-primary btn-info btn-lg btn-block"
  data-bind="click: $root.addTodo">
  <span class="glyphicon glyphicon-plus"></span> 新しいToDoを追加
</button>
```

<br>

追加時には削除ボタンが使用できないように非表示とします。

selectedItem の id が 0 の場合は追加、  
selectedItem の id が 0 以外の場合は更新と判断します。


```js
/**
 * 削除が可能かどうか
 * @return {boolean}
 */
self.isDeletable = function () {
  return self.selectedItem().id() != 0;
};
```

<br>

非表示となるように、 visibleバインディングを設定します。

```html
<button class="btn btn-danger"
  data-bind="visible:$root.isDeletable()">
  <span class="glyphicon glyphicon-trash"></span>
  削除
</button>
```


<br><br>

## 5. キャンセルボタンクリック時の処理

キャンセルをクリックした時は selectedItem に `null` をセットします。

```js
/**
 * キャンセルボタンのクリック
 */
self.cancelEdit = function () {
  // 編集フォームを閉じる
  self.selectedItem(null);
};
```

<br>

キャンセルボタンにメソッドを紐付けます。

```html
<button class="btn btn-default" data-bind="click: $root.cancelEdit">
  キャンセル
</button>
```

<br><br>

## 6. 登録ボタンクリック時の処理

selectedItem の id が 0 の場合は追加、
selectedItem の id が 0 以外の場合は更新なので、  
id の値に応じて処理を呼び分けます。

```js
/**
 * 登録ボタンのクリック
 */
self.registTodo = function () {
  var item = self.selectedItem();
  if (item.id() == 0) {
    addItem(item);
  } else {
    updateItem(item);
  }

  // 編集フォームを閉じる
  self.selectedItem(null);
};

/**
 * Todoを登録する
 */
function addItem (todoItem) {
  // リストに登録されている末尾の要素のIDを+1する
  var len = self.todoList().length;
  var newId = self.todoList()[len - 1].id() + 1;
  // idを設定
  todoItem.id(newId);
  // リストに追加
  self.todoList.push(todoItem);
}

/**
 * Todoを更新する
 */
function updateItem (todoItem) {
  var len = self.todoList().length;
  for (var i=0; i<len; i++) {
    var item = self.todoList()[i];
    if (todoItem.id() == item.id()) {
      // Todoを更新
      item.summary(todoItem.summary());
      item.detail(todoItem.detail());
      item.limit(todoItem.limit());
      item.done(todoItem.done());

      break;
    }
  }
}
```

observableArray は通常の配列のように `length` で要素数を取得したり
`push` で要素を追加できます。

<br>

```html
<button class="btn btn-primary" data-bind="click: $root.registTodo">
  <span class="glyphicon glyphicon-floppy-disk"></span>
  登録
</button>
```

登録ボタンをクリックしたらメソッドを呼び出すように、clickバインディングで紐付けます。

<br>

### 補足: JavaScriptで擬似的な public / private

`self.` から始まるプロパティ、メソッドは *public* だと考えてください。

`var` や `function` から始まるプロパティ、メソッドは *private* です。
ViewModelの外から参照することはできません。

これも JavaScript でよく使用されるテクニックのひとつです。

<br><br>

## 7. 削除ボタンクリック時の処理

observableArray の `remove` メソッドで、現在選択されている要素を削除します。

```js
/**
 * 削除ボタンのクリック
 */
self.deleteTodo = function () {
  self.todoList.remove(function(item) {
    return item.id() == self.selectedItem().id();
  });

  // 編集フォームを閉じる
  self.selectedItem(null);
};
```

`todoList` の各要素に対して `remove` メソッドで指定された `function` が実行され、
`true` となった要素が削除されます。

<br>

```html
<button class="btn btn-danger"
  data-bind="visible:$root.isDeletable(), click: $root.deleteTodo">
  <span class="glyphicon glyphicon-trash"></span>
  削除
</button>
```

clickバインディングで紐付けます。

<br><br>

-------

## おまけ: モーダルダイアログの表示

登録、削除が完了したらモーダルダイアログでメッセージを表示するように実装します。

bootstrap の modalダイアログを利用します。

```html
<div class="modal fade" id="dialog" data-bind="with: dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title" data-bind="text: title"></h4>
      </div>
      <div class="modal-body">
        <p class="lead" data-bind="text: message"></p>
      </div>
      <div class="modal-footer">
        <button class="btn btn-primary" data-dismiss="modal">OK</button>
      </div>
    </div>
  </div>
</div>
```

DialogModel を定義します。

```js
/**
 * modal dialog model
 */
var DialogModel = function () {
  var self = this;

  self.id = '#dialog';
  self.title = ko.observable('');
  self.message = ko.observable('');

  /**
   * ダイアログの表示
   * @param {object}
   */
  self.show = function (opts) {
    // 初期値
    var def = {
      title: '',
      message: ''
    };
    $.extend(def, opts);

    self.title(def.title);
    self.message(def.message);

    // モーダルダイアログの表示
    $(self.id).modal('show');
  };
  /**
   * ダイアログの非表示
   */
  self.hide = function () {
    $(self.id).modal('hide');
  };
};
```

AppViewModel で DialogModel を生成します。

```js
var AppViewModel = function () {
  var self = this;

  // Todoリスト
  self.todoList = ko.observableArray([ ... ]);

  // 選択されたTodo
  self.selectedItem = ko.observable();

  // モーダルダイアログ
  self.dialog = new DialogModel();

  /* ~~ 省略 ~~ */
};
```

登録、削除後にモーダルダイアログを表示するよう
処理を修正します。

```js
/**
 * 登録ボタンのクリック
 */
self.registTodo = function () {
  var item = self.selectedItem();
  if (item.id() == 0) {
    addItem(item);
  } else {
    updateItem(item);
  }

  // 編集フォームを閉じる
  self.selectedItem(null);

  self.dialog.show({
    title: '登録完了',
    message: '登録が完了しました。'
  });
};

/**
 * 削除ボタンのクリック
 */
self.deleteTodo = function () {
  self.todoList.remove(function(item) {
    return item.id() == self.selectedItem().id();
  });

  // 編集フォームを閉じる
  self.selectedItem(null);

  self.dialog.show({
    title: '削除完了',
    message: '削除が完了しました。'
  });
};
```


<br><br>

------

以上で、クライアントサイドだけで動作する Todoアプリが完成しました。

当然、データを保存する機能がないため、ブラウザをリロードすると更新した内容が失われます。

次回は Ajax で Web API を呼び出すことで クライアントサイドとサーバーサイドの処理を連携させ、
Todoアプリを完成させます。

<br><br>
