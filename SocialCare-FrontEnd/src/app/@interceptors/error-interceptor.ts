import { Injectable, Injector } from "@angular/core";
import { HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private inj: Injector) { }

  intercept(req: import("@angular/common/http").HttpRequest<any>, next: import("@angular/common/http").HttpHandler): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      debugger;
      if (error.status === 401) {
        return throwError(error.statusText);
      }
      if (error instanceof HttpErrorResponse) {
        const applicationError = error.headers.get("Application-Error");
        if (applicationError) {
          return throwError(applicationError);
        }
        const serverError = error.error;
        let modelStateError = '';
        if (serverError && typeof serverError.errors === "object") {
          for (const key in serverError.errors) {
            if (serverError.errors[key]) {
              modelStateError += serverError.errors[key] + "\n";
            }
          }
        }
        return throwError(modelStateError || "Server Error");
      }
    }))

  }

};
