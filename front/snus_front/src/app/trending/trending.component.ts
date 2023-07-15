import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TableInputTag, TableOutputTag } from '../database-manager/database-manager.component';
import { TagService } from '../services/tag.service';

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css', '../database-manager/database-manager.component.css']
})
export class TrendingComponent implements OnInit {
 
  displayedColumns = ['name', 'type', 'description', 'value', 'isScanOn', 'scanTime'];
  inputTags: TableInputTag[] = [];

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar,
    private router: Router){}

  ngOnInit(): void {
    this.getAllInputTags();
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
}