import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  portfolios: any;

  constructor(public navCtrl: NavController, public http: Http) {

    this.http.get('http://localhost:63471/api/Portfolio?investor=test').map(res => res.json()).subscribe(data => {
      this.portfolios = data;
    });
  }

}