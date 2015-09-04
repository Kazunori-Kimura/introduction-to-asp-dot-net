// app.js

// -- Model

/**
 * 薬品情報
 * @param id number
 * @param name string
 * @param typeName string
 * @param description string
 */
function Drug(id, name, typeName, description){
  var self = this;

  self.id = ko.observable(id);
  self.name = ko.observable(name);
  self.typeName = ko.observable(typeName);
  self.description = ko.observable(description);
}

// -- ViewModel

function AppView(){
  var self = this;

  // 薬品情報リスト
  self.drugList = ko.observableArray();
  // 追加する薬品情報
  self.editingDrug = new Drug("", "", "", "");
}
