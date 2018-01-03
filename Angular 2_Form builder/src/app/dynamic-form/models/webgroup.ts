import {Group} from './group'
import {WebControl} from './webcontrol'
import {WebForm} from './webform'

export class WebGroup{
   group:Group;
   webControls:Array<WebControl>;
   activeWebControl:WebControl;
   webForm:WebForm;
   active:boolean;
    constructor(group?:Group){
        if(group==null)
            group=new Group();
        this.group = group;
        this.webControls=new Array<WebControl>();
    }

    addControl(webControl:WebControl){
        if(this.activeWebControl!=null)
            this.activeWebControl.childWebControls.push(webControl);
        else
            this.webControls.push(webControl);
    }
    activate(){
      this.unActivate();
        this.active=true;
    }
    unActivate(){
  this.webForm.webGroups.forEach((g)=>{
            g.active=false;
        });
    }
}