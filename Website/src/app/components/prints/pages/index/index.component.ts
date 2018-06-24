import { Component, OnInit } from '@angular/core';
import {PrintsData} from '../../Shared/model';
import {PrintsService} from '../../Shared/service';
import { TypesService } from '../../../../components/types/Shared/service';
import { CompanyService } from '../../../../components/company/Shared/service';
import { TypesData } from '../../../../components/types/Shared/model';
import { CompanyData } from '../../../../components/company/Shared/model';
import { MatDatepickerInputEvent } from '@angular/material';
import { serverConfig } from '../../../../config/api';
@Component({
  selector: 'Prints-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.less'],
  providers: [PrintsService, CompanyService, TypesService]
})
export class IndexPrintsComponent implements OnInit {

  items: PrintsData[] = [];
  types: TypesData[] = [];
  companies: CompanyData[] = [];
  filter: PrintsData = new PrintsData();
  page = 1;
  totalpages = 0;
  constructor(private PrintsService: PrintsService,
    private TypesService: TypesService,
    private CompanyService: CompanyService,
  ) { }

  ngOnInit() {
    this.filter.createdAt = null;
    this.filter.updatedAt = null;
    this.getData();
    this.getTypes();
    this.getCompanies();
  }

  getData() {
    this.PrintsService.getAll(this.page, this.filter).subscribe(data => {
      this.page = data.currentPage;
      this.totalpages = data.pageCount;
      this.items = data.results;
    });

  }
  getTypes() {
    this.TypesService.getForDDL().subscribe(data => {
      this.types = data;
    });
  }
  getCompanies() {
    this.CompanyService.getForDDL().subscribe(data => {
      this.companies = data;
    });
  }
  filterData() {
    this.getData();
}

goToPage(i: number)
{
    this.page = this.page + i;
    this.getData();
  }



ExportXls() {

  let from: string = "";
  let to: string = "";

  if (this.filter.createdAt)
    from = this.filter.createdAt.toJSON();

  if (this.filter.updatedAt)
    to = this.filter.updatedAt.toJSON();


    return serverConfig.apiUrl + 'Prints/Export?title=' + this.filter.title
      + "&companyId=" + this.filter.companyId
      + "&typeId=" + this.filter.typeId
      + "&createdAt=" + from
      + "&updatedAt=" + to;


    /* this.PrintsService.exportXls(this.filter).subscribe(data => {
    });*/
  }
}
