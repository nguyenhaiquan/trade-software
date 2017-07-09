import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';

import { Api, AuthService } from '../../providers/auth-service';

import { HomePage } from '../home/home';

@Component({
  selector: 'page-order',
  templateUrl: 'order.html'
})
export class OrderPage {

  loading: Loading;

  account: any;
  order: any;
  stock: any;
  volume: any;
  type: any;
  price: any;
  expiry: any;

  constructor(
    public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http,
    public auth: AuthService) {
    this.account = this.auth.getAccount();

  }

  public submit() {
    if (this.order == 'buy') {
      this.showLoading();
      this.http.post(new Api().api + 'Portfolio/Buy?buyStock=' + this.stock +
        '&investor=' + this.account +
        '&quantity=' + this.volume +
        '&price=' + this.price, null).subscribe(result => {
          if (result) {
            this.showMessage("Information", "Order saved");
          } else {
            this.showMessage("Error", "Database connection fail");
          }
        },
        error => {
          this.showMessage("Error", error);
        });
    } else { // sell
      this.showLoading();
      this.http.post(new Api().api + 'Portfolio/Sell?sellStock=' + this.stock +
        '&investor=' + this.account +
        '&quantity=' + this.volume +
        '&price=' + this.price, null).subscribe(result => {
          if (result) {
            this.showMessage("Information", "Order saved");
          } else {
            this.showMessage("Error", "Database connection fail");
          }
        },
        error => {
          this.showMessage("Error", error);
        });
    }

  }

  showLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Please wait...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  showMessage(header, content) {
    this.loading.dismiss();

    let alert = this.alertCtrl.create({
      title: header,
      subTitle: content,
      buttons: ['OK']
    });
    alert.present(prompt);
  }

  cancel() {
    this.navCtrl.setRoot(HomePage);
  }

}
