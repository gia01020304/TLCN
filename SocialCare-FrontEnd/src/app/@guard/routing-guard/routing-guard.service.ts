import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { NbAccessChecker } from '@nebular/security';
import { tap } from 'rxjs/operators';

@Injectable()
export class RoutingGuardService implements CanActivate {
  canActivate(route: import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
    const permission = route.firstChild.data['permission'];
    const resource = route.firstChild.data['resource'];
    return this.access.isGranted(permission, resource).pipe(tap(result => {
      if (!result) {
        this.router.navigate(['/auth']);
        return;
      }
    }));
  }

  constructor(private router:Router,private access: NbAccessChecker) { }
}
