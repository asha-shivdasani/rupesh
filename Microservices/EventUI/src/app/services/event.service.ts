import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { EventInfo } from '../models/event-info';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  readonly API_URL:string ="http://localhost:59120/api/events";

  constructor(private http:HttpClient) { }
  getEvents():Observable<EventInfo[]>{
    return this.http.get<EventInfo[]>(this.API_URL);
  }
  addEvent(event:any):Observable<any>{
    let tokenData = JSON.parse(localStorage.getItem("auth-token"));
    console.log(tokenData);
    let options={
      headers:{
        "Content-Type":"application/json",
        "Accept":"application/json",
        "Authorization":`Bearer ${tokenData.token}`

      }
    }
    return this.http.post<any>(this.API_URL,event,options);
  }
}
