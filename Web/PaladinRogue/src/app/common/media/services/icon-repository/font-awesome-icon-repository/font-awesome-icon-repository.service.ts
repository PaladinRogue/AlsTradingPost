import { Injectable } from '@angular/core';
import { IconDefinition, IconPrefix, IconName, library } from '@fortawesome/fontawesome-svg-core';
import { map } from 'lodash';

import { IconRepository } from '../icon-repository.service';
import { IIconDefinition } from '../interfaces/icon-definition.interface';

@Injectable()
export class FontAwesomeIconRepository implements IconRepository {
  public addIcon(...icons: Array<IIconDefinition>): void {
    const faIcons: Array<IconDefinition> = map(icons, (icon: IIconDefinition): IconDefinition => {
      return {
        icon: icon.icon,
        iconName: this._formatIconName(icon.iconName),
        prefix: this._formatPrefix(icon.prefix)
      };
    });

    faIcons.forEach((faIcon: IconDefinition) => {
      library.add(faIcon);
    });
  }

  private _formatPrefix(prefix: string): IconPrefix {
    if (this._isIconPrefix(prefix)) {
      return prefix;
    }

    return 'far';
  }

  private _isIconPrefix(prefix: string): prefix is IconPrefix {
    return typeof prefix === 'string';
  }

  private _formatIconName(iconName: string): IconName {
    if (this._isIconName(iconName)) {
      return iconName;
    }

    return null;
  }

  private _isIconName(iconName: string): iconName is IconName {
    return typeof iconName === 'string';
  }
}
