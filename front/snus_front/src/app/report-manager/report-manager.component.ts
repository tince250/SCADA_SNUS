import { Component } from '@angular/core';

@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent {

  selectedCriteria: string = 'Select';
  selectedSort: string = 'Select';


  changeCritera(c: string) {
    this.selectedCriteria = c;
  }

  changeSort(s: string) {
    this.selectedSort = s;
  }

}
