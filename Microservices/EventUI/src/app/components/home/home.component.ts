import { Component, OnInit } from '@angular/core';
import {EventInfo} from 'src/app/models/event-info';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  events:EventInfo[];

  constructor(private eventSvc:EventService) { }

  ngOnInit() {
    this.eventSvc.getEvents().subscribe(result=>this.events=result, //success callback
      error=>console.log(error) // error callback
 
      )
  }

}
