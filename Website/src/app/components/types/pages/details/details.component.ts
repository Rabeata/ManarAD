import { Component, OnInit } from '@angular/core';
import {TypesData} from '../../Shared/model';
import {TypesService} from '../../Shared/service';
import { ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'Types-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.less'],
  providers: [TypesService]
})
export class DetailsTypesComponent implements OnInit {

  item : TypesData = new TypesData();
  subscription: any;

  constructor(private TypesService: TypesService,
    private activatedRoute: ActivatedRoute,
    private location: Location) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.params.subscribe(
      (param: any) => {
        this.item.id = +param['id'];

        this.getData();
      });
  }

  getData() {
    this.TypesService.getOne(this.item.id).subscribe(data => {
      this.item = data;

    });

  }

  back() {
    this.location.back();
  }
}
