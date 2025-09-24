import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';
import { Leave } from '@shared/models/Leave';

@Injectable({
  providedIn: 'root',
})
export class LeaveApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  getLeaveSummary(employeeId: string) {
    return this.http.get(`${this.API_BASE_URL}/api/employee/${employeeId}/summary`, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
      withCredentials: true,
    });
  }

  applyLeave(leaveData: any) {
    return this.http.post(`${this.API_BASE_URL}/api/leave`, leaveData, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
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
    return this.http.get<Leave[]>(`${this.API_BASE_URL}/api/leave/${employeeId}/history`, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('access_token')}`,
      },
      withCredentials: true,
    });
  }
}
