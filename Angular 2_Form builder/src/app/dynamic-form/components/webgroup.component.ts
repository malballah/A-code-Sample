import { Component, Input,Output,OnInit } from '@angular/core';
import {FormGroup,FormBuilder,Validators,FormControl} from '@angular/forms';

import { WebGroup }     from '../models/webgroup';
import { WebControl }     from '../models/webcontrol';


@Component({
  selector: 'webgroup',
  templateUrl: 'webgroup.component.html',
  providers:[]
})
export class WebGroupComponent implements OnInit {
  @Input() 
  webGroup: WebGroup;
  @Output()
  form: FormGroup;

  constructor(fb: FormBuilder){
    this.form=fb.group({}); 
    
  }

    //if form changes, we should remove validations from hidden questions
  formDataChanged(data:any){
    
    this.webGroup.webControls.forEach((wc)=>{
      wc.childWebControls.forEach((cwc)=>{
        //this conditions check if the child qestion is shown or hidden
        if(cwc.control.parentValue != wc.control.value){
          //if hidden remove all validations
          this.form.controls[cwc.uqId].setValidators(null);
          cwc.control.value=null;
        }
        else
        {
          //if displayed then put required validations 
           this.form.controls[cwc.uqId].setValidators(cwc.control.required?Validators.required:null);
        }
        this.form.controls[cwc.uqId].updateValueAndValidity({onlySelf:true});
      });
    });
   
  }

   parentQuestions(){
    //return this.groupQuestion.questions.filter(item=>item.parentId==undefined);
  }
  ngOnInit() {
     this.form = this.toFormGroup();
     this.form.valueChanges.subscribe(data => {
      this.formDataChanged(data);
    });
  }
  toFormGroup(){   
    let group: any = {};
    if(this.webGroup.webControls!=undefined)
    this.webGroup.webControls.forEach(wc => {
       group[wc.uqId] = wc.control.required ? new FormControl(wc.control.value || '', Validators.required)
                                              : new FormControl(wc.control.value || '');
    });
    return new FormGroup(group);
  
  }
  //activate current group to add controls
  activate(){
    this.webGroup.activate();
  }
  //activate control in group to add child questions
  activateControl(event:any,webControl:WebControl){
       if(event!=null)
        event.stopPropagation();
        if(webControl==this.webGroup.activeWebControl)
        return;
       webControl.isActive=true;
       this.webGroup.unActivate();
       if(this.webGroup.activeWebControl!=undefined)
        this.webGroup.activeWebControl.isActive=false;
        this.webGroup.webForm.isActive=false;
         this.webGroup.activeWebControl=webControl;
   }
}
