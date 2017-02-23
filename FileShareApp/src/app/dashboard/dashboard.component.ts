import { Component, OnInit } from '@angular/core';
import { User, UserService } from './../shared/user.service';
import { environment } from './../../environments/environment';

@Component({
    moduleId: module.id,
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
    currentUser: User;
    
    constructor(private userService: UserService) { }
    
    ngOnInit() { 
        console.log(`Production: ${environment.production}`);
        this.userService.getCurrentUser().then(user => this.currentUser = user);
    }
}