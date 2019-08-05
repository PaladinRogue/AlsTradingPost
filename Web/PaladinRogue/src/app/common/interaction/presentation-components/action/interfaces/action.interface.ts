import { IActionAction } from './action-action.interface';
import { IActionRoute } from './action-route.interface';

export type IAction = IActionRoute | IActionAction;
