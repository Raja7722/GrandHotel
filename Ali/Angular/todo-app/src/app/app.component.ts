import { Component } from '@angular/core';
import { TodoService } from './services/todo.service';

@Component({ // Here I define the class of my component
  selector: 'app-root', // This is the tag of my components
  templateUrl: './app.component.html', // This are the properties of my component
  styleUrls: ['./app.component.css']
})
export class AppComponent { // here I instantiate my component
  constructor(private todoService: TodoService) {}
}
