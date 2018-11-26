import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-chart-doughnut',
  templateUrl: './chart-doughnut.component.html',
  styles: []
})
export class ChartDoughnutComponent implements OnInit {
  @Input() labels:string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
  @Input() chartData:number[] = [350, 450, 100];
  public chartType : string = "doughnut";
  
  constructor() { }

  ngOnInit() {

  }

}
