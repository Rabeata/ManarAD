import { Component, OnInit } from '@angular/core';
import {TypesData} from '../../Shared/model';
import {TypesService} from '../../Shared/service';
@Component({
  selector: 'Types-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.less'],
  providers: [TypesService]
})
export class IndexTypesComponent implements OnInit {

  items:TypesData[] = [];
filter : TypesData = new TypesData();
page = 1;
totalpages = 0;
constructor(private TypesService: TypesService) { }

  ngOnInit() {
    this.getData();
  }

  getData ()
  {
     this.TypesService.getAll(this.page,this.filter).subscribe(data=>{
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
