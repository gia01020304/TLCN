import { Component, OnInit } from '@angular/core';
import { ReactiveFormConfig, ErrorMessageBindingStrategy } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    ReactiveFormConfig.set({
      "internationalization": {
        "dateFormat": "dmy",
        "seperator": "/"
      },
      "validationMessage": {
        "alpha": "Only alphabelts are allowed.",
        "alphaNumeric": "Only alphabet and numbers are allowed.",
        "compare": "inputs are not matched.",
        "contains": "value is not contains in the input",
        "creditcard": "creditcard number is not correct",
        "digit": "Only digit are allowed",
        "email": "email is not valid",
        "greaterThanEqualTo": "please enter greater than or equal to the joining age",
        "greaterThan": "please enter greater than to the joining age",
        "hexColor": "please enter hex code",
        "json": "please enter valid json",
        "lessThanEqualTo": "please enter less than or equal to the current experience",
        "lessThan": "please enter less than or equal to the current experience",
        "lowerCase": "Only lowercase is allowed",
        "maxLength": "maximum length is 10 digit",
        "maxNumber": "enter value less than equal to 3",
        "minNumber": "enter value greater than equal to 1",
        "password": "please enter valid password",
        "pattern": "please enter valid zipcode",
        "range": "please enter age between 18 to 60",
        "required": "This field is required",
        "time": "Only time format is allowed",
        "upperCase": "Only uppercase is allowed",
        "url": "Only url format is allowed",
        "zipCode": "enter valid zip code",
        "minLength": "minimum length is 10 digit"
      }
    });
  }
  /**
   *
   */
  constructor() {
  }
  title = 'Gfried-FrontEnd';
}
