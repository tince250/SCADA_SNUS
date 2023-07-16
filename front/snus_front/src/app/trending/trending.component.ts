import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TableInputTag, TableOutputTag } from '../database-manager/database-manager.component';
import { TagService } from '../services/tag.service';
import { InputTagValueSocketService } from '../services/trending-input-value-ws.service';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css', '../database-manager/database-manager.component.css']
})
export class TrendingComponent implements OnInit {
 
  displayedColumns = ['name', 'type', 'description', 'value', 'isScanOn', 'scanTime'];
  inputTags: TableInputTag[] = [];

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar,
    private router: Router,
    private inputTagValueSocketService: InputTagValueSocketService){}

  ngOnInit(): void {
    this.getAllInputTags();
    //this.inputTagValueSocketService.connect();
    //this.inputTagValueSocketService.openWebSocketConnection();
    this.initWebSocket();
  }

  getAllInputTags() {
    this.tagService.getAllInputTags().subscribe({
      next: (value) => {
        this.inputTags = []
        console.log("succ\n" + JSON.stringify(value));
        this.inputTags = value;
        for (let tag of this.inputTags) {
          if (tag.type == 0)
            tag.type = "DIGITAL"
          else 
            tag.type = "ANALOG"
        }
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
       console.log(err);
      }
    });
  }

  connection: any;
  

  initWebSocket() {
    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7012/hub/updateInput', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    this.connection
      .start()
      .then(() => console.log('Connection started'))
      .catch(() => console.log('Error while starting connection: '))
    this.connection.on('input', (from: string, body: string) => {
      console.log(from, body);
      this.handleInputTagUpdateWebSocket(from);
    });
  }

    handleInputTagUpdateWebSocket(tagRecord: any){
      console.log(tagRecord.id);
      for (let tag of this.inputTags){
        if (tag.id-1 == tagRecord.tagId){
          tag.value = tagRecord.value;
          break;
        }
      }
    }
  
}