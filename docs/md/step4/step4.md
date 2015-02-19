# `ASP.NET Web API`と`knockout.js`によるSingle Page Application開発

## Single Page Application とは？

* 文字通り、*単一ページによるWebアプリケーション* です。
* サーバーとのやりとりは `REST` などを使用して行います。

<br>

### UIの作成

* Webアプリケーションなので、ユーザー インターフェースは `HTML` です。
* 単一ページなので、画面遷移は行われません。
  - サーバーから受信したデータを表示するためには、HTMLの要素 (*D*ocument *O*bject *M*odel, `DOM`) を JavaScript で頻繁に書き換える必要があります。
  - 全ての処理を自分でコーディングすることも可能ですが、通常はJavaScriptフレームワークを使用します。

<br>

#### 代表的な JavaScriptフレームワーク

* [backbone.js](http://backbonejs.org/) : JavaScriptフレームワークの草分け的存在。
* [AngularJS](https://angularjs.org/) : Googleが中心になって開発している。利用者も多い。
* [Sencha](http://www.sencha.com/) : `ExtJS`という名前で、かなり昔からあったライブラリ。きれいなUIが特徴。
* [knockout.js](http://knockoutjs.com/) : Microsoftが中心になって開発している。IE6でも動作する。

上記以外にも大小様々なフレームワークが存在します。

それぞれ守備範囲が異なるので、プロジェクトにあったものを選択する (あるいは、採用したJavaScriptフレームワークの機能を考慮して設計する) 必要があります。

<br>

今回は比較的軽量な `knockout.js` を使用します。

* Ajax通信機能は含まれていないため、 `jQuery` と合わせて使用します。
  - HTMLへのデータ反映は `knockout.js` に任せ、`jQuery` では表示部分を触らないよう考慮して設計します。  
`knockout.js` と `jQuery` のHTML操作が混在するとカオスになります。
* UIのレイアウトには `Bootstrap`を使用します。
  - こちらも表示/非表示の切り替え等は`knockout.js`で行います。

<br>
<br>

------

## アプリ仕様

前回作成した Web API を使用した Todoアプリケーションを作成します。

* 画面を表示すると、Todoの一覧が表示されます。
* `Done`チェックボックスをONにすると、該当Todoがグレーアウトします。
* `Edit`ボタンをクリックすると、該当Todoを編集できます。
  - `Save`ボタンで編集内容が登録されます。
  - `Cancel`ボタンで編集状態が解除されます。 (編集内容は破棄)
* `Add`ボタンで新規Todoを登録します。

### knockout.js の MVVM

`knockout.js`では、以下の様な構成でコーディングします。

* *M*odel : サーバーから受け取ったデータ
* *V*iew : HTML
* *V*iew*M*odel : Modelを保持し、Viewとの仲介を行います。

`MVVM` は `クライアントサイドMVC` と呼ばれる事もあります。


#### Todoリストの項目

* id: Todoを一意に特定する数値。
* summary: 概要。文字列。
* detail: 詳細。文字列。
* limit: 期限。日時。
* done: 完了フラグ。真偽値。

    前回作成した Web API の Todo Modelと同じ内容です。
