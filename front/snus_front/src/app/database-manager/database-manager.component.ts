import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChangeTagValueComponent } from '../change-tag-value/change-tag-value.component';
import { TagService } from '../services/tag.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ManageAlarmsDialogComponent } from '../manage-alarms-dialog/manage-alarms-dialog.component';

@Component({
  selector: 'app-database-manager',
  templateUrl: './database-manager.component.html',
  styleUrls: ['./database-manager.component.css']
})
export class DatabaseManagerComponent implements OnInit {
 
  isInputTagsClicked: boolean = false;
  isOutputTagsClicked: boolean = false;
  outputTags: TableOutputTag[] = [] ;
  inputTags: TableInputTag[] = [];
  displayedColumnsOutput = ['name', 'type', 'description', 'value', 'actions'];
  displayedColumnsInput = ['name', 'type', 'description', 'scanTime', 'scan', 'actions'];

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar,
    private router: Router){}

  ngOnInit(): void {
    
  }

  get dataSource(): TableOutputTag[] | TableInputTag[] {
    return this.isOutputTagsClicked ? this.outputTags : this.inputTags;
  }

  getAllOutputTags(){
    this.tagService.getAllOutputTagsDBManager().subscribe({
      next: (value) => {
        this.outputTags = []
        console.log("succ\n" + JSON.stringify(value));
        this.outputTags = value;
        for (let tag of this.outputTags) {
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

  changeTagValue(tag: TableOutputTag){
    this.dialog.open(ChangeTagValueComponent, {
      data: {tag: tag}
    });
    window.location.reload();
  }

  deleteTag(tag: TableOutputTag){
    if (tag.type == "DIGITAL"){
      this.tagService.deleteDigitalOutput(tag.id).subscribe({
        next: (value) => {
          this.snackBar.open("Successfully deleted tag with id: " + tag.id, "", {
            duration: 2700, panelClass: ['snack-bar-success']
         });
         window.location.reload();
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        }
      })
    } else {
      this.tagService.deleteAnalogOutput(tag.id).subscribe({
        next: (value) => {
          this.snackBar.open("Successfully deleted tag with id: " + tag.id, "", {
            duration: 2700, panelClass: ['snack-bar-success']
         });
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        }
      })
    }
  }

  deleteInputTag(tag: TableInputTag) {
    if (tag.type == "DIGITAL"){
      this.tagService.deleteDigitalInput(tag.id).subscribe({
        next: (value) => {
          this.snackBar.open("Successfully deleted tag with id: " + tag.id, "", {
            duration: 2700, panelClass: ['snack-bar-success']
         });
         window.location.reload();
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        }
      })
    } else {
      this.tagService.deleteAnalogInput(tag.id).subscribe({
        next: (value) => {
          this.snackBar.open("Successfully deleted tag with id: " + tag.id, "", {
            duration: 2700, panelClass: ['snack-bar-success']
         });
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        }
      })
    }
    this.inputTags = this.inputTags.filter(elem => elem.id !== tag.id)
  }

  onInputTagsClicked(){
    this.isInputTagsClicked = true;
    this.isOutputTagsClicked = false;
    this.getAllInputTags();
  }
 

  onOutputTagsClicked(){
    this.isOutputTagsClicked = true;  
    this.isInputTagsClicked = false;
    this.getAllOutputTags();

  }

  navigateToAddTag() {
    this.router.navigate(["add-tag"]);
  }

  openManageAlarms(tag: TableInputTag) {
    this.dialog.open(ManageAlarmsDialogComponent, {
      data: {tagId: tag.id}
    });
  }

  onScanToggleChange(tag: TableInputTag) {
    this.tagService.updateTagIsScanOn({
      id: tag.id,
      isScanOn: tag.isScanOn,
      type: tag.type
    }).subscribe({
      next: (value) => {
        this.snackBar.open(value.message, "", {
          duration: 2700, panelClass: ['snack-bar-success']
       });
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
       console.log(err);
      },
    });
  }

  printTags() {
    console.log(this.inputTags);
  }
}


export interface TableOutputTag {
  id: number,
  description: string,
  value: string,
  unit: string,
  type: any
}

export interface TableInputTag {
  alarmType?: any;
  alarmValue?: any;
  lowLimit?: any;
  highLimit?: any;
  id: number,
  description: string,
  unit: string,
  type: any,
  isScanOn: boolean,
  scanTime: number,
  value: number
}