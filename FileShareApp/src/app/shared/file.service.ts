import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

export class File {
    constructor(public key: number, public name:string) { }
}

@Injectable()
export class FileService {
    constructor(private http: Http) { }

    //Observable is for event streams
    //Promises are for one time events
    getFiles(): Promise<File[]> {
        return this.http.get('http://file-upload.healthtrustpg.com/files')
        .toPromise().then(response => 
            response.json() as File[]
        );
    }

    getFile(key: number | string) {
        window.open('http://file-upload.healthtrustpg.com/files/${key}', '_blank', '');  
    }
}