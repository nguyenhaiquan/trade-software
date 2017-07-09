import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { StockPage } from '../stock/stock';
import { AlarmPage } from '../alarm/alarm';

@Component({
  selector: 'page-alarmlist',
  templateUrl: 'alarmlist.html'
})
export class AlarmlistPage {

  loading: Loading;

  account: any;
  alarms: any;

  constructor(
    public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http, 
    public auth: AuthService) {
    this.account = this.auth.getAccount();
    this.http.get(new Api().api + 'Alarm?investor=' + this.account).map(res => res.json()).subscribe(data => {
      this.alarms = data;
    });
  }

  public navigate(code) {
    this.navCtrl.push(StockPage, {
      stockCode: code
    });
  }

  public create() {
    this.navCtrl.setRoot(AlarmPage);
  }

  public delete(stockCode) {
    this.showLoading();
    this.http.post(new Api().api + 'Alarm/Delete?stock=' + stockCode +
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

}
