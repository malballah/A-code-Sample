import {Control} from './control'
import {Group} from './group'

export class Form{
    constructor(){
        this.groups=new Array<Group>();
    }
    id:number;
    name:string;
    title:string;
    groups:Array<Group>;
      
}