import { Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';

import { IconRepository } from '../icon-repository.service';
import { IIconDefinition } from '../interfaces/icon-definition.interface';

@Injectable()
export class MaterialIconRepository implements IconRepository {
  private readonly _matIconRegistry: MatIconRegistry;

  public constructor(matIconRegistry: MatIconRegistry) {
    this._matIconRegistry = matIconRegistry;
  }

  public addIcon(...icons: Array<IIconDefinition>): void {
    icons.forEach((iconDefinition: IIconDefinition) => {
      this._matIconRegistry.addSvgIconLiteral(iconDefinition.iconName, iconDefinition.icon.value);
    });
  }
}
