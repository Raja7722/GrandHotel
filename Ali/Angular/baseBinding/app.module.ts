import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MyFirstAppComponent } from './app.component';

@NgModule({
    imports: [BrowserModule],
    declarations: [MyFirstAppComponent],
    bootstrap:[MyFirstAppComponent]
})
export class AppModule {

}
