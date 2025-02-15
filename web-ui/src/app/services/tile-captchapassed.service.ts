import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TileCAPTCHAPassedService {
  constructor() { }

  private stateSource = new BehaviorSubject<boolean>(false);
  currentState = this.stateSource.asObservable();

  updateState(newValue: boolean) {
    this.stateSource.next(newValue);
  }
}
