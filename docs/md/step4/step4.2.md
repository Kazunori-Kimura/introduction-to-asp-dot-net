# 簡単な Knockout.js の例

## Hello, World!

非常に簡単な `knockout.js` のアプリの作成を通して、`MVVM` の概要について解説します。

<br><br>

### View の作成

まず、*MVVM* の *V* にあたる、*View* を作成します。

`index.cshtml` を以下のように修正します。

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

<br><br>

HTMLの要素と `knockout.js` の処理を紐付けるには、`data-bind` という属性を使用します。

<br>

```js
data-bind="value: user.name, valueUpdate: 'afterkeydown'"
```

<br>

これは、以下のような意味になります。

* `value` に `user.name` をセットする
* 'afterkeydown' (キーの押下時) に更新する
  - `valueUpdate: 'afterkeydown'` を指定しない場合、テキストボックスからフォーカスが外れた場合に更新されます。

<br>

少しスタイルシートを修正します。

`base.css`

```css
body {
    margin: 40px;
}
```

<br><br>

### Modelの定義

*MVVM* の *M* にあたる、*Model* を作成します。

`app.js`

```js
// app.js
/**
 * User Model
 */
var UserModel = function UserModel() {
    var self = this;
    self.name = ko.observable("名無し");
};
```

* `ko` は `knockout.js`のオブジェクトです。
* `observable` は該当の変数を`knockout.js`の監視対象とします。
  - ここでは、`UserModel`の`name`の値が変わるたびに *View* に反映されます。

<br>

------

ちなみに...

<br>

```js
var varName = function funcName(){ };
```

というのは `JavaScript`で`class` (のようなもの) を定義する際の構文です。

また、`var self = this;` というのは、`JavaScript`でよく使用するテクニックです。

JavaScript では文脈によって `this` の中身が異なります (例えば、button押下時のイベントから呼び出された場合、
`this` には button のオブジェクトが入っています) が
`class`内部ではどのような状況であっても自分自身を取得する方法が欲しいので、
`new` した際に `this` を `self` という変数に格納しています。

`self`変数を参照することで、どのような文脈からの呼び出しであっても `self` は自分自身を指します。

<br>

------

<br><br>

### ViewModel の定義

つづいて、 *MVVM* の *VM* にあたる、*ViewModel* を作成します。

```js
/**
 * ApplicationViewModel
 */
var AppViewModel = function AppViewModel() {
    var self = this;
    self.user = new UserModel();
};
```

* `AppViewModel` は 変数 `user` に *Model* `UserModel` のインスタンスを一つ保持しています。

<br><br>

### View と ViewModel を紐付ける

最後に、*View* と *ViewModel* を紐付けます。

```js
$(function () {
    ko.applyBindings(new AppViewModel());
});
```

<br>

* `ko.applyBindings(viewModel);` で `body`と`ViewModel`を紐付けます。
  - HTML内の各要素に`data-bind`属性を設定することで、*ViewModel* と *View* の紐付けの詳細を定義します。

<br>

デバッグ実行して動作確認してください。

テキストボックスの値を変更すると、ほぼリアルタイムで `span`タグ内のテキストに反映されます。

<br>

------

<br>

### 補足: ViewModelについて

今回の例は *ViewModel* がひとつですが、メニュー部分とコンテンツ部分で *ViewModel* を分割するような場合、
以下のように記述することが可能です。

```js
// メニュー部分
ko.applyBindings(new MenuViewModel(), document.getElementById("menu"));
// コンテンツ部分
ko.applyBindings(new ContentViewModel(), document.getElementById("content"));
```

<br>

### 補足: バインディングについて

`data-bind="value: name"` といった、DOM要素 と ViewModel のプロパティ・メソッドを紐付ける機能を
*バインディング* と呼びます。

knockout.js では 様々なバインディングの機能が用意されています。  
また、*カスタムバインディング* を作成して、独自のバインディングを実装することも可能です。

*バインディング* の詳細については knockout.js のドキュメントを参照してください。

<br>

------

<br>

[次へ](step4.3.html)

<br><br>
