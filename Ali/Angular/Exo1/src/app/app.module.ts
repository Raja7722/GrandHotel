import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { TodolistComponent } from './app.component';


@NgModule({
  declarations: [
    TodolistComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [TodolistComponent]
})
export class AppModule { }
