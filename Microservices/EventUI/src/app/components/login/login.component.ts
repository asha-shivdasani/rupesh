import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form:FormGroup
  status=undefined;

  constructor(private fb:FormBuilder, private userSvc:UserService, private router:Router) { }

  ngOnInit() {
    this.form= this.fb.group({      
      email:["",Validators.compose([Validators.required,Validators.email])],
      password:["",Validators.compose([Validators.required,Validators.minLength(8)])]

    })
  }

  login(){
    this.status=undefined;
    if(this.form.valid)
    {
      this.userSvc.getToken(this.form.value).subscribe(
        result=>{
          console.log(result);
          localStorage.setItem("auth-token",JSON.stringify(result));
          this.router.navigate(['/']);          
        },
        err=>{
          console.log(err);
          this.status={success:false, class:"alert-danger", message:"Login failed"}

        }
      );
    }
    else{
      console.log(this.form.value);
      this.status={success:false, class:"alert-danger", message:"Invalid user data"}

    }
    //alert("Done");
  }

}
