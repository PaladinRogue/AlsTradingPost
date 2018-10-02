import { ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { has } from 'lodash';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { ActionType } from './constants/action-type.constant';
import { IActionAction } from './interfaces/action-action.interface';
import { IActionRoute } from './interfaces/action-route.interface';
import { IAction } from './interfaces/action.interface';

export abstract class ActionComponent implements OnChanges {
  @Input()
  public prAction: IAction;

  public readonly actionType$: Observable<ActionType>;
  public actionTypes: typeof ActionType = ActionType;

  private readonly _actionTypeSubject: Subject<ActionType>;
  protected readonly _hostElement: ElementRef;

  protected constructor(hostElement: ElementRef) {
    this._hostElement = hostElement;

    this._actionTypeSubject = new BehaviorSubject<ActionType>(ActionType.NONE);
    this.actionType$ = this._actionTypeSubject.asObservable();
  }

  public get isRaised(): boolean {
    return this._hostElement.nativeElement.classList.contains('PrAction--raised');
  }

  public get colourPalette(): string {
    if (this._hostElement.nativeElement.classList.contains('PrAction--primary')) {
      return 'primary';
    } else if (this._hostElement.nativeElement.classList.contains('PrAction--accent')) {
      return 'accent';
    } else if (this._hostElement.nativeElement.classList.contains('PrAction--warning')) {
      return 'warn';
    }
    return '';
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (has(changes, 'prAction')) {
      this._actionTypeSubject.next(this._getActionType());
    }
  }

  public action(): void {
    if (this._isActionAction(this.prAction)) {
      this.prAction.action();
    }
  }

  private _getActionType(): ActionType {
    if (this._isActionRoute(this.prAction)) {
      return ActionType.ROUTE;
    } else if (this._isActionAction(this.prAction)) {
      return ActionType.ACTION;
    }

    return ActionType.NONE;
  }

  private _isActionRoute(action: IAction): action is IActionRoute {
    return !!(action as IActionRoute).route;
  }

  private _isActionAction(action: IAction): action is IActionAction {
    return !!(action as IActionAction).action;
  }
}
