import { Component, ViewChild } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Chart } from 'chart.js';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Api } from '../../providers/auth-service';

@Component({
    selector: 'page-chart',
    templateUrl: 'chart.html'
})
export class ChartPage {

    @ViewChild('barCanvas') barCanvas;
    @ViewChild('lineCanvas') lineCanvas;

    stockCode: any;
    days: any;

    dates: any;
    prices: any;
    volumes: any;

    barChart: any;
    lineChart: any;

    range = { 'max': 50, 'min': 10, 'step': 10 };

    constructor(
        public navCtrl: NavController,
        public navParams: NavParams,
        public http: Http) {
        this.stockCode = navParams.get("stockCode");
        this.days = this.range.min;
        this.http.get(new Api().api + 'TradeHistory?stock=' + this.stockCode + '&days=' + this.range.max).map(res => res.json()).subscribe(data => {
            // get data
            this.dates = data.map(x => x.date);
            this.prices = data.map(x => x.price);
            this.volumes = data.map(x => x.volume);

            // reverse array
            this.dates.reverse();
            this.prices.reverse();
            this.volumes.reverse();

            // draw chart
            this.drawChart();
        });
    }

    drawChart() {
        this.barChart =null; // reset
        this.barChart = new Chart(this.barCanvas.nativeElement, {
            type: 'bar',
            data: {
                labels: this.dates.slice(-this.days),
                datasets: [{
                    label: 'Volume',
                    data: this.volumes.slice(-this.days),
                    backgroundColor: "rgba(0, 0, 0, 1)",
                    borderColor: "rgba(0, 0, 0, 1)",
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

        this.lineChart = null; // reset
        this.lineChart = new Chart(this.lineCanvas.nativeElement, {
            type: 'line',
            data: {
                labels: this.dates.slice(-this.days),
                datasets: [
                    {
                        label: "Price",
                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: "rgba(0, 0, 0, 1)",
                        borderColor: "rgba(0, 0, 0, 1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(0, 0, 0, 1)",
                        pointBackgroundColor: "rgba(0, 0, 0, 1)",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(0, 0, 0, 1)",
                        pointHoverBorderColor: "rgba(0, 0, 0, 1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: this.prices.slice(-this.days),
                        spanGaps: false,
                    }
                ]
            }
        });
    }
}