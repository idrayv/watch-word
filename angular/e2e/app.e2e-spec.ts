import { WatchWordTemplatePage } from './app.po';

describe('WatchWord App', function() {
  let page: WatchWordTemplatePage;

  beforeEach(() => {
    page = new WatchWordTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
