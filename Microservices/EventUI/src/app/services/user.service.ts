import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly API_URL:string="http://localhost:52670/api/identity"

  constructor(private http:HttpClient) { }

  addUser(user:any):Observable<any>{
    let options = {
      headers:{
        "Content-Type":"application/json",
        "Accept":"application/json"
      }
    };
    return this.http.post<any>(`${this.API_URL}/auth/register`,user,options);
  }
  getToken(user:any):Observable<any>{
    let options = {
      headers:{
        "Content-Type":"application/json",
        "Accept":"application/json"
      }
    };
    return this.http.post<any>(`${this.API_URL}/auth/token`,user,options);

  }
}
