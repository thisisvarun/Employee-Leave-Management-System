import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-employees',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-employees.html',
  styleUrls: ['./manage-employees.css']
})
export class ManageEmployees implements OnInit {
  employees: any[] = [];

  constructor() { }

  ngOnInit(): void {
    this.employees = [
      { name: 'John Doe', email: 'john.doe@example.com', designation: 'Software Engineer', team: 'Alpha', status: 'Active' },
      { name: 'Jane Smith', email: 'jane.smith@example.com', designation: 'Project Manager', team: 'Bravo', status: 'Active' },
      { name: 'Peter Jones', email: 'peter.jones@example.com', designation: 'UI/UX Designer', team: 'Charlie', status: 'Inactive' },
    ];
  }
}