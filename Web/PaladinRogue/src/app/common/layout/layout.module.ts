import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material';
import { SideDrawerLayoutComponent } from './business-components/side-drawer-layout/side-drawer-layout.component';

@NgModule({
  imports: [
    MatSidenavModule,
    CommonModule
  ],
  declarations: [
    SideDrawerLayoutComponent
  ],
  exports: [
    SideDrawerLayoutComponent
  ]
})
export class LayoutModule { }
