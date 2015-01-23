
# 3. `ASP.NET Web API`によるREST API開発

## Web API とは？

* `API`: Application Programming Interface
  - あるプログラムが提供する機能を、 *外部の別のプログラム* から呼び出して利用するための手法


プログラムからのHTTPリクエストに対し、`XML` や `JSON` などのデータを返すWebアプリケーションを *Web API* と呼びます。

<br>

### Web API のメリットは？

* HTTP が使用できるものであれば、どのようなクライアントからも利用できる
  - Webブラウザ向けのシステムをモバイル向けに作り直す、といった時も Web API は変更しなくてよい
  - 複数のWeb APIを組み合わせたシステムを開発することも可能 (マッシュアップ)
* クライアントサイドのプログラムと完全に分断して開発できる
  - システムの役割が明確になり、より良い設計になる (ことが期待できる)

<br>

## `REST` とは？

* Web API のソフトウェアアーキテクチャのスタイルのひとつ。
  - REST の原則に従っているシステムは `RESTful`なシステム、 といわれます。
  - REST をとても熱心に支持する *RESTafarians* と呼ばれる人たちがいます。
このような人たちに、適当な設計のWebアプリケーションを「REST APIです」と紹介すると、すごいマサカリが飛んできます。


### 原則

* ステートレスなクライアント/サーバープロトコル

セッションやクッキーによるセッション状態の管理を行わず、
一度のリクエスト/レスポンスで問い合わせが完了する。


* `URI (Uniform Resource Identifier)` でリソースを一意に識別する

`http://{webapi}/user/1/task/5` のような URL にリクエストを投げると
`userId`が`1`のユーザーが持っている`taskId`が`5`のタスク情報を返すようなイメージ。


* HTTPメソッドでリソースを操作する

| HTTPメソッド | リソースの操作 |
| ------ | ------------------ |
| GET    | リソースの取得 (Read)   |
| POST   | リソースの追加 (Create) |
| PUT    | リソースの更新 (Update) |
| DELETE | リソースの削除 (Delete) |


* 操作の結果は HTTPのステータスコードで返す

| ステータスコード | 意味 |
| --- | ---------- |
| 200 | OK         |
| 201 | Created    |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden   |
| 404 | Not Found   |
| 500 | Internal Server Error |


<br>

------

<br>

## Getting Started with ASP.NET Web API

参考: [Getting Started with ASP.NET Web API 2](http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)

### 作成するWeb APIの仕様

* ToDo管理API



---

### クライアント環境のインストール

今回はクライアント側のプログラムは作成しないので、Web APIに問い合わせを行う
クライアントアプリケーションを用意します。

HTTPが喋れるツールであれば何でも良いのですが、今回は `cURL` を使用します。


#### cURL

> cURL（カール）は、さまざまなプロトコルを用いてデータを転送するライブラリとコマンドラインツールを提供するプロジェクトである。
> 
> [cURL](http://ja.wikipedia.org/wiki/CURL)


#### Chocolatey

自分で`cURL`のバイナリを探してきてダウンロード・インストールするのは面倒臭いので、
パッケージ管理ツールを使用します。

[Chocolatey](https://chocolatey.org/) は `apt-get` のようなパッケージ管理ツールです。
(`Chocolatey` の背後では NuGet が動いているようです。)

コマンドプロンプトを `管理者として実行` し、以下のコマンドをコピー&ペーストします。

```bat
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
```

#### cURLのインストール

`Chocolatey` を使用して、`cURL`をインストールします。

```bat
choco install curl
```

#### 動作確認

`www.example.com`のhtmlを取得してみます。

```bat
>curl http://www.example.com
<!doctype html>
<html>
<head>
    <title>Example Domain</title>

    <meta charset="utf-8" />
    :
```

きちんとhtmlが返ってくれば、準備完了です。


