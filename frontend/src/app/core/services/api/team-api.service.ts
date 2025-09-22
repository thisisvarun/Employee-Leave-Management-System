import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class TeamApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  getManagerLeaveRequests(managerId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/team/leaves`, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
    });
  }

  updateLeaveStatus(leaveId: number, status: string, comment: string) {
    return this.http.put(
      `${this.API_BASE_URL}/api/team/leaves/${leaveId}/status`,
      { status, comment },
      {
        headers: {
          Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
        },
        withCredentials: true,
      }
    );
  }
}
