import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class EmployeeApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  getEmployeeById(id: string) {
    return this.http.get(`${this.API_BASE_URL}/api/employee/${id}`, {
      withCredentials: true,
    });
  }

  createEmployee(employeeData: any) {
    return this.http.post(`${this.API_BASE_URL}/api/employee`, employeeData, {
      withCredentials: true,
    });
  }

}
