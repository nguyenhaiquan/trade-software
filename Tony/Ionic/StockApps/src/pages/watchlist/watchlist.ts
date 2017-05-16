import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Component({
  selector: 'page-watchlist',
  templateUrl: 'watchlist.html'
})

export class WatchlistPage {

  watchlists: any;

  constructor(public navCtrl: NavController, public http: Http) {
        this.http.get('http://localhost:63471/api/Watchlist?investor=test').map(res => res.json()).subscribe(data => {
          this.watchlists = data;
        });
  }

}
