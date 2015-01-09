README
============

`ASP.NET`の入門者向けドキュメント。

ToDoアプリの開発を例に、`ASP.NET MVC`による開発手順について解説する。

参考
-----------

* [ASP.NET](http://www.asp.net/)
* [ASP.NET入門](http://aspnet.keicode.com/)


------

## コンバートツールについて

* markdownをhtmlに変換する
* 指定されたtemplateに変換で得られたhtmlを埋め込む
* 出力先はoptionで指定、あるいは設定ファイルで定義

### フォルダ構成

```
  docs/
    images/
    md/
  templates/
    template.ect
  tools/
    npm_modules/
    config.json
    index.js
    package.json
  .gitignore
```

### config.json

```js
{
  "source": "../docs/md",
  "destination": "/dest/path",
  "template": "../templates",
  "info": {
    "project": "Introduction to ASP.NET",
    "copyright": "2015 kazunori.kimura.js@gmail.com"
  }
}
```

引数での指定を優先。

```
node tools/index.js [-source <path>] [-destination <path>] [-template <path>]
```

templateは[ECT](http://ectjs.com/)を使用する。

#### 処理内容

1. markdown を html に変換
2. `ECT`を使用して1.を`template.ect`に埋め込む
3. 生成されたファイルを指定されたフォルダに移動

