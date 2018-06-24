import { Component, OnInit } from '@angular/core';
import {CompanyData} from '../../Shared/model';
import {CompanyService} from '../../Shared/service';
@Component({
  selector: 'Company-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.less'],
  providers: [CompanyService]
})
export class IndexCompanyComponent implements OnInit {

  items:CompanyData[] = [];
filter : CompanyData = new CompanyData();
page = 1;
totalpages = 0;
constructor(private CompanyService: CompanyService) { }

  ngOnInit() {
    this.getData();
  }

  getData ()
  {
     this.CompanyService.getAll(this.page,this.filter).subscribe(data=>{
      this.page = data.currentPage;
      this.totalpages = data.pageCount;
        this.items = data.results;
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

}
