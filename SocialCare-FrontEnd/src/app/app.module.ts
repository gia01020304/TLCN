import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbToastrModule, NbWindowModule, NbDialogModule, NbDatepickerModule, NbMenuModule, NbSidebarModule } from '@nebular/theme';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CoreModule } from './@core/core.module';
import { ThemeModule } from './@theme/theme.module';
import { NbAuthModule, NbPasswordAuthStrategy, NbAuthJWTInterceptor, NB_AUTH_TOKEN_INTERCEPTOR_FILTER } from '@nebular/auth';
import { NbPasswordAuthStrategyOptions } from './@option/password-option';
import { AuthGuardService } from './@guard/auth-guard/auth-guard.service';
import { NbSecurityModule, NbRoleProvider } from '@nebular/security';
import { RoleProvider } from './@providers/role.provider';
import { ErrorInterceptor } from './@interceptors/error-interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    NbToastrModule.forRoot(),
    ThemeModule.forRoot(),
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbDatepickerModule.forRoot(),
    NbDialogModule.forRoot(),
    NbWindowModule.forRoot(),
    CoreModule.forRoot(),
    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup(new NbPasswordAuthStrategyOptions())
      ],
      forms: {},
    }),
    NbSecurityModule.forRoot({
      accessControl: {
        user: {
          view: ['status', 'home', 'dashboard'],
          parent: 'guest',
          create: 'comments',
        },
        admin: {
          parent: 'user',
          view: ['admin'],
        },
      },
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: NbAuthJWTInterceptor, multi: true },
    { provide: NB_AUTH_TOKEN_INTERCEPTOR_FILTER, useValue: function () { return false; }, },
    AuthGuardService,
    { provide: NbRoleProvider, useClass: RoleProvider },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
