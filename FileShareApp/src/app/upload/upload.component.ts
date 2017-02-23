import { Component, OnInit, isDevMode } from '@angular/core';
import { FileSelectDirective, FileDropDirective, FileUploader } from 'ng2-file-upload/ng2-file-upload';

const localURL = 'http://file-upload.healthtrustpg.com/files';
const prodURL = 'http://api-file-share.ttiamus.com/files';

@Component({
    moduleId: module.id,
    templateUrl: './upload.component.html'
})
export class UploadComponent implements OnInit {
    /*
    ** url - URL of File Uploader's route
    ** authToken - auth token that will be applied as 'Authorization' header during file send.
    ** disableMultipart - If 'true', disable using a multipart form for file upload and instead stream the file. Some APIs (e.g. Amazon S3) may expect the file to be streamed rather than sent via a form. Defaults to false. 
    */

    public uploader:FileUploader;
    public hasBaseDropZoneOver:boolean = false;         //handles on hover styling for the drop zones

    public constructor(){
        if(isDevMode()){
            this.uploader  = new FileUploader({url: localURL,});
        } else {
            this.uploader  = new FileUploader({url: prodURL,});
        }
    }

    public ngOnInit() { 
        //This disables the need to add cors credentials
        //Before adding this I was getting errors around Access-Control-Allow-Credentials being empty
        this.uploader.onBeforeUploadItem = (item) => {
            item.withCredentials = false;
        }
    }

    public fileOverBase(e:any):void {
        this.hasBaseDropZoneOver = e;
    }
}