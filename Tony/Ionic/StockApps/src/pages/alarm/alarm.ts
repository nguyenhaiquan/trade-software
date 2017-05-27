import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { HomePage } from '../home/home'

@Component({
  selector: 'page-alarm',
  templateUrl: 'alarm.html'
})
export class AlarmPage {

  code: '';
  type: '';
  codition: '';
  value: '';
  comment: '';
  expiry: '';
  notification: '';

  constructor(public navCtrl: NavController) {

  }

  public create() {
    
  }

  public register() {
    
  }

}
