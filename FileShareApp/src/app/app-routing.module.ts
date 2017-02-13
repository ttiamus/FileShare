import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  { path: 'upload', loadChildren: 'app/upload/upload.module#UploadModule' },
  { path: 'download', loadChildren: 'app/download/download.module#DownloadModule' },
  { path: 'dashboard', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
  //{ path: 'dashboard', component:  DashboardComponent},
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' },
];

@NgModule({
    //should setup preloading
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }

//export const routedComponents = [NameComponent];