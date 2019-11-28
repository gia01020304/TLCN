import { NgModule } from '@angular/core';
import { NbMenuModule, NbCardModule, NbTabsetModule, NbInputModule, NbButtonModule, NbTooltipModule, NbTreeGridModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { PagesComponent } from './pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { PagesRoutingModule } from './pages-routing.module';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { RoutingGuardService } from '../@guard/routing-guard/routing-guard.service';
import { SystemConfigurateComponent } from './system-configurate/system-configurate.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SystemConfigurateResolver } from '../@resolvers/system-configurate.resolvers';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';
import { SocialConfigurationComponent } from './social-configuration/social-configuration.component';


@NgModule({
  imports: [
    NbTabsetModule,
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    DashboardModule,
    NbCardModule,
    NbInputModule,
    NbButtonModule,
    FormsModule,
    ReactiveFormsModule,
    RxReactiveFormsModule,
    NbTooltipModule,
    NbTreeGridModule
  ],
  declarations: [
    PagesComponent,
    HomeComponent,
    AdminComponent,
    SystemConfigurateComponent,
    SocialConfigurationComponent,
  ],
  providers:[
    RoutingGuardService,
    SystemConfigurateResolver
  ]
})
export class PagesModule {
}
