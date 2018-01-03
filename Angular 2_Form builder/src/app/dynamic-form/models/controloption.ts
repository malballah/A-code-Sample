export class ControlOption{
    id:number;
    label:string;
    value:string;
    order:number;
    constructor(label:string,value:string,order:number = 0){
        this.label = label;
        this.value = value;
        this.order = order;
    }
}