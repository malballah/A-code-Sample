import {Control} from './control';
import {ControlOption} from './controloption';

export class WebControl{
    constructor(control?:Control){
        if(control==null)
            control=new Control();
        this.control = control;
        this.childWebControls=new Array<WebControl>();
    }
 uqId:number;
 control:Control; 
 isActive:boolean;
 childWebControls:Array<WebControl>;

  get optionString():string{
        return this.control.options.map(item=>item.label).join("\n");
    }
     optionsChanged(value:string){
       let options = value.split("\n");
       if(this.control.options!=undefined){
           this.control.options.length=0;
            options.forEach((item,i)=>{
                this.control.options.push(new ControlOption(item,item,i+1));
            });
       }
       
    }
}