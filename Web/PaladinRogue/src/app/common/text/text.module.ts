import { NgModule } from '@angular/core';
import { InternationalizationModule } from '../internationalization';
import { NoContentComponent } from './presentation-components/no-content/no-content.component';
import { CodeBlockComponent } from './presentation-components/code-block/code-block.component';

@NgModule({
  imports: [
    InternationalizationModule.forChild()
  ],
  declarations: [
    NoContentComponent,
    CodeBlockComponent
  ],
  exports: [
    NoContentComponent,
    CodeBlockComponent
  ],
  providers: []
})
export class TextModule {
}
