import { Component, OnInit, Input } from '@angular/core';
import { TypesService } from '../Shared/service';
import { TypesData } from '../Shared/model';
import { Location } from '@angular/common';

@Component({
  selector: 'Types-addEditform',
    templateUrl: './addEditForm.html',
    providers: [TypesService]
})
export class TypesAddEditFormComponent implements OnInit {
  @Input() item: TypesData = <TypesData>{};

  constructor(private TypesService: TypesService,
        private location: Location) { }

  ngOnInit() {

    }

    onSubmit() {
      if (this.item.id != 0 && this.item.id != null) // Update
          this.TypesService.updateItem(this.item)
                .subscribe(data => {

                    this.back();
                });
        else //  ADD
          this.TypesService.addNewItem(this.item)
                .subscribe(data => {

                    this.back();
                });
    }

    back() {
        this.location.back();
    }
}
