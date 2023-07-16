import { AlarmDTO, AlarmReturnedDTO, AlarmService } from './../services/alarm.service';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-manage-alarms-dialog',
  templateUrl: './manage-alarms-dialog.component.html',
  styleUrls: ['./manage-alarms-dialog.component.css']
})
export class ManageAlarmsDialogComponent {
  addAlarmForm = new FormGroup({
    type: new FormControl('', [Validators.required]),
    value: new FormControl(0, [Validators.required]),
    priority: new FormControl('', [Validators.required]),
  });

  tagId: number = -1;
  alarms: AlarmReturnedDTO[] = [];

  constructor(public dialogRef: MatDialogRef<ManageAlarmsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar,
    private alarmService: AlarmService) { }

  ngOnInit(): void {
    this.tagId = this.data.tagId;
    this.fetchAlarms();
  }

  fetchAlarms() {
    this.alarmService.getAlarmsForTag(this.tagId).subscribe({
      next: (value) => {
        console.log(value);
        this.alarms = value;
      },
      error: (err) => {
        console.log(err);
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
      },
    });
  }

  addNewAlarm() {
    if (this.addAlarmForm.valid) {
      let dto: AlarmDTO = {
        value: this.addAlarmForm.value.value!,
        priority: this.addAlarmForm.value.priority!,
        type: this.addAlarmForm.value.type!,
        tagId: this.tagId
      };
      console.log(dto)
      this.alarmService.addAlarm(dto).subscribe({
        next: (value) => {
          console.log(value);
          this.alarms.push(value);
          this.snackBar.open("Successfully added new alarm for tag.", "", {
            duration: 2700, panelClass: ['snack-bar-success']
          });
          this.resetForm();
        },
        error: (err) => {
          console.log(err);
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
          });
        },
      });
    }
  }

  removeAlarm(i: number) {
    this.alarmService.deleteAlarm(this.alarms[i].id, this.tagId).subscribe({
      next: (value) => {
        console.log(value);
        this.snackBar.open(value.message, "", {
          duration: 2700, panelClass: ['snack-bar-success']
        });
        this.alarms = this.alarms.filter(elem => elem.id != this.alarms[i].id);
      },
      error: (err) => {
        console.log(err);
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
      }
    });
  }

  resetForm() {
    this.addAlarmForm.reset();
    Object.keys(this.addAlarmForm.controls).forEach(key => {
      const control = this.addAlarmForm.get(key) as FormControl;
      control.setErrors(null);
    });
    this.addAlarmForm.updateValueAndValidity();
  }
}