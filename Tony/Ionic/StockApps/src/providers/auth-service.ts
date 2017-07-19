import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

export class User {
    account: string;
    password: string;
    code: string;
    name: string;

    constructor(account: string, password: string, code: string, name: string) {
        this.account = account;
        this.password = password;
        this.code = code;
        this.name = name;
    }
}

export class List {
    list: string[];

    constructor(list: string[]) {
        this.list = list;
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
    stockList: List;

    constructor(
        public http: Http) {

    }

    public getAccount(): string {
        return this.currentUser.account;
    }

    public getCode(): string {
        return this.currentUser.code;
    }

    public getName(): string {
        return this.currentUser.name;
    }

    public getStocklList(): string[] {
        return this.stockList.list;
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
                            this.currentUser = new User(account, password, result.code, result.name);
                            this.getData();
                            
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

    public getData() {
        this.http.get(
            new Api().api + 'Stock').map(res => res.json()).subscribe(data => {
                this.stockList = new List(data);
            });
    }
}