import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-chart-doughnut',
  templateUrl: './chart-doughnut.component.html',
  styles: []
})
export class ChartDoughnutComponent implements OnInit {
  @Input() labels:string[];
  @Input() chartData:number[];
  
  constructor() { }

  ngOnInit() {

  }

}
