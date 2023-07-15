import { AbstractControl } from "@angular/forms";

export function tagValueRegexValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const regex = /^-?\d+(\.\d+)?$/;
    if (control.value !== undefined && !regex.test(control.value)) {
      return { tagValueRegexError: true };
    }
    return null;
  }
  