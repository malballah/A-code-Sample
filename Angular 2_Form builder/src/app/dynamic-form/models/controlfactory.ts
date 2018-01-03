import {ControlType} from './controltype'
import {WebControl} from './webcontrol'
import {Control} from './control'

export class ControlFactory{
    static get(fieldType:ControlType){
        let control=new Control(fieldType);
        let webControl =new WebControl(control); 
        return webControl;       
    }
}