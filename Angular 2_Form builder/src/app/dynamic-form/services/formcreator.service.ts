import { Injectable } from '@angular/core';
import { Http, Response, Headers} from '@angular/http'
import 'rxjs/add/operator/map';

@Injectable()
export class FormCreatorService {

  constructor(private http:Http) { }

  getControls() {
    //get questions from the api
    return this.http.get("data.json").map((res:Response) => res.json());
  }
   saveForm(answers:{id:number,value:any}[]){
     //post the answers to the server
       //this.post("");
       console.log(answers);
   }

}
