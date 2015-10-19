# 簡単な Knockout.js の例

## Hello, World!

まず、非常に簡単な `knockout.js` のアプリをサンプルとして
`MVVM` の概要について解説します。

`index.cshtml`

```html

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_LayoutPage1.cshtml";
}

<h2>Index</h2>

<input type="text"
       placeholder="お名前"
       data-bind="value: user.name, valueUpdate: 'afterkeydown'" />

<p>こんにちは、<span data-bind="text: user.name"></span>さん！</p>

```

HTMLの要素と `knockout.js` の処理を紐付けるには、`data-bind` という属性を使用します。

```js
data-bind="value: user.name, valueUpdate: 'afterkeydown'"
```

これは、以下のような意味になります。

* `value` に `user.name` をセットする
* 'afterkeydown' (キーの押下時) に更新する
  - `valueUpdate: 'afterkeydown'` を指定しない場合、テキストボックスからフォーカスが外れた場合に更新されます。

<br><br>

`base.css`

```css
body {
    margin: 40px;
}
```

<br><br>

`app.js`

```js
// app.js
$(function () {
    // Model
    var UserModel = function UserModel() {
        var self = this;
        self.name = ko.observable("名無し");
    };

    // ViewModel
    var AppViewModel = function AppViewModel() {
        var self = this;
        self.user = new UserModel();
    };

    // bind
    ko.applyBindings(new AppViewModel());
});
```

* `UserModel`が *MVVM* の *Model* です。
* `AppViewModel`が *MVVM* の *ViewModel* です。
  - `AppViewModel` は 変数 `user` に *Model* `UserModel` のインスタンスを一つ保持しています。
* `ko` は `knockout.js`のオブジェクトです。
* `ko.applyBindings(viewModel);` で `body`と`ViewModel`を紐付けます。
  - つまり、*MVVM* の *View* と *ViewModel* の紐付けを行っています。
  - HTML内の各要素に`data-bind`属性を設定することで、*ViewModel* と *View* の紐付けの詳細を定義します。
* `observable` は該当の変数を`knockout.js`の監視対象とします。
  - ここでは、`UserModel`の`name`の値が変わるたびに *ViewModel* を介して *View* に反映されます。

-> テキストボックスの値を変更すると、ほぼリアルタイムで `span`タグ内のテキストに反映されます。

<br>

------

<br>

ちなみに...

```js
var varName = function funcName(){ };
```

というのは `JavaScript`で`class` (のようなもの) を定義する際の構文です。

また、`var self = this;` というのは、`JavaScript`でよく使用するテクニックです。

JavaScript では文脈によって `this` の中身が異なります (例えば、button押下時のイベントから呼び出された場合、
`this` には button のオブジェクトが入っています) が
`class`内部ではどのような状況であっても自分自身を取得する方法が欲しいので、
`new` した際に `this` を `self` という変数に格納しています。

以降、`self`変数を参照することで、どのような文脈からの呼び出しであっても `self` は自分自身を指します。

<br><br>
