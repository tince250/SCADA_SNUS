import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ALarmDTO, AnalogInputDTO, DigitalInputDTO, ReportService, TagRecordDTO } from '../services/report.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TableInputTag } from '../database-manager/database-manager.component';

@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent {

  allTags : TagRecordDTO[] = [];
  allTagss: any;
  allTagsDisplayedColumns = ['timestamp', 'value', 'ioAddress']

  allAITags: AnalogInputDTO[] = [];
  allAITagsDisplayedColumns = ['description', 'value', 'ioAddress']

  allDITags: DigitalInputDTO[] = [];
  allDITagsDisplayedColumns = ['description', 'value', 'ioAddress']
  alarmDisplayedColumns = ['timestamp', 'value', 'priority', 'ioAddress']

  allTagsByAddress: TagRecordDTO[] = [];
  allTagsByAddresses: any;

  allAlarmsByPriority: ALarmDTO[] = [];
  allAlarmsByPriorities: any;

  allAlarmsBetweenDates: ALarmDTO[] = [];
  allAlarmsBetweenDatess: any;

  @ViewChild(MatSort) sortAlarm: any;
  @ViewChild(MatSort) sortAlarmPriority: any;
  @ViewChild(MatSort) sortTagsByAddresses: any;
  @ViewChild(MatSort) sortAllTagss: any;

  selectedCriteria: string = 'Select';
  selectedSort: string = 'Select';
  selectedAlarmPriority: string = 'Select'
  selectedTagAddress: string = 'Select'

  dateForm = new FormGroup({
    start: new FormControl(''),
    end: new FormControl('')
  }, [])

  constructor(private reportService: ReportService,
    public snackBar: MatSnackBar,) {
  }


  changeCritera(c: string) {
    this.selectedCriteria = c;
  }

  changeSort(s: string) {
    this.selectedSort = s;
  }

  changeSelectedAlarmPriority(p: string) {
    this.selectedAlarmPriority = p;
  }

  changeSelectedTagAddress(a: string) {
    this.selectedTagAddress = a;
  }

  generate() {
    console.log(this.selectedCriteria)
    if (this.selectedCriteria == 'Tags') {
      this.getAllTags();
    }
    else if (this.selectedCriteria == 'Alarms') {
      this.getAllAlarmsBetweenDates();
    }
    else if (this.selectedCriteria == 'Alarms by priority') {
      this.getAllAlarmsByPriority();
    }
    else if (this.selectedCriteria == 'Tag by I/O address') {
      this.getAllTagsByAddress();
    }
    else if (this.selectedCriteria == 'AI Tags') {
      this.getAllAITags();
    }
    else if (this.selectedCriteria == 'DI Tags') {
      this.getAllDITags();
    }
  }

  getAllTags() {
    this.reportService.getAllTags().subscribe({
      next: (value) => {
        console.log("succ\n" + JSON.stringify(value));
        this.allTags = value;
        this.allTagss = new MatTableDataSource<TagRecordDTO>(this.allTags);
        this.allTagss.sort = this.sortAllTagss;
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
      }
    })
  }

  getAllAITags() {
    this.reportService.getAllAITags().subscribe({
      next: (value) => {
        console.log("succ\n" + JSON.stringify(value));
        this.allAITags = value;
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
      }
    })
  }

  getAllDITags() {
    this.reportService.getAllDITags().subscribe({
      next: (value) => {
        console.log("succ\n" + JSON.stringify(value));
        this.allDITags = value;
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
      }
    })
  }

  getAllTagsByAddress() {
    console.log(this.selectedTagAddress);
    if (this.selectedTagAddress != 'Select') {
      this.reportService.getAllTagsByAddress(this.selectedTagAddress).subscribe({
        next: (value) => {
          console.log("succ\n" + JSON.stringify(value));
          this.allTagsByAddress = value;
          this.allTagsByAddresses = new MatTableDataSource<TagRecordDTO>(this.allTagsByAddress);
          this.allTagsByAddresses.sort = this.sortTagsByAddresses;
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
        });
        }
      })
    } else {
      this.snackBar.open('Please, select the address.', "", {
        duration: 2700, panelClass: ['snack-bar-server-error']
    });
    }
  }

  getAllAlarmsBetweenDates() {
    console.log(this.dateForm.value.end);
    console.log(this.dateForm.value.start);
    if (this.dateForm.value.start != '' && this.dateForm.value.end != '' &&
    this.dateForm.value.start != null && this.dateForm.value.end != null) {
      this.reportService.getAlarmsBetweenDates(this.dateForm.value.start, this.dateForm.value.end).subscribe({
        next: (value) => {
          console.log("succ\n" + JSON.stringify(value));
          this.allAlarmsBetweenDates = value;
          this.allAlarmsBetweenDatess = new MatTableDataSource<ALarmDTO>(this.allAlarmsBetweenDates);
          this.allAlarmsBetweenDatess.sort = this.sortAlarm;
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
        });
        }
      })
    } else {
      this.snackBar.open('Please, select the date range.', "", {
        duration: 2700, panelClass: ['snack-bar-server-error']
    });
    }
  }

  getAllAlarmsByPriority() {
    if (this.selectedAlarmPriority != 'Select') {
      this.reportService.getAlarmsByPriority(this.selectedAlarmPriority).subscribe({
        next: (value) => {
          console.log("succ\n" + JSON.stringify(value));
          this.allAlarmsByPriority = value;
          this.allAlarmsByPriorities = new MatTableDataSource<ALarmDTO>(this.allAlarmsByPriority);
          this.allAlarmsByPriorities.sort = this.sortAlarmPriority;
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
        });
        }
      })
    } else {
      this.snackBar.open('Please, select the priority.', "", {
        duration: 2700, panelClass: ['snack-bar-server-error']
    });
    }
  }

  
}
