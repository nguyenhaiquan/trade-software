import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';

import { MyApp } from './app.component';

import { AccountPage } from '../pages/account/account';
import { AdvicePage } from '../pages/advice/advice';
import { AlarmPage } from '../pages/alarm/alarm';
import { AlarmlistPage } from '../pages/alarmlist/alarmlist';
import { ChartPage } from '../pages/chart/chart'
import { HomePage } from '../pages/home/home';
import { LoginPage } from '../pages/login/login';
import { OrderPage } from '../pages/order/order';
import { PortfolioPage } from '../pages/portfolio/portfolio';
import { RegisterPage } from '../pages/register/register';
import { SettingPage } from '../pages/setting/setting';
import { StockPage } from '../pages/stock/stock'
import { TabsPage } from '../pages/tabs/tabs';
import { WatchlistPage } from '../pages/watchlist/watchlist';

import { DatePickerModule } from 'datepicker-ionic2';
import { DatePipe } from '@angular/common';

import { AuthService } from '../providers/auth-service';

@NgModule({
  declarations: [
    MyApp,
    AccountPage,
    AdvicePage,
    AlarmPage,
    AlarmlistPage,
    ChartPage,
    HomePage,
    LoginPage,
    OrderPage,
    PortfolioPage,
    RegisterPage,
    SettingPage,
    StockPage,
    TabsPage,
    WatchlistPage
  ],
  imports: [
    BrowserModule,
    HttpModule,
    DatePickerModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    AccountPage,
    AdvicePage,
    AlarmPage,
    AlarmlistPage,
    ChartPage,
    HomePage,
    LoginPage,
    OrderPage,
    PortfolioPage,
    RegisterPage,
    SettingPage,
    StockPage,
    TabsPage,
    WatchlistPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    AuthService,
    {
      provide: ErrorHandler,
      useClass: IonicErrorHandler
    },
    DatePipe
  ]
})
export class AppModule {}
