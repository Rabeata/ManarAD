import { Component, OnInit } from '@angular/core';
import {CompanyData} from '../../Shared/model';
import {CompanyService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";


@Component({
  selector: 'Company-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.less'],
  providers: [CompanyService]
})
export class EditCompanyComponent implements OnInit {

  item : CompanyData = new CompanyData();
  subscription: any;
  flag = false;
  constructor(private CompanyService: CompanyService,
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
    this.CompanyService.getOne(this.item.id).subscribe(data => {
      this.item = data;
      this.flag = true;
    });

  }




}
