import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material';
import { InteractionModule } from '../interaction/interaction.module';

import { DrawerContentComponent } from './business-components/drawer-content/drawer-content.component';
import { SideDrawerLayoutComponent } from './business-components/side-drawer-layout/side-drawer-layout.component';
import { DrawerComponent } from './business-components/drawer/drawer.component';

@NgModule({
  imports: [
    InteractionModule,
    MatSidenavModule,
    CommonModule
  ],
  declarations: [
    SideDrawerLayoutComponent,
    DrawerContentComponent,
    DrawerComponent
  ],
  exports: [
    SideDrawerLayoutComponent,
    DrawerContentComponent,
    DrawerComponent
  ]
})
export class LayoutModule { }
