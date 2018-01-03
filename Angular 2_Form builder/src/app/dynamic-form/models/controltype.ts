export class ControlType{
    id:number;
    type:string;
    title:string;
    constructor(id:number,type:string,title:string=type){
        this.id = id;
        this.type = type;
        this.title = title;
    }
}