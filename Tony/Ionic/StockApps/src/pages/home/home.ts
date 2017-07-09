import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { OrderPage } from '../order/order';
import { PortfolioPage } from '../portfolio/portfolio';
import { StockPage } from '../stock/stock';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})

export class HomePage {

  account: any;
  portfolios: any;

  constructor(
    public navCtrl: NavController,
    public http: Http, 
    public auth: AuthService) {
    this.account = this.auth.getAccount();
    this.http.get(new Api().api + 'Investment?investor=' + this.account).map(res => res.json()).subscribe(data => {
      this.portfolios = data;
    });
  }

  public navigate(code) {
    this.navCtrl.push(StockPage, {
      stockCode: code
    });
  }

  public order() {
    this.navCtrl.setRoot(OrderPage);
  }

  public detail() {
    this.navCtrl.push(PortfolioPage);
  }

}