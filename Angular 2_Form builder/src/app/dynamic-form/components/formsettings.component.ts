import { Component, OnInit,Input } from '@angular/core';

import { WebForm } from '../models/webform';

@Component({
    selector: 'formsettings',
    templateUrl: 'formsettings.component.html'
})

export class FormSettingsComponent implements OnInit {

    @Input()
    webForm:WebForm;

    constructor() { }

    ngOnInit() { }
}