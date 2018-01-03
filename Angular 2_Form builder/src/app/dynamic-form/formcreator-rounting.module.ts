import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormCreatorComponent } from './components/formcreator.component';
import { FormListComponent } from './components/formlist.component';


const formCreatorRoutes: Routes = [
    { path: 'formcreator', component: FormCreatorComponent },
    { path: 'formcreator/formlist', component: FormListComponent },
    { path: 'formcreator/editform/:id', component: FormCreatorComponent },
    { path: 'formcreator/createform', component: FormCreatorComponent },
];
@NgModule({
    imports: [
        RouterModule.forChild(formCreatorRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class FormCreatorRoutingModule { }