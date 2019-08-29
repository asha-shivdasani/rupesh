import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {

  form:FormGroup
  status=undefined;

  constructor(private fb:FormBuilder, private eventSvc:EventService,  private router:Router) { }

  ngOnInit() {
    this.form= this.fb.group({      
      eventTitle:["",Validators.required],
      startDate:["",Validators.required],
      endDate:["",Validators.required],
      location:["",Validators.required],
      organizer:["",Validators.required],
      registrationUrl:["",Validators.required]

    })
  }
  addEvent(){
    this.status=undefined;
    if(this.form.valid)
    {
      this.eventSvc.addEvent(this.form.value).subscribe(
        result=>{
          console.log(result);          
          this.router.navigate(['/']);          
        },
        err=>{
          console.log(err);
          this.status={success:false, class:"alert-danger", message:"Failed to add new event item"}

        }
      );
    }
    else{
      console.log(this.form.value);
      this.status={success:false, class:"alert-danger", message:"Invalid  data"}

    }

  }
}
