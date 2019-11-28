import { Injectable } from '@angular/core';
import { SystemConfigurate } from '../@models/system-configurate';
import { Resolve } from '@angular/router';
import { SystemConfigurateService } from '../@services/system-configurate.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
@Injectable()

export class SystemConfigurateResolver implements Resolve<SystemConfigurate>{
  resolve(route: import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): SystemConfigurate | import("rxjs").Observable<SystemConfigurate> | Promise<SystemConfigurate> {
    return this.systemConfigurateService.getSystemConfigurate().pipe(catchError(error => {
      console.log(error);
      return of(null);
    }))
  }
  constructor(private systemConfigurateService: SystemConfigurateService) { }
}
