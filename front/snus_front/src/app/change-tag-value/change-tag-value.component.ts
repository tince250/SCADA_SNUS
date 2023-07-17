import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOutputTag } from '../database-manager/database-manager.component';
import { tagValueRegexValidator } from '../validators/tag-value-validator';
import { TagService } from '../services/tag.service';

@Component({
  selector: 'app-change-tag-value',
  templateUrl: './change-tag-value.component.html',
  styleUrls: ['./change-tag-value.component.css']
})
export class ChangeTagValueComponent implements OnInit {

  tag: any;

  constructor(public dialogRef: MatDialogRef<ChangeTagValueComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tagService: TagService,
    private snackBar: MatSnackBar) { }

  tagValueForm = new FormGroup({
    value: new FormControl('', [Validators.required, tagValueRegexValidator])
  });

  ngOnInit(): void {
    this.tag = this.data.tag;
  }

  changeTagValue(){
    if (this.tagValueForm.valid){
      // console.log(this.tagValueForm.value.value)
      if (this.tag.type == "DIGITAL"){
        if (this.tagValueForm.value.value != "0" && this.tagValueForm.value.value != "1"){
          this.snackBar.open("Digital output can only have 0 and 1 values!", "", {
            duration: 2700, panelClass: ['snack-bar-front-error']
         });
         return;
        }
        this.tagService.updateDigitalOutputValue(this.tag.id, this.tagValueForm.value.value).subscribe({
          next: (value) => {
            this.snackBar.open("Successfully updated value of tag with id: " + this.tag.id, "", {
              duration: 2700, panelClass: ['snack-bar-success']
           });
           this.dialogRef.close();
          },
          error: (err) => {
            this.snackBar.open(err.error, "", {
              duration: 2700, panelClass: ['snack-bar-server-error']
           });
           console.log(err);
          }
        })
      } else {
        this.tagService.updateAnalogOutputValue(this.tag.id, this.tagValueForm.value.value).subscribe({
          next: (value) => {
            this.snackBar.open("Successfully updated value of tag with id: " + this.tag.id, "", {
              duration: 2700, panelClass: ['snack-bar-success']
           });
           this.dialogRef.close();
          },
          error: (err) => {
            this.snackBar.open(err.error, "", {
              duration: 2700, panelClass: ['snack-bar-server-error']
           });
           console.log(err);
          }
        })
      }
    } else {
      this.snackBar.open("Check your inputs again!", "", {
        duration: 2700, panelClass: ['snack-bar-front-error']
     });
    }
  }
}
