import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { StockPage } from '../stock/stock';

@Component({
  selector: 'page-alarmlist',
  templateUrl: 'alarmlist.html'
})
export class AlarmlistPage {

  alarms: any;

  constructor(public navCtrl: NavController, public http: Http) {
    this.http.get('http://localhost:63471/api/Alarm?investor=test').map(res => res.json()).subscribe(data => {
      this.alarms = data;
    });
  }

  public navigate(code) {
    this.navCtrl.push(StockPage, {
      stockCode: code
    });
  }

}
