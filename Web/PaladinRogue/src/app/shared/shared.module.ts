import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { ApiService } from './services/api.service';

@NgModule({
  declarations: [],
  imports: [
    HttpClientModule
  ],
  providers: [
    ApiService
  ],
  bootstrap: []
})
export class SharedModule {
}
