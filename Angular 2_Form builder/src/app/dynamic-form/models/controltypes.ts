import {ControlType} from './controltype'

export class ControlTypes{
   static textbox = new ControlType(1,"textbox","TextBox");
   static numberbox = new ControlType(2,"numberbox","NumberBox");
   static emailbox = new ControlType(3,"emailbox","EmailBox");
   static datebox = new ControlType(4,"datebox","DateBox");
   static textarea = new ControlType(5,"textarea","TextArea");
   static dropdown = new ControlType(6,"dropdown","DropDown");
   static checkbox = new ControlType(7,"checkbox","CheckBox");
   static radiobuttonlist = new ControlType(8,"radiobuttonlist","RadioButtonList");
   static checkboxlist = new ControlType(9,"checkboxlist","CheckBoxList");
   static group = new ControlType(9,"group","Group");

   static getTypes(){
       let controlTypes = new Array<ControlType>();
       controlTypes.push(ControlTypes.textbox);
        controlTypes.push(ControlTypes.numberbox);
         controlTypes.push(ControlTypes.emailbox);
          controlTypes.push(ControlTypes.datebox);
           controlTypes.push(ControlTypes.textarea);
            controlTypes.push(ControlTypes.dropdown);
             controlTypes.push(ControlTypes.checkbox);
              controlTypes.push(ControlTypes.radiobuttonlist);
              controlTypes.push(ControlTypes.checkboxlist);
              controlTypes.push(ControlTypes.group);
              return controlTypes;
   }
}