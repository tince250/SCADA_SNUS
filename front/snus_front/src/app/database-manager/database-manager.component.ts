import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-database-manager',
  templateUrl: './database-manager.component.html',
  styleUrls: ['./database-manager.component.css']
})
export class DatabaseManagerComponent implements OnInit {
 
  outputTags: TableOutputTag[] = [];
  displayedColumns = ['name', 'type', 'description', 'value', 'actions'];

  constructor(){}
  ngOnInit(): void {
    // throw new Error('Method not implemented.');
    let tag : TableOutputTag = {
      id: "1",
      description: "2",
      value: "3",
      unit: "4",
      type: "5"
    }
    this.outputTags.push(tag);
  }

  handleButtonClick(tag: any){
    
  }


}


export interface TableOutputTag {
  id: string,
  description: string,
  value: string,
  unit: string,
  type: string
}