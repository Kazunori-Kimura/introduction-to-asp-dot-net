// markdown to html
// md2html.js
(function(mod){

  // CommonJS export
  module.exports = mod();

})(function(){
  // モジュール定義
  'use strict';

  var marked = require("marked");
  var ECT = require("ect");
  var extend = require("extend");
  var Q = require("q");

  var MyModule = function MyModule(){};

  // use highlight.js
  marked.setOptions({
    highlight: function(code){
      return require("highlight.js").highlightAuto(code).value;
    }
  });

  MyModule.prototype = {

  };

  return MyModule;
});
