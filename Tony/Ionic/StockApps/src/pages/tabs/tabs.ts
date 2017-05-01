import { Component } from '@angular/core';

import { AdvicePage } from '../advice/advice';
import { AlarmPage } from '../alarm/alarm';
import { HomePage } from '../home/home';
import { WatchlistPage } from '../watchlist/watchlist';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {

  tab1Root = HomePage;
  tab2Root = WatchlistPage;
  tab3Root = AlarmPage;
  tab4Root = AdvicePage;

  constructor() {

  }
}
