"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var TodoListComponent = /** @class */ (function () {
    function TodoListComponent() {
        this.listTitle = "Ma liste de todo";
        this.todos = [];
        this.newTodo = 'Ali';
    }
    TodoListComponent.prototype.createTodo = function () {
        if (this.newTodo) {
            this.todos.push(this.newTodo); // push is equivalent to Add, it will add the newTodo in the list of strings todos
            this.newTodo = '';
        }
    };
    TodoListComponent = __decorate([
        core_1.Component({
            selector: 'app-todolist',
            template: "<h1>{{listTitle}}</h1>\n    <form (submit)='createTodo()'>\n        <input type='text' name='todoLabel' [(ngModel)]='newTodo' />\n</form>" // When we use ` instead of this ' we can write everything in one line
        })
    ], TodoListComponent);
    return TodoListComponent;
}());
exports.TodoListComponent = TodoListComponent;
//# sourceMappingURL=app.component.js.map