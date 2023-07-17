import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TableInputTag, TableOutputTag } from '../database-manager/database-manager.component';
import { TagService } from '../services/tag.service';
import { InputTagValueSocketService } from '../services/trending-input-value-ws.service';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css', '../database-manager/database-manager.component.css']
})
export class TrendingComponent implements OnInit {
 
  displayedColumns = ['name', 'type', 'description', 'value', 'isScanOn', 'scanTime', 'alarmInfo'];
  inputTags: TableInputTag[] = [];
  alarmColors: any = [];
  inputTagUpdateConnection: any;
  alarmUpdateConnection: any;
  inputTagss: any;

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar,
    private router: Router,
    private inputTagValueSocketService: InputTagValueSocketService){}

  ngOnInit(): void {
    this.getAllInputTags();
    this.initInputTagUpdateWebSocket();
    this.initAlarmUpdateWebSocket();
    
  }

  @ViewChild(MatSort) sort: any;


  getAllInputTags() {
    this.tagService.getAllInputTags().subscribe({
      next: (value) => {
        this.inputTags = []
        // console.log("succ\n" + JSON.stringify(value));
        this.inputTags = value;
        let i = 0;
        for (let tag of this.inputTags) {
          this.alarmColors[i] = -1;
          i += 1
          if (tag.type == 0)
            tag.type = "DIGITAL"
          else 
            tag.type = "ANALOG"
        }
        this.inputTagss = new MatTableDataSource<TableInputTag>(this.inputTags);
        this.inputTagss.sort = this.sort;
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
      //  console.log(err);
      }
    });
  }

  initInputTagUpdateWebSocket() {
    this.inputTagUpdateConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7012/hub/updateInput', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    this.inputTagUpdateConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(() => console.log('Error while starting connection: '))
    this.inputTagUpdateConnection.on('input', (from: string, body: string) => {
      // console.log(from, body);
      this.handleInputTagUpdateWebSocket(from);
    });
  }

    handleInputTagUpdateWebSocket(tagRecord: any){
      // console.log(tagRecord.id);
      let i = 0;
      for (let tag of this.inputTags){
        if (tag.id == tagRecord.tagId){
          // if (tag.id == 6)
          //       console.log(tag.value, tagRecord.highLimit, tagRecord.lowLimit);
          if (tag.value != tagRecord.highLimit && tag.value != tagRecord.lowLimit){
            // if (tag.id == 6)
            //     console.log("SADA");
            this.alarmColors[i] = -1;}
          tag.value = tagRecord.value;
          break;
        }
        i += 1;
      }
    }

  initAlarmUpdateWebSocket() {
    this.alarmUpdateConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7012/hub/updateAlarm', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    this.alarmUpdateConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(() => console.log('Error while starting connection: '))
    this.alarmUpdateConnection.on('alarm', (from: string, body: string) => {
      // console.log(from, body);
      this.handleAlarmUpdateWebSocket(from);
    });
  }

    handleAlarmUpdateWebSocket(alarmRecord: any){
      let i = 0;
      //console.log(alarmRecord);
      for (let tag of this.inputTags){
        if (tag.id == alarmRecord.tagId){
          if (alarmRecord.priority == 0)
            this.alarmColors[i] = 0
          else if (alarmRecord.priority == 1)
            this.alarmColors[i] = 1;
          else 
            this.alarmColors[i] = 2;
          tag.alarmValue = alarmRecord.value;
          tag.alarmType = alarmRecord.type;
          break;
        }
        i += 1;
      }
    }

    getRowColor(rowId: number): any {
      //console.log(rowId);
      let alarmColorValue = this.alarmColors[rowId];
      if (alarmColorValue === 0) {
        return { background: '#edd2d1', color: 'black' };
      } else if (alarmColorValue === 1) {
        return { background: '#d38f8c', color: 'white' };
      } else if (alarmColorValue === 2) {
        return { background: '#B94C47', color: '#999999' };
      } else {
        return {}; 
      }
    }

    typeNumberToEnum(num: number){
      if (num == 0)
        return "LOWER"
      return "HIGHER"
    }
  
}