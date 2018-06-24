import { NgModule, Inject } from '@angular/core';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { HttpModule, Http  } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatCardModule,
  MatCheckboxModule,
  MatIconModule,
  MatInputModule,
  MatSelectModule,
  MatDatepickerModule,  MatNativeDateModule,
  MAT_DATE_LOCALE
} from '@angular/material';
//----------------------------------------------------------------------
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { PrintsAddEditFormComponent } from './components/prints/components/addEditForm';
import { IndexPrintsComponent } from './components/prints/pages/index/index.component';
import { AddPrintsComponent } from './components/prints/pages/add/add.component';
import { DetailsPrintsComponent } from './components/prints/pages/details/details.component';
import { DeletePrintsComponent } from './components/prints/pages/delete/delete.component';
import { EditPrintsComponent } from './components/prints/pages/edit/edit.component';
import { PrintsService } from './components/prints/Shared/service';
import { HttpClientModule } from '@angular/common/http';
import { TypesAddEditFormComponent } from './components/types/components/addEditForm';
import { IndexTypesComponent } from './components/types/pages/index/index.component';
import { AddTypesComponent } from './components/types/pages/add/add.component';
import { DetailsTypesComponent } from './components/types/pages/details/details.component';
import { DeleteTypesComponent } from './components/types/pages/delete/delete.component';
import { EditTypesComponent } from './components/types/pages/edit/edit.component';
import { CompanyAddEditFormComponent } from './components/company/components/addEditForm';
import { IndexCompanyComponent } from './components/company/pages/index/index.component';
import { AddCompanyComponent } from './components/company/pages/add/add.component';
import { DetailsCompanyComponent } from './components/company/pages/details/details.component';
import { DeleteCompanyComponent } from './components/company/pages/delete/delete.component';
import { EditCompanyComponent } from './components/company/pages/edit/edit.component';
//----------------------------------------------------------------------


@NgModule({
    declarations: [

    AppComponent,
    NavMenuComponent,
      HomeComponent,
      PrintsAddEditFormComponent,
      IndexPrintsComponent,
      AddPrintsComponent,
      DetailsPrintsComponent,
      DeletePrintsComponent,
      EditPrintsComponent,
 
   TypesAddEditFormComponent,
      IndexTypesComponent,
      AddTypesComponent,
      DetailsTypesComponent,
      DeleteTypesComponent,
      EditTypesComponent,

CompanyAddEditFormComponent,
      IndexCompanyComponent,
      AddCompanyComponent,
      DetailsCompanyComponent,
      DeleteCompanyComponent,
      EditCompanyComponent,
//-----------------------------------------------------


  ],
    imports: [
      HttpClientModule,
      NoopAnimationsModule,
      MatAutocompleteModule,
      MatButtonModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatCardModule,
      MatCheckboxModule,
      MatIconModule,
      MatInputModule,
      MatSelectModule,
    CommonModule,
    HttpModule,
    FormsModule,
    // You could also split this up if you don't want the Entire Module imported
      BrowserModule,

    // App Routing
    RouterModule.forRoot([
      {
        path: '',
        redirectTo: 'Prints',
        pathMatch: 'full'
        },
      {
          path: 'home', component: HomeComponent,
                data: {
              title: 'Home'
          }
      },
      {
        path: 'Prints', component: IndexPrintsComponent,
        data: {
          title: 'prints'
        }
      },
      {
        path: 'Prints/add', component: AddPrintsComponent,
        data: {
          title: 'prints add'
        }
      },
      {
        path: 'Prints/details/:id', component: DetailsPrintsComponent,
        data: {
          title: 'prints details'
        }
      },
      {
        path: 'Prints/delete/:id', component: DeletePrintsComponent,
        data: {
          title: 'prints delete'
        }
      },
         {
           path: 'Prints/edit/:id', component: EditPrintsComponent,
        data: {
          title: 'prints edit'
        }
      },
         {
           path: 'Types', component: IndexTypesComponent,
           data: {
             title: 'Types'
           }
         },
         {
           path: 'Types/add', component: AddTypesComponent,
           data: {
             title: 'Types add'
           }
         },
         {
           path: 'Types/details/:id', component: DetailsTypesComponent,
           data: {
             title: 'Types details'
           }
         },
         {
           path: 'Types/delete/:id', component: DeleteTypesComponent,
           data: {
             title: 'Types delete'
           }
         },
         {
           path: 'Types/edit/:id', component: EditTypesComponent,
           data: {
             title: 'Types edit'
           }
         },
    {
           path: 'Company', component: IndexCompanyComponent,
           data: {
             title: 'Company'
           }
         },
         {
           path: 'Company/add', component: AddCompanyComponent,
           data: {
             title: 'Company add'
           }
         },
         {
           path: 'Company/details/:id', component: DetailsCompanyComponent,
           data: {
             title: 'Company details'
           }
         },
         {
           path: 'Company/delete/:id', component: DeleteCompanyComponent,
           data: {
             title: 'Company delete'
           }
         },
         {
           path: 'Company/edit/:id', component: EditCompanyComponent,
           data: {
             title: 'Company edit'
           }
         }
      
    ]
        , {
        // Router options
        useHash: false,
        preloadingStrategy: PreloadAllModules,
        initialNavigation: 'enabled'
      })
  ],
    providers: [MatDatepickerModule,
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
    ],
    bootstrap: [AppComponent]

})
export class AppModuleShared {
}
