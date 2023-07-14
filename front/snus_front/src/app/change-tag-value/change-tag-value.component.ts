import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TableOutputTag } from '../database-manager/database-manager.component';
import { tagValueRegexValidator } from '../validators/tag-value-validator';

@Component({
  selector: 'app-change-tag-value',
  templateUrl: './change-tag-value.component.html',
  styleUrls: ['./change-tag-value.component.css']
})
export class ChangeTagValueComponent implements OnInit {

  tag: any;

  constructor(public dialogRef: MatDialogRef<ChangeTagValueComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar) { }

  tagValueForm = new FormGroup({
    value: new FormControl('', [Validators.required, tagValueRegexValidator])
  });

  ngOnInit(): void {
    this.tag = this.data.tag;
  }

  changeTagValue(){
    if (this.tagValueForm.valid){
      console.log(this.tagValueForm.value.value)
    } else {
      this.snackBar.open("Check your inputs again!", "", {
        duration: 2700, panelClass: ['snack-bar-front-error']
     });
    }
  }
}
