import { NgModule } from '@angular/core';
import {CommonModule } from '@angular/common'
import { ReactiveFormsModule,FormsModule }          from '@angular/forms';

import { PopoverModule } from 'ngx-bootstrap';
import { TabsModule } from 'ngx-bootstrap';
import { DragulaModule,dragula } from 'ng2-dragula';
import { FormCreatorRoutingModule } from './formcreator-rounting.module';
import { FormCreatorService } from './services/formcreator.service';
import { FormCreatorComponent } from './components/formcreator.component';
import { FormListComponent } from './components/formlist.component'
import { ControlsComponent } from './components/controls.component';
import { ControlSettingsComponent } from './components/controlsettings.component';
import { WebFormComponent } from './components/webform.component';
import { FormSettingsComponent } from './components/formsettings.component';
import { WebControlComponent } from './components/webcontrol.component';
import { WebGroupComponent } from './components/webgroup.component';

@NgModule({
    imports: [CommonModule,ReactiveFormsModule,PopoverModule.forRoot(),TabsModule.forRoot(),DragulaModule,FormsModule,FormCreatorRoutingModule],
    exports: [FormCreatorComponent],
    declarations: [FormCreatorComponent,FormListComponent,WebFormComponent,WebControlComponent,WebGroupComponent,ControlsComponent,ControlSettingsComponent,FormSettingsComponent],
    providers: [FormCreatorService],
})
export class FormCreatorModule { }
 