import {Control} from './control'

export class Group{
    constructor(){
        this.controls=new Array<Control>();
    }
    id:number;
    title:string;
    order:number;
    tooltip:string;
    formId:number;
    controls:Array<Control>;    
}
