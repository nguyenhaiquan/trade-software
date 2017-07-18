import { Component, OnInit } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';

import { Api, AuthService } from '../../providers/auth-service';

import { HomePage } from '../home/home';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'page-order',
  templateUrl: 'order.html',
  providers: [DatePipe]
})
export class OrderPage implements OnInit {

  loading: Loading;

  account: any;

  order: any;
  stock: any;
  volume: any;
  type: any;
  price: any;
  expiry: any;
  stocklist: any;
  date: any;
  minDate: any;
    
  constructor(
    public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http,
    public auth: AuthService,
    public datepipe: DatePipe) {
    this.account = this.auth.getAccount();
    this.stocklist = this.auth.getStocklList();
  }

  ngOnInit() {
    this.date = new Date();
    this.minDate = this.datepipe.transform(this.date, 'yyyy-MM-dd');
    this.expiry = this.minDate;
  }

  public today() {
    return new Date().toISOString().substring(0,10);
  }

  public submit() {
    if (this.order == 'buy') {
      this.showLoading();
      this.http.post(new Api().api + 'Portfolio/Buy?buyStock=' + this.stock +
        '&investor=' + this.account +
        '&quantity=' + this.volume +
        '&price=' + this.price, null).subscribe(result => {
          if (result) {
            this.showMessage("Information", "Order matched");
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
            this.showMessage("Information", "Order matched");
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
