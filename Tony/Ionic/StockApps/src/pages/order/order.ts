import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';

import { TabsPage } from '../tabs/tabs';

@Component({
  selector: 'page-order',
  templateUrl: 'order.html'
})
export class OrderPage {

  loading: Loading;

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
    public http: Http) {

  }

  public submit() {
    this.showLoading()
    this.http.post('http://localhost:63471/api/Portfolio?stock=' + this.stock +
      '&investor=test' +
      '&quantity=' + this.volume +
      '&price=' + this.price, null).subscribe(result => {
        this.navCtrl.setRoot(TabsPage);
      },
      error => {
        this.showError(error);
      });
  }

  showLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Please wait...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  showError(text) {
    this.loading.dismiss();

    let alert = this.alertCtrl.create({
      title: 'Fail',
      subTitle: text,
      buttons: ['OK']
    });
    alert.present(prompt);
  }

  cancel() {
    this.navCtrl.setRoot(TabsPage);
  }

}
