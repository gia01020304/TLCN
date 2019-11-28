import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { RoutingGuardService } from '../@guard/routing-guard/routing-guard.service';
import { SystemConfigurateComponent } from './system-configurate/system-configurate.component';
import { SystemConfigurateResolver } from '../@resolvers/system-configurate.resolvers';
import { SocialConfigurationComponent } from './social-configuration/social-configuration.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  canActivate: [RoutingGuardService],
  children: [
    {
      path: '',
      component: DashboardComponent,
      data: {
        permission: 'view',
        resource: 'dashboard'
      },
    },
    {
      path: 'home',
      component: HomeComponent,
      data: {
        permission: 'view',
        resource: 'home'
      },
    },
    {
      path: 'admin',
      component: AdminComponent,
      data: {
        permission: 'view',
        resource: 'admin'
      },
    },
    {
      path: 'configurate',
      component: SystemConfigurateComponent,
      data: {
        permission: 'view',
        resource: 'admin'
      },
      resolve:{
        model:SystemConfigurateResolver
      }
    },
    {
      path: 'social-configuration',
      component: SocialConfigurationComponent,
      data: {
        permission: 'view',
        resource: 'admin'
      }
    },
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
