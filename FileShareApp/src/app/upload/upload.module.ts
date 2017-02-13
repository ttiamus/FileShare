import { NgModule } from '@angular/core';

import { FileUploadModule } from 'ng2-file-upload';

import { SharedModule } from './../shared/shared.module'
import { FileService } from './../shared/file.service';

import { UploadComponent }   from './upload.component';
import { UploadRoutingModule }   from './upload-routing.module';

@NgModule({
    imports: [ UploadRoutingModule, SharedModule, FileUploadModule ],
    declarations: [ UploadComponent ],
    providers: [ FileService ],
    exports: [],
})
export class UploadModule { }
