import { Injectable } from '@angular/core';
import { library } from '@fortawesome/fontawesome-svg-core';

import { IconRepository } from '../icon-repository.service';

@Injectable()
export class FontAwesomeIconRepository implements IconRepository {
  public addIcon(icon: any): void {
    library.add(icon);
  }
}
