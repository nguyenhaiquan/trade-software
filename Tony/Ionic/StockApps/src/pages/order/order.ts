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

  cancel() {
    this.navCtrl.setRoot(TabsPage);
  }

}
