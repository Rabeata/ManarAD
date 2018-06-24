import { Component, OnInit } from '@angular/core';
import {CompanyData} from '../../Shared/model';
import {CompanyService} from '../../Shared/service';
import { ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'Company-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.less'],
  providers: [CompanyService]
})
export class DetailsCompanyComponent implements OnInit {

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

  getData() {
    this.CompanyService.getOne(this.item.id).subscribe(data => {
      this.item = data;

    });

  }

  back() {
    this.location.back();
  }
}
