"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var MyFirstAppComponent = /** @class */ (function () {
    function MyFirstAppComponent() {
        this.listName = "Ma Liste de chose � faire"; // We could specify listName:string or in general listName:any
    }
    MyFirstAppComponent = __decorate([
        core_1.Component({
            selector: 'my-first-app',
            templateUrl: './todo-list.component.html' // In the specified file I will put this: <h1>{{listName}}</h1>
        })
    ], MyFirstAppComponent);
    return MyFirstAppComponent;
}());
exports.MyFirstAppComponent = MyFirstAppComponent;
//# sourceMappingURL=app.component.js.map