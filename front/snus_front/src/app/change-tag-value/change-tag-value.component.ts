import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-change-tag-value',
  templateUrl: './change-tag-value.component.html',
  styleUrls: ['./change-tag-value.component.css']
})
export class ChangeTagValueComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ChangeTagValueComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar) { }

  tagValueForm = new FormGroup({
    value: new FormControl('', [Validators.required])
  });

  ngOnInit(): void {
    
  }
}
