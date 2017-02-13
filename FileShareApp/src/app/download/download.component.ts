import { Component, OnInit } from '@angular/core';

import { Observable }        from 'rxjs/Observable';
import { Subject }           from 'rxjs/Subject';

import { File, FileService } from './../shared/file.service';

@Component({
    moduleId: module.id,
    templateUrl: './download.component.html'
})
export class DownloadComponent implements OnInit {
    files: File[];
    //files: string[]
    
    constructor(private fileService: FileService) { }

    ngOnInit() {
        //this.files = this.fileService.getFiles();
        this.fileService.getFiles().then(files => this.files = files);
     }

     getFile(key: string) {
        this.fileService.getFile(key);
     }
}