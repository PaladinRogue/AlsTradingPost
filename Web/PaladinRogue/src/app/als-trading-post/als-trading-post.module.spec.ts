import { AlsTradingPostModule } from './als-trading-post.module';

describe('AlsTradingPostModule', () => {
  let alsTradingPostModule: AlsTradingPostModule;

  beforeEach(() => {
    alsTradingPostModule = new AlsTradingPostModule();
  });

  it('should create an instance', () => {
    expect(alsTradingPostModule).toBeTruthy();
  });
});
