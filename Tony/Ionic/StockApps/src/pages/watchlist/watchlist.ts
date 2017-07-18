import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { StockPage } from '../stock/stock';

@Component({
  selector: 'page-watchlist',
  templateUrl: 'watchlist.html'
})

export class WatchlistPage {

  loading: Loading;

  account: any;
  stock: any;
  stocklist: any;
  watchlist: any;

  constructor(
    public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http, 
    public auth: AuthService) {
    this.account = this.auth.getAccount();
    this.stocklist = this.auth.getStocklList();
    this.http.get(new Api().api + 'Watchlist?investor=' + this.account).map(res => res.json()).subscribe(data => {
      this.watchlist = data;
    });
  }

  public add() {
    this.showLoading();
    this.http.post(new Api().api + 'Watchlist/Insert?stockI=' + this.stock +
      '&investor=' + this.account, null).subscribe(result => {
        if (result) {
          this.navCtrl.setRoot(this.navCtrl.getActive().component);
        } else {
          this.showMessage("Error", "Database connection fail");
        }
      },
      error => {
        this.showMessage("Error", error);
      });
  }

  showLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Please wait...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  showMessage(title, content) {
    this.loading.dismiss();

    let alert = this.alertCtrl.create({
      title: title,
      subTitle: content,
      buttons: ['OK']
    });
    alert.present(prompt);
  }

  public navigate(code) {
    this.navCtrl.push(StockPage, {
      stockCode: code
    });
  }

  public delete(stockCode) {
    this.showLoading();
    this.http.post(new Api().api + 'Watchlist/Delete?stockD=' + stockCode +
      '&investor=' + this.account, null).subscribe(result => {
        if (result) {
          this.navCtrl.setRoot(this.navCtrl.getActive().component);
        } else {
          this.showMessage("Error", "Database connection fail");
        }
      },
      error => {
        this.showMessage("Error", error);
      });    
  }

}
