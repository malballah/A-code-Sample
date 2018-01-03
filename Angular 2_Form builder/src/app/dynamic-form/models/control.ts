import {ControlType} from './controltype'
import {ControlOption} from './controloption'

export class Control {
  constructor(controlType?:ControlType){
    this.controlType = controlType;
    this.childControls=new Array<Control>();
    if(controlType.type=="dropdown"||controlType.type=="radiobuttonlist"||controlType.type=="checkboxlist")
       this.options=new Array<ControlOption>();
  }
  id:number;
  name:string;
  parentId:number;
  groupId:number;
  formId:number;
  value: any;
  parentValue:any;
  label: string;
  tooltip:string;
  required: boolean;
  order: number;
  controlType: ControlType;
  options:Array<ControlOption>;
  childControls:Array<Control>;
}
