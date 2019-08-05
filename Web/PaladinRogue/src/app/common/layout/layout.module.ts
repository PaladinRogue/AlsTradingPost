import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule, MatSidenavModule, MatTabsModule, MatToolbarModule } from '@angular/material';
import { InteractionModule } from '../interaction';
import { InternationalizationModule } from '../internationalization';
import { ActionsComponent } from './business-components/actions/actions.component';
import { CardHeaderComponent } from './business-components/card-header/card-header.component';
import { CardComponent } from './business-components/card/card.component';

import { ContentComponent } from './business-components/content/content.component';
import { DrawerComponent } from './business-components/drawer/drawer.component';
import { FooterComponent } from './business-components/footer/footer.component';
import { SideDrawerLayoutComponent } from './business-components/side-drawer-layout/side-drawer-layout.component';
import { TabGroupComponent } from './business-components/tab-group/tab-group.component';
import { ToolbarComponent } from './business-components/toolbar/toolbar.component';
import { TabComponent } from './business-components/tab/tab.component';

@NgModule({
  imports: [
    CommonModule,
    InteractionModule,
    InternationalizationModule.forChild(),
    MatCardModule,
    MatSidenavModule,
    MatTabsModule,
    MatToolbarModule
  ],
  declarations: [
    ActionsComponent,
    CardComponent,
    CardHeaderComponent,
    ContentComponent,
    DrawerComponent,
    FooterComponent,
    SideDrawerLayoutComponent,
    TabComponent,
    TabGroupComponent,
    ToolbarComponent
  ],
  exports: [
    ActionsComponent,
    CardComponent,
    CardHeaderComponent,
    ContentComponent,
    DrawerComponent,
    FooterComponent,
    SideDrawerLayoutComponent,
    TabComponent,
    TabGroupComponent,
    ToolbarComponent
  ]
})
export class LayoutModule {
}
