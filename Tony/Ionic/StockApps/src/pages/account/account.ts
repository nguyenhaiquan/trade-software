import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api, AuthService } from '../../providers/auth-service';

import { LoginPage } from '../login/login';

@Component({
  selector: 'page-account',
  templateUrl: 'account.html'
})

export class AccountPage {
  username: any;
  notification: any;
  startAmount: any;
  currentAmount: any;
  buttonName: any;

  constructor(
    public navCtrl: NavController,
    public http: Http,
    public auth: AuthService) {
      if (typeof this.auth.currentUser === 'undefined' || this.auth.currentUser === null) {
        this.username = ''; // show nothing
        this.notification = 'You need to login!';
        this.startAmount = ''; // show nothing
        this.currentAmount = ''; // show nothing
        this.buttonName = 'Quit';
      }
      else {
        this.http.get(new Api().api + 'Investor/GetAsset?code=' + this.auth.getCode()).map(res => res.json()).subscribe(data => {
          this.startAmount = 'Starting capital : ' + data[0] + ' VND';
          this.currentAmount = 'Withdrawable amount : ' + data[1] + ' VND';
        });
        
        this.username = this.auth.getName();
        this.notification = ''; // show nothing
        this.buttonName = 'Logout';
      }
  }

  public logout() {
    this.auth.logout().subscribe(success => {
      this.navCtrl.setRoot(LoginPage);
    });
  }

}
