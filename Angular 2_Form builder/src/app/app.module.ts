import { BrowserModule }  from '@angular/platform-browser';
import { NgModule }       from '@angular/core';
import { HttpModule }     from '@angular/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent }   from './app.component';
import { FormCreatorComponent }   from './dynamic-form/components/formcreator.component';
import { FormCreatorModule }    from './dynamic-form/formcreator.module';

const appRoutes: Routes = [
   { path: '', component: FormCreatorComponent },
    { path: 'formcreator', component: FormCreatorComponent },
    { path: '**', redirectTo: '' },
   
];

@NgModule({
  imports: [ BrowserModule,HttpModule,FormCreatorModule,RouterModule.forRoot(appRoutes)],
  declarations: [ AppComponent ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
  constructor() {
  }
}
 