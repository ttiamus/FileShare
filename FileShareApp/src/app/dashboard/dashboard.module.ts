import { NgModule } from '@angular/core';

import { SharedModule } from './../shared/shared.module';
import { UserService } from './../shared/user.service';

import { DashboardComponent }   from './dashboard.component';
import { DashboardRoutingModule }   from './dashboard-routing.module';

@NgModule({
    imports: [ DashboardRoutingModule, SharedModule ],
    declarations: [ DashboardComponent ],
    providers: [ UserService ],
    exports: [],
})
export class DashboardModule { }
