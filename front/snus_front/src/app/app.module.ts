import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from 'src/infrastructure/app-routing.module';
import { MaterialModule } from 'src/infrastructure/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { NavbarComponent } from './navbar/navbar.component';
import { ReportManagerComponent } from './report-manager/report-manager.component';
import { MatNativeDateModule } from '@angular/material/core';
import { DatabaseManagerComponent } from './database-manager/database-manager.component';
import { ChangeTagValueComponent } from './change-tag-value/change-tag-value.component';
import { AddTagComponent } from './add-tag/add-tag.component';
import { TrendingComponent } from './trending/trending.component';
import { InputTagValueSocketService } from './services/trending-input-value-ws.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavbarComponent,
    ReportManagerComponent,
    DatabaseManagerComponent,
    ChangeTagValueComponent,
    AddTagComponent,
    TrendingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    CommonModule,
    HttpClientModule,
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'outline', hideRequiredMarker: 'true' }}, InputTagValueSocketService 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
}
