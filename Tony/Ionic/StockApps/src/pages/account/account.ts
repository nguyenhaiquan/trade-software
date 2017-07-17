import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { AuthService } from '../../providers/auth-service';

import { LoginPage } from '../login/login';

@Component({
  selector: 'page-account',
  templateUrl: 'account.html'
})

export class AccountPage {
  username: string;
  information: string;

  constructor(
    public navCtrl: NavController,
    public auth: AuthService) {
      if (typeof this.auth.currentUser === 'undefined' || this.auth.currentUser === null) {
        this.username = ''; // show nothing
        this.information = 'You need to login!';
      }
      else {
        this.username = this.auth.getName();
        this.information = ''; // show nothing
      }
  }

  public logout() {
    this.auth.logout().subscribe(success => {
      this.navCtrl.setRoot(LoginPage);
    });
  }

}
