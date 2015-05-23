
# Visual Studio CodeによるASP.NETアプリケーション開発

Visual Studio Codeのドキュメントにしたがって、
Mac OS XでASP.NETのアプリケーションを開発する手順について解説します。


* [Building ASP.NET 5 Applications with Visual Studio Code](https://code.visualstudio.com/Docs/ASPnet5)

> Note: ASP.NET 5 and DNX (the .NET Execution Environment) on OS X and Linux are in an early Beta/Preview state. This guide will help you get started but we recommend following the aspnet Home project on GitHub for the latest information.

Mac/Linux版のASP.NET5とDNX(.NET実行環境)はBeta/Previewなので、
最新情報はGitHubのaspnet Home projectを参照してね！という事です。


## ASP.NET 5 and DNX

> ASP.NET 5/DNX is a lean .NET stack for building modern cloud and web apps that run on OS X, Linux, and Windows. It has been built from the ground up to provide an optimized development framework for apps that are either deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions.

ASP.NET 5 / DNXはOS X 、 Linux、およびWindows上で動作する
モダンなクラウドとWebアプリケーションを構築するためのリーン.NETスタックです。

より詳細な説明は以下を参照。

* [Introduction to ASP.NET 5](http://docs.asp.net/en/latest/conceptual-overview/aspnet.html)

## Installing ASP.NET 5 and DNX

Mac OS上に.NET実行環境を構築する手順について。

* [Getting Started with ASP.NET 5 and DNX - os-x](https://github.com/aspnet/home#os-x)

> On OS X the best way to get DNVM is to use Homebrew.

* [Homebrew](http://brew.sh/index_ja.html)

> HomebrewはAppleが用意していないあなたが必要なものをインストールします。

```sh
brew tap aspnet/dnx
brew update
brew install dnvm
```

`.bash_profile`に以下を追加。

```sh
source dnvm.sh
export MONO_MANAGED_WATCHER=disabled
```

追加した後に再読み込みしておくこと。

```sh
source ~/.bash_profile
```

## Installing Visual Studio Code

* [Setting up Visual Studio Code](https://code.visualstudio.com/Docs/setup)

1. Download Visual Studio Code for Mac OS X
2. Double-click on VSCode-osx.zip to expand the contents
3. Drag Visual Studio Code.app to the Applications folder, making it available in the Launchpad
4. Add VSCode to your Dock by right-clicking on the icon and choosing Options, Keep in Dock

If you want to run VSCode from the terminal, append the following to your `.bash_profile` file

```sh
code () {
    if [[ $# = 0 ]]
    then
        open -a "Visual Studio Code"
    else
        [[ $1 = /* ]] && F="$1" || F="$PWD/${1#./}"
        open -a "Visual Studio Code" --args "$F"
    fi
}
```

Now, you can simply type `code .` in any folder to start editing files in that folder.


## Getting Started

> If you don't have an existing ASP.NET DNX application, we recommend using yeoman to scaffold a new one. Presuming you have Node.js installed, simply npm install yeoman and a few supporting tools such as the asp.net generator, grunt, and bower.

### 【意訳】

プロジェクトを新規開発するには、`yeoman`で土台を作成します。  
Node.jsをインストールして、npmでyeomanといくつかのサポートツール
(asp.net generator, grunt, bower)をインストールします。

#### 補足

* [Node.js](https://nodejs.org/)

> Node.js is a platform built on Chrome's JavaScript runtime for easily building fast, scalable network applications. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient, perfect for data-intensive real-time applications that run across distributed devices.

* [npm](https://www.npmjs.com/)

> npm makes it easy for JavaScript developers to share and reuse code, and it makes it easy to update the code that you're sharing.

```sh
npm install -g yo grunt-cli generator-aspnet bower
```

このコマンドでインストールされるのは、以下のツール類になります。

* `yo` : yeoman. Webアプリケーションのひな形を生成するツール。
* `grunt-cli` : タスクランナー。Javaだとant。ビルド等の自動化を行う。
* `generator-aspnet` : yeomanのプラグイン。asp.netのひな形を作成する。
* `bower` : クライアントサイドのライブラリ(jQueryやBootstrapなど)を管理する
パッケージマネージャ。


## Your First ASP.NET 5 Application on a Mac

> [Your First ASP.NET 5 Application on a Mac](http://docs.asp.net/en/latest/tutorials/your-first-mac-aspnet.html)



### Scaffolding your first Application

> From the terminal, run yo aspnet to start the generator. Follow the prompts and pick the Web Application

```sh
yo aspnet
? ==========================================================================
We're constantly looking for ways to make yo better!
May we anonymously report usage statistics to improve the tool over time?
More info: https://github.com/yeoman/insight & http://yeoman.io
========================================================================== Yes
 Y
     _-----_
    |       |    .--------------------------.
    |--(o)--|    |      Welcome to the      |
   `---------´   |   marvellous ASP.NET 5   |
    ( _´U`_ )    |        generator!        |
    /___A___\    '--------------------------'
     |  ~  |
   __'.___.'__
 ´   `  |° ´ Y `

? What type of application do you want to create? Web Application
? What's the name of your ASP.NET application? WebApplication
```

`yo`コマンドで土台を作成したら、生成されたプロジェクトのフォルダに移動して
Visual Studio Codeを起動します。

```sh
cd WebApplication
code .
```

### Your First ASP.NET 5 Application on a Mac

[Your First ASP.NET 5 Application on a Mac](http://docs.asp.net/en/latest/tutorials/your-first-mac-aspnet.html)

まずは必要なパッケージをダウンロードします。

1. Visual Studio Codeで `Command-o`
2. プロンプトが表示されるので、`>d` と入力して `Enter`

NuGetから色々なパッケージがダウンロードされます。
(めっちゃ時間がかかります...)

```
Done, without errors.
Restore complete, 294192ms elapsed
```

### Running Locally Using Kestrel

* 起動

ターミナルで、カレントディレクトリを該当プロジェクトとした状態で
以下のコマンドを実行
```sh
dnx . kestrel
```

* 停止

ターミナルで `Enter` を押下

それでも止まらない場合は

> If simply pressing Enter doesn't work for you, try the following in the terminal window where you're running Kestrel:  
>  1. Hit Ctrl + z to suspend the process.
>  2. Type: kill %1.
>  
> [How to quit ASP.NET Kestrel web server on a Mac](http://stackoverflow.com/questions/25712814/how-to-quit-asp-net-kestrel-web-server-on-a-mac)


### Debug

> Visual Studio Code and ASP.NET 5 are in preview and at this time debugging ASP.NET 5 is not supported in Visual Studio Code (on any platform). Rest assured, we are working hard to bring these experiences to you in the near future.

【意訳】  
まだデバッグ実行をサポートしていません。
