import { PlaygroundModule } from './playground.module';

describe('PlaygroundModule', () => {
  let playgroundModule: PlaygroundModule;

  beforeEach(() => {
    playgroundModule = new PlaygroundModule();
  });

  it('should create an instance', () => {
    expect(playgroundModule).toBeTruthy();
  });
});
