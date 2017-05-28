import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { AuthService } from '../../providers/auth-service';

import { StockPage } from '../stock/stock';
import { AlarmPage } from '../alarm/alarm';

@Component({
  selector: 'page-alarmlist',
  templateUrl: 'alarmlist.html'
})
export class AlarmlistPage {

  account: any;
  alarms: any;

  constructor(public navCtrl: NavController, public http: Http, 
    public auth: AuthService) {
    this.account = this.auth.getAccount();
    this.http.get('http://localhost:63471/api/Alarm?investor=' + this.account).map(res => res.json()).subscribe(data => {
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

}
