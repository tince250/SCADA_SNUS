import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeTagValueComponent } from './change-tag-value.component';

describe('ChangeTagValueComponent', () => {
  let component: ChangeTagValueComponent;
  let fixture: ComponentFixture<ChangeTagValueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeTagValueComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeTagValueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
