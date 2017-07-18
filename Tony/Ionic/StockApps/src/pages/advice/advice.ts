import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api } from '../../providers/auth-service';

@Component({
  selector: 'page-advice',
  templateUrl: 'advice.html'
})
export class AdvicePage {

  stockCode: any;
  currentPrice: any;
  targetPrice: any;
  cutlossPrice: any;
  explication: any;
  source: any;

  constructor(
    public navCtrl: NavController,
    public http: Http) {
    this.http.get(new Api().api + 'Advice').map(res => res.json()).subscribe(result => {
      this.stockCode = result.code;
      this.currentPrice = result.currentPrice;
      this.targetPrice = result.targetPrice;
      this.cutlossPrice = result.cutlossPrice;
      this.explication = result.explication;
      this.source = result.source;
    });
  }

}