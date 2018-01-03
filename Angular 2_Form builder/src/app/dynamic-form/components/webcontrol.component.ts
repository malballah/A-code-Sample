import { Component, Input } from '@angular/core';
import { FormGroup }        from '@angular/forms';

import { WebControl }     from '../models/webcontrol';

@Component({
  selector: 'webcontrol',
  templateUrl: 'webcontrol.component.html'
})
export class WebControlComponent {
  @Input() webControl: WebControl;
  @Input() form: FormGroup;
  get valid() { 
    if(this.form.controls[this.webControl.uqId]!=undefined)
        return this.form.controls[this.webControl.uqId].valid&&(this.form.controls[this.webControl.uqId].touched||this.form.controls[this.webControl.uqId].dirty); 
      return true;
  }
  get invalid() { 
    if(this.form.controls[this.webControl.uqId]!=undefined)
        return !this.form.controls[this.webControl.uqId].valid&&(this.form.controls[this.webControl.uqId].touched||this.form.controls[this.webControl.uqId].dirty); 
      return true;
  }
  getChildQuestions(id:number){
    //return this.questions.filter(item=>item.parentId==id);
  }
  //for radio button list
  onSelectionChange(key:string){
    this.webControl.control.value=key;
  }
}
