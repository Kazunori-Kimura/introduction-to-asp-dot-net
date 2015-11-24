// app.js

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

/**
 * ViewModel
 */
var AppViewModel = function () {
    var self = this;

    // Todoリスト
    self.todoList = ko.observableArray([]);

    // 選択されたTodoを格納
    self.selectedItem = ko.observable();

    // モーダルダイアログ
    self.dialog = new DialogModel();

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

    /**
     * 新しいTodoの入力フォームを表示する
     */
    self.addTodo = function () {
        self.selectedItem(new ToDoModel());
    };

    /**
     * 削除が可能かどうか
     * @return {boolean}
     */
    self.isDeletable = function () {
        return self.selectedItem().id() != 0;
    };

    /**
     * キャンセルボタンのクリック
     */
    self.cancelEdit = function () {
        // 編集フォームを閉じる
        self.selectedItem(null);
    };

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