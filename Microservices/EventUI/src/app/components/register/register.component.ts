import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form:FormGroup
  status=undefined;

  constructor(private fb:FormBuilder, private userSvc:UserService) { }

  ngOnInit() {
    this.form= this.fb.group({
      firstName:["",Validators.required],
      lastName:["",Validators.required],
      email:["",Validators.compose([Validators.required,Validators.email])],
      password:["",Validators.compose([Validators.required,Validators.minLength(8)])]

    })
  }
  register(){
    this.status=undefined;
    if(this.form.valid)
    {
      this.userSvc.addUser(this.form.value).subscribe(
        result=>{
          console.log(result);
          this.status={success:true, class:"alert-success", message:"User registered successfully"}
        },
        err=>{
          console.log(err);
          this.status={success:false, class:"alert-danger", message:"User registration failed"}

        }
      );
    }
    else{
      console.log(this.form.value);
      this.status={success:false, class:"alert-danger", message:"Invalid user details"}

    }
    //alert("Done");
  }

}
