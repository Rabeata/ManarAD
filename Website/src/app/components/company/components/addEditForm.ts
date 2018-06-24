import { Component, OnInit, Input } from '@angular/core';
import { CompanyService } from '../Shared/service';
import { CompanyData } from '../Shared/model';
import { Location } from '@angular/common';

@Component({
  selector: 'Company-addEditform',
    templateUrl: './addEditForm.html',
    providers: [CompanyService]
})
export class CompanyAddEditFormComponent implements OnInit {
  @Input() item: CompanyData = <CompanyData>{};

  constructor(private CompanyService: CompanyService,
        private location: Location) { }

  ngOnInit() {

    }

    onSubmit() {
      if (this.item.id != 0 && this.item.id != null) // Update
          this.CompanyService.updateItem(this.item)
                .subscribe(data => {

                    this.back();
                });
        else //  ADD
          this.CompanyService.addNewItem(this.item)
                .subscribe(data => {

                    this.back();
                });
    }

    back() {
        this.location.back();
    }
}
