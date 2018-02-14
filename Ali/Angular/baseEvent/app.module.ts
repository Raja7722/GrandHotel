import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TodoListComponent } from './app.component';

@NgModule({
    imports: [BrowserModule],
    declarations: [TodoListComponent],
    bootstrap: [TodoListComponent]
})
export class AppModule {

}
