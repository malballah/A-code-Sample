import { Component, OnInit,EventEmitter,Output } from '@angular/core';

import {ControlType} from '../models/controltype'
import {ControlTypes} from '../models/controltypes'
import {WebControl} from '../models/webcontrol'
import {ControlFactory} from '../models/controlfactory'

@Component({
    selector: 'controls',
    templateUrl: 'controls.component.html'
})

export class ControlsComponent implements OnInit {
    controlTypes:Array<ControlType>;
    @Output()
    addControlEvent:EventEmitter<WebControl>=new EventEmitter<WebControl>();
    constructor() { 

    }
    
    ngOnInit() {
        this.controlTypes = ControlTypes.getTypes();
     }

     addControl(controlType:ControlType){
        this.addControlEvent.emit(ControlFactory.get(controlType));
     }
}