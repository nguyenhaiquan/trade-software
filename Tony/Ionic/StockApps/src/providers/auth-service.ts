import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthService {

    constructor(public http: Http) {

    }

    public login(account, password) {
        if (account === null || password === null) {
            return Observable.throw("Please insert credentials");
        } else {
            return Observable.create(observer => {
                this.http.get(
                    'http://localhost:63471/api/Investor/LogIn?account='
                    + account + '&password='
                    + password).map(res => res.json()).subscribe(result => {
                        console.log(result);
                        if (result === true) {
                            observer.next(true);
                            observer.complete();
                        }
                        else {
                            observer.next(false);
                            observer.complete();
                        }
                    },
                    error => {
                        console.log(error)
                    });
            });
        }
    }
}