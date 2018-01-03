import { Component, OnInit,Input } from '@angular/core';

import { WebControl } from '../models/webcontrol';

@Component({
    selector: 'controlsettings',
    templateUrl: 'controlsettings.component.html'
})

export class ControlSettingsComponent implements OnInit {

     @Input() 
     webControl: WebControl;

    constructor() { }

    ngOnInit() { }

    isListControl(){
        return this.webControl.control.options!=undefined;
    }
    optionsListKeyPress(event:any){
    if(event.keyCode==13)
       this.webControl.optionsChanged(event.target.value);
}
}