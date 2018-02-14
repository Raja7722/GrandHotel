import { Injectable } from '@angular/core';
import { Todo } from '../classes/todo';

@Injectable()
export class TodoService {
  private todos: Todo[];
  private nextId: number;
  constructor() {
    this.todos = [
      new Todo(1, 'Voir AngularJs sur W3Schools'),
      new Todo(2, 'Finir le projet WebStatic'),
      new Todo(3, 'Se reposer'),
    ]; // A list of 3 objects
    this.nextId = 4;
  }
  public getTodos(): Todo[] { return this.todos; } // A method that returns a list of Todos

  public addTodo(text: string):void {
    this.todos.push(new Todo(this.nextId, text));
    this.nextId++;
  }
  public removeTodo(id: number): void { // A function that removes a todo and it returns nothing
    this.todos=this.todos.filter(t=>t.id!==id); // filter is like where in c# Here I say "Take all the todos in my list that have a different id than the one given and replace the old list with the new". Thus it will remove the one with the given id."
  }
}
