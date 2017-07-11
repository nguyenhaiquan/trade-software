import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { ChartPage } from '../chart/chart';

@Component({
  selector: 'page-stock',
  templateUrl: 'stock.html'
})
export class StockPage {

  beginTime: any;
  endTime: any;
  stockCode: any;
  financialData: any;

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    public http: Http,
    public auth: AuthService) {
    this.beginTime = 2015;
    this.endTime = 2016;
    this.stockCode = navParams.get("stockCode");
    this.http.get(new Api().api + 'FinancialData?code=' + this.stockCode 
    + '&time1=' + this.beginTime + '&time2=' + this.endTime).map(res => res.json()).subscribe(data => {
      this.financialData = data;
    });
  }

  public chart() {
    this.navCtrl.push(ChartPage, {
      stockCode: this.stockCode
    });
  }

}
