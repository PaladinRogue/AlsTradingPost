import { Component, ChangeDetectionStrategy, ContentChild } from '@angular/core';
import { ActionsComponent } from '../actions/actions.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'pr-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardComponent {
  @ContentChild(FooterComponent)
  public cardFooter: FooterComponent;

  @ContentChild(ActionsComponent)
  public cardActions: ActionsComponent;
}
