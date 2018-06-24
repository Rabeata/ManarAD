import { Component, OnInit } from '@angular/core';
import {CompanyData} from '../../Shared/model';
import {CompanyService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';


@Component({
  selector: 'Company-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.less'],
  providers: [CompanyService]
})
export class DeleteCompanyComponent implements OnInit {

  item : CompanyData = new CompanyData();
  subscription: any;
  constructor(private CompanyService: CompanyService,
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
    this.CompanyService.getOne(this.item.id).subscribe(data => {
      this.item = data;
    });

  }
  onDelete() {
    this.CompanyService.deleteItem(this.item.id)
      .subscribe(data => {
       
        this.back();
      });
  }
  back() {
    this.location.back();
  }


}
