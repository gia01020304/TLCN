import { Component } from '@angular/core';
import { NbAuthService, NbAuthToken, NbAuthJWTToken } from '@nebular/auth';

@Component({
  selector: 'ngx-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  user = {};
  constructor(private authService: NbAuthService) {

  }
}
