# Connect 2015

2015/11/18 ~ 18 にニューヨークで マイクロソフトの開発者向けイベント *Connect(); // 2015* が
開催されました。

* [Connect(); // 2015](https://channel9.msdn.com/Events/Visual-Studio/Connect-event-2015/)
* [Microsoft Connect (); // 2015 Developer Event Set for November 18-19](http://blogs.msdn.com/b/visualstudio/archive/2015/10/19/microsoft-connect-2015-developer-event-set-for-november-18-19.aspx)

英語の動画を見ても何のことやら分からないので、内容についてまとめてみました。

<br><br>

## トピック

ざっくりと重要そうなトピックを抽出しました。

<br>

### Visual Studio Code のオープンソース化、ベータ版リリース

無償かつマルチプラットフォームなコードエディタである *Visual Studio Code* が
オープンソースとなりました。  
[GitHubで公開](https://github.com/Microsoft/vscode/)されています。

また、あわせてベータ版がリリースされています。

Node.js や C# (ASP.NET) のデバッグ実行もサポートされ、
実は最強のJavaScript開発環境なのではと噂されています。

実際、*AngularJS* は *TypeScript* と *Visual Studio Code* で開発されているそうです。


エクステンションにより機能拡張が可能になり
*Go* や *Pascal*, *F#* など、様々な言語に対応できるようになりました。

エクステンションの導入は *Sublime Text* や *Atom* と似たような使用感みたいです。

<br><br>

### .NET Core と ASP.NET 5 が RC に

Windows上で作成しビルドした ASP.NET 5 の Webアプリケーションを
Ubuntu の中で走っている docker のイメージに展開してそのまま実行させる、というデモをやっていました。

なんか半年前も同じようなデモを見た気がします。

会場では、Linux上で ASP.NET 5 が動くことより
Windows のコマンドプロンプトから Windowsの新しいsshコマンドを使用して
Linuxにアクセスしている事に拍手が起こったみたいです。

<br>

*Go-Liveライセンス* となったので、プロダクトの開発にも安心して使用できる...  
らしいですが、よく分かりません。

<br><br>

### Node.js tools for VS

Visual Studio で Node.jsアプリの開発・デバッグ・Azureへのデプロイが可能になるプラグイン。  
コレ使うなら *Visual Studio Code* 使うと思います。

<br><br>

### デベロッパー向けの無料プログラム *Free Visual Studio Dev Essentials*

* *Visual Studio Community Edition* や *Visual Studio Code* など無料版の *Visual Studio*
* コードリポジトリやビルド、デプロイ、テストなどの機能を提供するクラウドサービス *Visual Studio Team Services* の利用
  - *Visual Studio Online* の名称が変わったらしいです。
* 月額25ドル相当の *Microsoft Azure* の利用
* トレーニングやサポートのためのコンテンツ

...など

登録と利用は無料。

<br><br>

半年前の [build2015](./build2015.html) から順当に前進している感じです。  
サプライズは無いですが、ネガティブな点も無いですね。

*Free Visual Studio Dev Essentials* によって
*Visual Studio Team Services* や *Azure* への間口が広がりましたね。

*Visual Studio Team Services* は *GitHub* よりも優位な点を明確にアピールできれば
面白いんじゃないでしょうか。

グループウェア勉強会で少し見た限り、UIがクソでしたが...

<br><br>
