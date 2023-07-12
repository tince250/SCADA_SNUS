import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent {

  selectedCriteria: string = 'Select';
  selectedSort: string = 'Select';
  selectedAlarmPriority: string = 'Select'

  dateForm = new FormGroup({
    start: new FormControl(''),
    end: new FormControl('')
  }, [])


  changeCritera(c: string) {
    this.selectedCriteria = c;
  }

  changeSort(s: string) {
    this.selectedSort = s;
  }

}
