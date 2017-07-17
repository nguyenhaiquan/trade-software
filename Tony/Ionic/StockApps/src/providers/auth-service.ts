import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

export class User {
    account: string;
    password: string;
    name: string;
    code: string;

    constructor(account: string, password: string, name: string, code: string) {
        this.account = account;
        this.password = password;
        this.name = name;
        this.code = code;
    }
}

@Injectable()
export class Api {
    //api = "http://localhost:63471/api/";
    api = "http://qstockmobile.azurewebsites.net/api/";
    constructor() {

    }
}

@Injectable()
export class AuthService {
    currentUser: User;

    constructor(
        public http: Http) {    

    }

    public getAccount(): string {
        return this.currentUser.account;
    }

    public getName(): string {
        return this.currentUser.name;
    }

    public getCode(): string {
        return this.currentUser.code;
    }

    public login(account, password) {
        if (account === null || password === null) {
            return Observable.throw("Please insert credentials");
        } else {
            return Observable.create(observer => {
                this.http.get(
                    new Api().api + 'Investor/LogIn?account='
                    + account + '&password='
                    + password).map(res => res.json()).subscribe(result => {
                        if (result === null) {
                            observer.next(false);
                            observer.complete();
                        }
                        else {
                            this.currentUser = new User(account, password, result.name, result.code);
                            observer.next(true);
                            observer.complete();
                        }
                    },
                    error => {
                        console.log(error)
                    });
            });
        }
    }

    public logout() {
        return Observable.create(observer => {
            this.currentUser = null;
            observer.next(true);
            observer.complete();
        });
    }
}