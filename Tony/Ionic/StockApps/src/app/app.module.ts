import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';

import { MyApp } from './app.component';

import { AdvicePage } from '../pages/advice/advice';
import { AlarmPage } from '../pages/alarm/alarm';
import { HomePage } from '../pages/home/home';
import { LoginPage } from '../pages/login/login';
import { OrderPage } from '../pages/order/order';
import { RegisterPage } from '../pages/register/register';
import { TabsPage } from '../pages/tabs/tabs';
import { WatchlistPage } from '../pages/watchlist/watchlist';

import { AuthService } from '../providers/auth-service';

@NgModule({
  declarations: [
    MyApp,
    AdvicePage,
    AlarmPage,
    HomePage,
    LoginPage,
    OrderPage,
    RegisterPage,
    TabsPage,
    WatchlistPage
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    AdvicePage,
    AlarmPage,
    HomePage,
    LoginPage,
    OrderPage,
    RegisterPage,
    TabsPage,
    WatchlistPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    AuthService,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class AppModule {}
