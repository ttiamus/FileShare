import { Component, OnInit } from '@angular/core';
import { User, UserService } from './../shared/user.service';

@Component({
    moduleId: module.id,
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
    currentUser: User;
    
    constructor(private userService: UserService) { }
    
    ngOnInit() { 
        this.userService.getCurrentUser().then(user => this.currentUser = user);
    }
}