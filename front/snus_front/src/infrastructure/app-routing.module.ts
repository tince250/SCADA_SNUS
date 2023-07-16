import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTagComponent } from 'src/app/add-tag/add-tag.component';
import { DatabaseManagerComponent } from 'src/app/database-manager/database-manager.component';
import { LoginComponent } from 'src/app/login/login.component';
import { ReportManagerComponent } from 'src/app/report-manager/report-manager.component';
import { TrendingComponent } from 'src/app/trending/trending.component';

const routes: Routes = [
  {path: "", component: TrendingComponent},
  {path: "login", component: LoginComponent},
  {path: "reports", component: ReportManagerComponent},
  {path: "database", component: DatabaseManagerComponent},
  {path: "add-tag", component: AddTagComponent},
  {path: "abc", component: DatabaseManagerComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
