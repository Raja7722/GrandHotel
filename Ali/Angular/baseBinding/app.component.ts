import { Component } from '@angular/core';

@Component({ // This is the equivalent of attributes in c# but we call them annotations or decorations
    selector: 'my-first-app',
    templateUrl: './todo-list.component.html' // In the specified file I will put this: <h1>{{listName}}</h1>
})
export class MyFirstAppComponent { // A component is a a class in typescript
    listName = "Ma Liste de chose à faire";     // We could specify listName:string or in general listName:any
}