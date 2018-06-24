import { Component, OnInit, OnDestroy, Inject, ViewEncapsulation, RendererFactory2, PLATFORM_ID } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, PRIMARY_OUTLET } from '@angular/router';
import { Meta, Title, DOCUMENT, MetaDefinition } from '@angular/platform-browser';

import { isPlatformServer } from '@angular/common';


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class AppComponent implements OnInit, OnDestroy {



    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute
    )
    {

   
 

    }

    ngOnInit() {
        // Change "Title" on every navigationEnd event
        // Titles come from the data.title property on all Routes (see app.routes.ts)
        
    }

    ngOnDestroy() {
        // Subscription clean-up
   
    }


}

