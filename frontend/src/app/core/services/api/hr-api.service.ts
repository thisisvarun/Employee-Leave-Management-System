import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class HrApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  getHrLeaveRequests(hrId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/hr/leaves`, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
      withCredentials: true,
    });
  }

  updateLeaveStatus(leaveId: number, status: string, comment: string) {
    return this.http.put(
      `${this.API_BASE_URL}/api/hr/leaves/${leaveId}/status`,
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
