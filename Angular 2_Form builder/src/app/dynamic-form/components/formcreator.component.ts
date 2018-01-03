
import { Component, OnInit,Output,Input,ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup,FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';

import {WebForm} from '../models/webform'
import {FormCreatorService} from '../services/formcreator.service'

@Component({
    selector: 'formcreator',
    templateUrl: 'formcreator.component.html',
    styleUrls:["formcreator.component.css","../../../assets/css/dragula.min.css"],
     encapsulation: ViewEncapsulation.None 
})

export class FormCreatorComponent implements OnInit {
    @Output()
    webForm:WebForm;

    constructor(private formBuilder:FormBuilder,private route: ActivatedRoute,
        private router: Router,private formCreatorrService:FormCreatorService) {
        this.createNewForm();
    }
    createNewForm(){
              this.webForm=new WebForm();
    }
    ngOnInit() { }
}