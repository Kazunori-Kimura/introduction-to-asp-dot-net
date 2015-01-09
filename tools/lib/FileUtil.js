// ファイル操作モジュール
// fileUtil.js
(function(mod){

  // CommonJS export
  module.exports = mod();

})(function(){
  // モジュール定義
  'use strict';

  var fs = require("fs");
  var path = require("path");
  var Q = require("q");

  var MyModule = function MyModule(){};

  MyModule.prototype = {
    // 指定されたフォルダのファイルを削除する
    // @param {string} targetFolder
    // @param {regexp} filter
    // @return {array} delete filenames
    removeFiles: function(targetFolder, filter){

      var files = [];

      Q.nfcall(fs.readdir, targetFolder).then(function(files){
        files.forEach(function(file){
          if(filter.test(file)){
            var filePath = path.resolve(targetFolder, file);
            files.push(filePath);

            // 取得したファイルを削除
            fs.unlink(filePath);
          }
        });
      });

      // 削除したファイル名を返却
      return files;
    }
  };

  return MyModule;
});

