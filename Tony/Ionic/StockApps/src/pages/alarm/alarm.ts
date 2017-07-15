import { Component, OnInit } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading } from 'ionic-angular';
import { Http } from '@angular/http';

import { Api, AuthService } from '../../providers/auth-service';

import { AlarmlistPage } from '../alarmlist/alarmlist';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'page-alarm',
  templateUrl: 'alarm.html',
  providers: [DatePipe]
})
export class AlarmPage implements OnInit {

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
  date: any;
  minDate: any;

  constructor(public navCtrl: NavController,
    public alertCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public http: Http, 
    public auth: AuthService,
    public datepipe: DatePipe) {
    this.account = this.auth.getAccount();
  }

  ngOnInit() {
    this.date = new Date();
    this.minDate = this.datepipe.transform(this.date, 'yyyy-MM-dd');
    this.expiry = this.minDate;
  }

  public today() {
    return new Date().toISOString().substring(0,10);
  }

  public create() {
    this.showLoading();
    this.http.post(new Api().api + 'Alarm/Insert?stock=' + this.stock +
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
