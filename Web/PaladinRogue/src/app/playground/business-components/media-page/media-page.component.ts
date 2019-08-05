import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-media-page',
  templateUrl: './media-page.component.html',
  styleUrls: ['./media-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MediaPageComponent {
}
