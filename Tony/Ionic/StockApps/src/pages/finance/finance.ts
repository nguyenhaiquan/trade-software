import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

@Component({
  selector: 'page-finance',
  templateUrl: 'finance.html'
})
export class FinancePage {

  stockCode: any;
  financialData: any;

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    public http: Http,
    public auth: AuthService) {
    this.stockCode = navParams.get("stockCode");
    this.http.get(new Api().api + 'FinancialData?code=' + this.stockCode + '&time1=2015&time2=2016').map(res => res.json()).subscribe(data => {
      this.financialData = data;
    });
  }
}
