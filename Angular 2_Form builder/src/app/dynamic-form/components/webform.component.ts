import { Component, OnInit,Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { WebForm } from '../models/webform';
import { WebGroup } from '../models/webgroup';
import { WebControl } from '../models/webcontrol';

@Component({
    selector: 'webform',
    templateUrl: './webform.component.html'
})

export class WebFormComponent implements OnInit {
    @Input()
    webForm:WebForm;   
    constructor() { }

    ngOnInit() { }

    addControl(webControl:WebControl){
        this.webForm.addControl(webControl);
    }
    activateForm(){
        this.webForm.isActive=true;
    }
}