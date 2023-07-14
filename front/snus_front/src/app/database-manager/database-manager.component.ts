import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChangeTagValueComponent } from '../change-tag-value/change-tag-value.component';
import { TagService } from '../services/tag.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-database-manager',
  templateUrl: './database-manager.component.html',
  styleUrls: ['./database-manager.component.css']
})
export class DatabaseManagerComponent implements OnInit {
 
  isInputTagsClicked: boolean = false;
  isOutputTagsClicked: boolean = false;
  outputTags: TableOutputTag[] = [];
  displayedColumns = ['name', 'type', 'description', 'value', 'actions'];

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar){}

  ngOnInit(): void {
    this.tagService.getAllOutputTagsDBManager().subscribe({
      next: (value) => {
        console.log("succ\n" + JSON.stringify(value));
        this.outputTags = value;
      },
      error: (err) => {
        this.snackBar.open(err.error, "", {
          duration: 2700, panelClass: ['snack-bar-server-error']
       });
       console.log(err);
      }
    })
  }

  changeTagValue(tag: TableOutputTag){
    this.dialog.open(ChangeTagValueComponent, {
      data: {}
    });
  }

  deleteTag(tag: TableOutputTag){

  }

  onInputTagsClicked(){
    this.isInputTagsClicked = true;
    this.isOutputTagsClicked = false;
  }

  onOutputTagsClicked(){
    this.isOutputTagsClicked = true;  
    this.isInputTagsClicked = false;

  }
}


export interface TableOutputTag {
  id: number,
  description: string,
  value: string,
  unit: string,
  type: string
}