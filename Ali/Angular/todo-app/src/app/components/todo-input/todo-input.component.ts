import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'app-todo-input',
  templateUrl: './todo-input.component.html',
  styleUrls: ['./todo-input.component.css']
})
export class TodoInputComponent implements OnInit {

  private todoText: string;
  constructor(private todoService: TodoService) { //"private todoService : TodoService" This will create a private property get/set of type TodoService named todoService
    this.todoText = ''; 
    // "this" is necessary to define a property of this class. If I just said todoText= "" for it it would be a global variable and it would't find it.
  }

  ngOnInit() {
  }

  private addTodo(): void { // The functionality of my button
    this.todoService.addTodo(this.todoText);
    //console.log("TODO:", this.todoText); // This will meke my message appear in the console of the page (visible with F12)
    this.todoText = ''; // This will make my text disappear once I click "submit". This happens thanks to the binding we did.
  }
}
