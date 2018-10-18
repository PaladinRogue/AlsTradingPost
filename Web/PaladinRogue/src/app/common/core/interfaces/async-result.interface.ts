import { Observable } from 'rxjs';

export type IAysnycResult<T> = Observable<T> | Promise<T> | T;
