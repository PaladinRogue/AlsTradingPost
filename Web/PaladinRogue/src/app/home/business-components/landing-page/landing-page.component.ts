import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LandingPageComponent {
  public title: string = 'Paladin Rogue development';
}
