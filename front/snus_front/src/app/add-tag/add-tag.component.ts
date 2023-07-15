import { MatSnackBar } from '@angular/material/snack-bar';
import { CreateTagDTO, TagService } from './../services/tag.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.css']
})
export class AddTagComponent {
  chosenTagType: string = '';
  tagTypes = ["Digital Input", "Digital Output", "Analog Input", "Analog Output"];
  driverTypes = ["Real-Time Driver", "Simulation Driver"];
  functionTypes = ["sin", "cos", "ramp"];
  showForm = false;
  selectedDriver = '';

  freeAddresses: string[] = [];

  addTagForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    ioAddress: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    units: new FormControl('', []),
    lowLimit: new FormControl('', []),
    highLimit: new FormControl('', []),
    initValue: new FormControl(0, []),
    initValueRadio: new FormControl('', []),
    driverType: new FormControl('', []),
    scanActivity: new FormControl(false, []),
    scanTime: new FormControl(0, []),
    
  });

  constructor(private tagService: TagService,
              private snackBar: MatSnackBar) {

  }

  onChosenTagTypeChange(newValue: string) {
    this.showForm = true;
    this.resetForm();
    this.fetchFreeAddresses(newValue);
  }

  fetchFreeAddresses(newValue: string) {
    if (newValue.includes("Input")) {
      this.tagService.getFreeAdresses().subscribe({
        next: (value) => {
          this.freeAddresses = value;
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        },
      });
    } else {
      this.tagService.getFreeOutputAdresses().subscribe({
        next: (value) => {
          this.freeAddresses = value;
        },
        error: (err) => {
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
         console.log(err);
        },
      });
    }
  }

  addTag() {
    console.log(this.addTagForm.value);
    console.log(this.addTagForm.valid);
    if (this.addTagForm.valid) {

      let initialValue;
      if (this.chosenTagType.includes("Analog"))
      {
        initialValue = this.addTagForm.value.initValue;
      } else {
        initialValue = this.addTagForm.value.initValueRadio == "0"? 0: 1;
      }

      let dto: CreateTagDTO = {
        name: this.addTagForm.value.name!,
        ioAddress: this.addTagForm.value.ioAddress!,
        description: this.addTagForm.value.description!,
        unit: this.addTagForm.value.units,
        lowLimit: this.addTagForm.value.lowLimit,
        highLimit: this.addTagForm.value.highLimit,
        scanTime: this.addTagForm.value.scanTime,
        isScanOn: this.addTagForm.value.scanActivity,
        type: this.chosenTagType!,
        initialValue: initialValue
      }

      this.tagService.addTag(dto).subscribe({
        next: (value) => {
          console.log(value);
          this.snackBar.open(value.message, "", {
            duration: 2700, panelClass: ['snack-bar-success']
          });
          this.fetchFreeAddresses(this.chosenTagType);
          this.resetForm();
        },
        error: (err) => {
          console.log(err);
          this.snackBar.open(err.error, "", {
            duration: 2700, panelClass: ['snack-bar-server-error']
         });
        },
      });
    }
  }
  resetForm() {
    this.addTagForm.reset();
    Object.keys(this.addTagForm.controls).forEach(key => {
      const control = this.addTagForm.get(key) as FormControl;
      control.setErrors(null);
    });
    this.addTagForm.updateValueAndValidity();
  }

  onDriverTypeChange(event: any) {
    if (event.value==this.driverTypes[0])
    {
      this.selectedDriver = "rt";
    } else {
      this.selectedDriver = "sim";
    }
  }
}
