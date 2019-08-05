import { LanguageService } from '../../services/language/language.service';
import { TranslateService } from '../../services/translate/translate.service';
import { TranslatePipe } from './translate.pipe';

describe('TranslatePipe', () => {
  let pipe: TranslatePipe,
    dependencies: {
      translateService: TranslateService,
      languageService: LanguageService
    };

  beforeEach(() => {
    dependencies = {
      translateService: jasmine.createSpyObj('translateService', []), // new TranslateServiceStub(),
      languageService: jasmine.createSpyObj('languageService', []), // new LanguageServiceStub()
    };

    pipe = new TranslatePipe(dependencies.translateService, dependencies.languageService);
  });

  it('create an instance', () => {
    expect(pipe).toBeTruthy();
  });
});
