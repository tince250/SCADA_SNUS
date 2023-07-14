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
  showForm = false;

  addTagForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    ioAddress: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    units: new FormControl('', []),
    lowLimit: new FormControl('', []),
    highLimit: new FormControl('', []),
    initValue: new FormControl('', []),
    driverType: new FormControl('', []),
    scanActivity: new FormControl('', []),
    scanTime: new FormControl('', []),
    
  });

  onChosenTagTypeChange(newValue: string) {
    this.showForm = true;
  }

  addTag() {

  }
}
