import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';

import { IAysnycResult } from '../../../core/interfaces/async-result.interface';
import { UnsavedChangesService } from '../../services/unsaved-changes/unsaved-changes.service';

@Injectable({
  providedIn: 'root'
})
export class UnsavedChangesGuard implements CanDeactivate<void> {
  private readonly _unsavedChangesService: UnsavedChangesService;

  public constructor(unsavedChangesService: UnsavedChangesService) {
    this._unsavedChangesService = unsavedChangesService;
  }

  public canDeactivate(): IAysnycResult<boolean> {
    if (this._unsavedChangesService.any()) {
      return this._unsavedChangesService.prompt();
    }

    return true;
  }
}
