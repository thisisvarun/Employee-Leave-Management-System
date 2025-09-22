import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-teams',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-teams.html',
  styleUrls: ['./manage-teams.css']
})
export class ManageTeams implements OnInit {
  teams: any[] = [];

  constructor() { }

  ngOnInit(): void {
    this.teams = [
      { name: 'Alpha', manager: 'Jane Smith' },
      { name: 'Bravo', manager: 'Alex Johnson' },
      { name: 'Charlie', manager: 'Emily White' },
    ];
  }
}