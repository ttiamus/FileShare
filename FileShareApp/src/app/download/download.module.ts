import { NgModule } from '@angular/core';

import { FileService } from './../shared/file.service';
import { SharedModule } from './../shared/shared.module';

import { DownloadComponent }   from './download.component';
import { DownloadRoutingModule } from './download-routing.module'

@NgModule({
    imports:  [ DownloadRoutingModule, SharedModule ],
    declarations: [ DownloadComponent ],
    providers: [ FileService ],
    exports: [],
})
export class DownloadModule { }
