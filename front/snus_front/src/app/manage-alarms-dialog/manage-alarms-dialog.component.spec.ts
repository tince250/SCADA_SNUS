import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAlarmsDialogComponent } from './manage-alarms-dialog.component';

describe('ManageAlarmsDialogComponent', () => {
  let component: ManageAlarmsDialogComponent;
  let fixture: ComponentFixture<ManageAlarmsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAlarmsDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAlarmsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
