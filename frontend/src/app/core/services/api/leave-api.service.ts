import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class LeaveApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  getLeaveSummary(employeeId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/employee/${employeeId}/summary`, {
      withCredentials: true,
    });
  }

  applyLeave(leaveData: any) {
    return this.http.post(`${this.API_BASE_URL}/api/leave`, leaveData, {
      withCredentials: true,
    });
  }

  getRecentLeaveStatus(employeeId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/employee/${employeeId}/recent-leave-status`, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
      withCredentials: true,
    });
  }

  getLeaveHistory(employeeId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/employee/${employeeId}/history`, {
      withCredentials: true,
    });
  }
}
