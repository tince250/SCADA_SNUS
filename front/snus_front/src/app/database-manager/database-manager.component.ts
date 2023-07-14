import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChangeTagValueComponent } from '../change-tag-value/change-tag-value.component';

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

  constructor(private dialog: MatDialog){}

  ngOnInit(): void {
    // throw new Error('Method not implemented.');
    let tag : TableOutputTag = {
      id: 1,
      description: "2",
      value: "3",
      unit: "4",
      type: "5"
    }
    this.outputTags.push(tag);
    this.outputTags.push(tag);
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