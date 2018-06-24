import { Component, OnInit } from '@angular/core';
import {PrintsData} from '../../Shared/model';
import {PrintsService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";


@Component({
  selector: 'Prints-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.less'],
  providers: [PrintsService]
})
export class EditPrintsComponent implements OnInit {

  item : PrintsData = new PrintsData();
  subscription: any;
  flag = false;
  constructor(private PrintsService: PrintsService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.params.subscribe(
      (param: any) => {
        this.item.id = +param['id'];

        this.getData();
      });

  }

  getData ()
  {
    this.PrintsService.getOne(this.item.id).subscribe(data => {
      this.item = data;
      this.flag = true;
    });

  }




}
