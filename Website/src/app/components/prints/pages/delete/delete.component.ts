import { Component, OnInit } from '@angular/core';
import {PrintsData} from '../../Shared/model';
import {PrintsService} from '../../Shared/service';
import { Router, ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';


@Component({
  selector: 'Prints-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.less'],
  providers: [PrintsService]
})
export class DeletePrintsComponent implements OnInit {

  item : PrintsData = new PrintsData();
  subscription: any;
  constructor(private PrintsService: PrintsService,
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
    this.PrintsService.getOne(this.item.id).subscribe(data => {
      this.item = data;
    });

  }
  onDelete() {
    this.PrintsService.deleteItem(this.item.id)
      .subscribe(data => {
       
        this.back();
      });
  }
  back() {
    this.location.back();
  }


}
