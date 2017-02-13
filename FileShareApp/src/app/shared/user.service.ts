import { Injectable } from '@angular/core';

export class User {
    constructor( 
        public domain: string,
        public samAccountName: string,
    ) { }
}

let USER = new User("hca", "bsi7887")

let userPromise = Promise.resolve(USER);

@Injectable()
export class UserService {
    constructor() { }

    getCurrentUser(){
        return userPromise;
    }
}