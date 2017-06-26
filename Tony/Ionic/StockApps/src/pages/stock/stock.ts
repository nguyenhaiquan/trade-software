import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { ChartPage } from '../chart/chart';

@Component({
  selector: 'page-stock',
  templateUrl: 'stock.html'
})
export class StockPage {

  stockCode: any;
  financialData: any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public http: Http) {
    this.stockCode = navParams.get("stockCode");
    this.http.get('http://localhost:63471/api/FinancialData?code=' + this.stockCode + '&time1=2015&time2=2016').map(res => res.json()).subscribe(data => {
      this.financialData = data;
    });
  }

  public chart() {
    this.navCtrl.push(ChartPage, {
      stockCode: this.stockCode
    });
  }

}
