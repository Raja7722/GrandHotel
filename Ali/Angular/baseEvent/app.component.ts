import { Component } from '@angular/core';

@Component({
    selector: 'app-todolist',
    template:
    `<h1>{{listTitle}}</h1>
    <form (submit)='createTodo()'>
        <input type='text' name='todoLabel' [(ngModel)]='newTodo' />
</form>` // When we use ` instead of this ' we can write everything in one line
})
export class TodoListComponent { // A component is a a class in typescript
    listTitle = "Ma liste de todo";
    todos: string[] = [];
    newTodo: string = 'Ali';
    createTodo() {
        if (this.newTodo) {
            this.todos.push(this.newTodo); // push is equivalent to Add, it will add the newTodo in the list of strings todos
            this.newTodo = '';
        }
    }
}