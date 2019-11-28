import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpResponse, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/do';

@Injectable()
export class LoginInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    return next.handle(req).do(evt => {
      if (req.url.indexOf('auth/login') === 1) {
        if (evt instanceof HttpResponse) {
          console.log('---> status:', evt.status);
          console.log('---> filter:', req.params.get('filter'));
        }
      }
    });

  }
}
