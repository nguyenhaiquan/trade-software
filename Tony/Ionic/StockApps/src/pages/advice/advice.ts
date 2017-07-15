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

  advide = {'code' : '', 'current' : 0, 'target' : 0, 'cutloss' : 0, 'explication' : '', 'source' : ''};

  constructor(
    public navCtrl: NavController,
    public http: Http) {
    this.http.get(new Api().api + 'Advide').map(res => res.json()).subscribe(data => {
      this.advide = data;
      console.log(this.advide);
    });
  }

}