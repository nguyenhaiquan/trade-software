import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { ChartPage } from '../chart/chart';
import { FinancePage } from '../finance/finance';

@Component({
  selector: 'page-stock',
  templateUrl: 'stock.html'
})
export class StockPage {

  stockCode: any;
  stockName: any;
  openPrice: any;
  closePrice: any;
  lowPrice: any;
  highPrice: any;
  volume: any;
  PB: any;
  PE: any;
  EPS: any;
  ROA: any;
  ROE: any;
  BETA: any;

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    public http: Http,
    public auth: AuthService) {
    this.stockCode = navParams.get("stockCode");
    this.http.get(new Api().api + 'Stock?code=' + this.stockCode).map(res => res.json()).subscribe(data => {
      this.stockName = data.name;
      this.openPrice = data.openPrice;
      this.closePrice = data.closePrice;
      this.lowPrice = data.lowPrice;
      this.highPrice = data.highPrice;
      this.volume = data.volume;
      this.PB = data.PB;
      this.PE = data.PE;
      this.EPS = data.EPS;
      this.ROA = data.ROA;
      this.ROE = data.ROE;
      this.BETA = data.BETA;
    });
  }

  public chart() {
    this.navCtrl.push(ChartPage, {
      stockCode: this.stockCode
    });
  }

  public finance() {
    this.navCtrl.push(FinancePage, {
      stockCode: this.stockCode
    });
  }
}
