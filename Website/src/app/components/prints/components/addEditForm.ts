import { Component, OnInit, Input } from '@angular/core';
import { PrintsService } from '../Shared/service';
import { PrintsData } from '../Shared/model';
import { Location } from '@angular/common';
import { TypesData } from '../../../components/types/Shared/model';
import { TypesService } from '../../../components/types/Shared/service';
import { CompanyService } from '../../../components/company/Shared/service';
import { CompanyData } from '../../../components/company/Shared/model';

@Component({
  selector: 'Prints-addEditform',
    templateUrl: './addEditForm.html',
    providers: [PrintsService, TypesService, CompanyService]
})
export class PrintsAddEditFormComponent implements OnInit {
  @Input() item: PrintsData = <PrintsData>{};
  companies: CompanyData[] = [];
  types: TypesData[] = [];
  constructor(private PrintsService: PrintsService,
    private TypesService: TypesService,
    private CompanyService: CompanyService,
        private location: Location) { }

  ngOnInit() {
    this.getTypes();
    this.getCompanies();
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

  onSubmit() {
    if (this.item.id != 0 && this.item.id != null) // Update
          this.PrintsService.updateItem(this.item)
                .subscribe(data => {

                    this.back();
                });
        else //  ADD
          this.PrintsService.addNewItem(this.item)
                .subscribe(data => {

                    this.back();
                });
    }

    back() {
        this.location.back();
    }
}
