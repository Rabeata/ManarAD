import { Component, OnInit } from '@angular/core';
import {TypesData} from '../../Shared/model';
import {TypesService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";


@Component({
  selector: 'Types-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.less'],
  providers: [TypesService]
})
export class EditTypesComponent implements OnInit {

  item : TypesData = new TypesData();
  subscription: any;
  flag = false;
  constructor(private TypesService: TypesService,
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
    this.TypesService.getOne(this.item.id).subscribe(data => {
      this.item = data;
      this.flag = true;
    });

  }




}
