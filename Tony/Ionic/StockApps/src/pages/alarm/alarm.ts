import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';

import { AuthService } from '../../providers/auth-service';

import { AlarmlistPage } from '../alarmlist/alarmlist'

@Component({
  selector: 'page-alarm',
  templateUrl: 'alarm.html'
})
export class AlarmPage {

  loading: Loading;

  account: any;
  stock: any;
  type: any;
  condition: any;
  value: any;
  comment: any;
  status: any;
  expiry: any;
  notification: any;

  constructor(public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http, 
    public auth: AuthService) {
    this.account = this.auth.getAccount();

  }

  public create() {
    this.showLoading()
    this.http.post('http://localhost:63471/api/Alarm?stock=' + this.stock +
      '&investor=' + this.account +
      '&type=' + this.type +
      '&condition=' + this.condition +
      '&value=' + this.value + 
      '&comment=' + this.comment +
      '&status=' + this.status +
      '&expiry=' + this.expiry +
      '&notification=' + this.notification, null).subscribe(result => {
        if (result) {
          this.showMessage("Information", "Alarm saved");
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
    this.navCtrl.setRoot(AlarmlistPage);
  }

}
