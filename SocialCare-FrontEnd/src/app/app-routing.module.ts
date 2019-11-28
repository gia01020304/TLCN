import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './@guard/auth-guard/auth-guard.service';


const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService], 
    loadChildren: () => import('./pages/pages.module')
      .then(m => m.PagesModule),

  },
  {
    path: 'auth',
    loadChildren: './@auth/auth.module#AuthModule',
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
