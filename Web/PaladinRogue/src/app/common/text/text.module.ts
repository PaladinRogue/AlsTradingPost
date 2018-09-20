import { NgModule } from '@angular/core';
import { InternationalizationModule } from '../internationalization';
import { NoContentComponent } from './presentation-components/no-content/no-content.component';

@NgModule({
  imports: [
    InternationalizationModule.forChild()
  ],
  declarations: [
    NoContentComponent
  ],
  exports: [
    NoContentComponent
  ],
  providers: []
})
export class TextModule {
}
