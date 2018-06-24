import { Component, OnInit } from '@angular/core';
import {TypesData} from '../../Shared/model';
import {TypesService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';


@Component({
  selector: 'Types-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.less'],
  providers: [TypesService]
})
export class DeleteTypesComponent implements OnInit {

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

  getData ()
  {
    this.TypesService.getOne(this.item.id).subscribe(data => {
      this.item = data;
    });

  }
  onDelete() {
    this.TypesService.deleteItem(this.item.id)
      .subscribe(data => {
       
        this.back();
      });
  }
  back() {
    this.location.back();
  }


}
