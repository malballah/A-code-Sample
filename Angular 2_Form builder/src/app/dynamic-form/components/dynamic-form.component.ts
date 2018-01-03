import {Component, Input,Output, OnInit} from '@angular/core';
import {FormGroup,FormBuilder} from '@angular/forms';



@Component({
  selector: 'dynamic-form',
  templateUrl: './dynamic-form.component.html',
  //styleUrls: ['./dynamic-form.component.css'],
  providers: [  ]
})
export class DynamicFormComponent  {
 /* @Output()
  questions: QuestionBase<any>[] = [];
  questionGroups:Array<QuestionGroup>;
 form: FormGroup;

  constructor(fb: FormBuilder,private qservice: QuestionService ) { 
    this.form=fb.group({}); 
     this.questionGroups=new Array<QuestionGroup>(); 
  }

  ngOnInit() {
      //map each question to it's model 
    this.qservice.getQuestions()   
    .subscribe(qs => { 
      let i=1;
       (qs as any[]).forEach(question=>{
          this.questions.push(question);
        //get question groups
        if(question.groupId!=undefined && !this.questionGroups.some(gi=>gi.id==question.groupId)){
          let qg=new  QuestionGroup();
          qg.id=question.groupId;
          qg.name = "group "+ (i++);
          qg.questions.push(question);
          this.questionGroups.push(qg);
        } 
        else
        {
            let qg=this.questionGroups.find(g=>g.id==question.groupId);
            if(qg!=undefined){
              qg.questions.push(question);
            }
        }           
    });

    this.questions=this.questions.sort((a,b)=>a.order-b.order);
   
  });
  }
isGroupValid(index:number){
  let valid = true;
  for(let i = 0;i<index;i++){
        valid = this.questionGroups[i].form.valid && valid;
  }
  return valid;
}
 getQuestionObject(question:any):QuestionBase<any>{    
   let questionModel:any;
      switch(question.controlType){
        case "textbox":
        questionModel = question as TextboxQuestion;
         case "textarea":
        questionModel = question as TextAreaQuestion;
        case "dropdown":
        questionModel = question as DropdownQuestion;
         case "checkbox":
        questionModel = question as CheckboxQuestion;
        case "radiobuttonlist":
        questionModel = question as RadioButtonListQuestion;
      }

  return questionModel;
 }

  submit() {
    let answers:{id:number,value:any}[]=[];
    this.questions.forEach(item=>{
      answers.push({id:item.id,value:item.value});
    }); 
    this.qservice.saveAnswers(answers);
  }
*/
}
