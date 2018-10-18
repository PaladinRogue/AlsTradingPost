import { Directive, Input, OnDestroy, OnInit } from '@angular/core';
import { Form } from '../services/form/form.service';
import { UnsavedChangesService } from '../services/unsaved-changes/unsaved-changes.service';

@Directive({
  selector: '[prForm]'
})
export class FormDirective implements OnInit, OnDestroy {
  @Input()
  public prForm: Form;

  private readonly _unsavedChangesService: UnsavedChangesService;

  public constructor(unsavedChangesService: UnsavedChangesService) {
    this._unsavedChangesService = unsavedChangesService;
  }

  public ngOnInit(): void {
    this._unsavedChangesService.registerForm(this.prForm);
  }

  public ngOnDestroy(): void {
    this._unsavedChangesService.deregisterForm(this.prForm);
  }
}
