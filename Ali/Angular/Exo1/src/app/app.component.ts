import { Component } from '@angular/core';

@Component({
  selector: 'app-todolist',
  template:
  `<h1>{{listTitle}}</h1>
   <ul>
      <li *ngFor='let todo of todos'>
    {{todo}}
</li>
</ul>`,
})
export class TodolistComponent {
  listTitle = "Ma Todo List";
  todos: string[] = ["a", "b", "c", "d"];
}

//import { Component } from '@angular/core';

//@Component({
//  selector: 'app-root',
//  templateUrl: './app.component.html',
//  styleUrls: ['./app.component.css']
//})
//export class AppComponent {
//  title = 'app';
//}
