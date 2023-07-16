import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TableOutputTag } from '../database-manager/database-manager.component';
import { TagService } from '../services/tag.service';

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css', '../database-manager/database-manager.component.css']
})
export class TrendingComponent implements OnInit {
 
  displayedColumns = ['name', 'type', 'description', 'value', 'isScanOn', 'scanTime'];
  inputTags: TableOutputTag[] = [];

  constructor(private dialog: MatDialog,
    private tagService: TagService,
    private snackBar: MatSnackBar,
    private router: Router){}

  ngOnInit(): void {
    
  }
}