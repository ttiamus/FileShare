import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { environment } from './../../environments/environment';

import 'rxjs/add/operator/toPromise';

export class File {
    constructor(public Id: string, public FileName:string) { }
}

@Injectable()
export class FileService {
    public downloadUrl: string;
    public getAllUrl:string;

    constructor(private http: Http) { }

    //Observable is for event streams
    //Promises are for one time events
    getFiles(): Promise<File[]> {
        return this.http.get(environment.getAllFileDataUrl)
        .toPromise().then(response => 
            response.json() as File[]
        );
    }

    getFile(id: string) {
        console.log(id);
        window.open(environment.downloadFileUrl + id, '_blank', '');  
    }
}