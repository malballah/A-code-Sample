import {FormGroup} from '@angular/forms'

import {Form} from './form'
import {WebControl} from './webcontrol'
import {WebGroup} from './webgroup'

export class WebForm{
    constructor(form?:Form){
        if(form==null)
            form=new Form();
        this.form = form;
        this.webGroups=new Array<WebGroup>();
    }
    form:Form;
    webGroups:Array<WebGroup>; 
    activeWebGroup:WebGroup;
    formGroup:FormGroup;   
    controlCounter:number=1;
    groupCounter:number=1;
    isActive:boolean;
    addControl(webControl:WebControl){
        if(webControl.control.controlType.type=="group"){
           this.addNewGroup();
        }
        else
        {
            if(this.webGroups.length==0)
            this.addNewGroup();
            webControl.control.name = webControl.control.controlType.type + this.controlCounter++;
            webControl.control.label = "Click to edit...";
            this.activeWebGroup.addControl(webControl)
        }
    }
   private addNewGroup(){
         let webGroup = new WebGroup();
            webGroup.group.title = "Group " + this.groupCounter++;
            this.webGroups.push(webGroup);
            if(this.activeWebGroup!=undefined)
            this.activeWebGroup.webControls.forEach((wc)=>{
                wc.isActive=false;
            });
            this.activeWebGroup = webGroup;
            webGroup.webForm = this;
    }
}