import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environment/environment';

// class Request {
//   private BASE_URL: string = '';
//   private url: string = '';
//   private body!: object;
//   private contentType: string = '';

//   constructor() {}

//   public addBaseUrl(url: string) {
//     this.BASE_URL = url;
//     return this;
//   }

//   public addUrl(url: string) {
//     this.url = url;
//     return this;
//   }

//   public addBody(body: object) {
//     this.body = body;
//     return this;
//   }

//   public addContentType(contentType: string) {
//     this.contentType = contentType;
//     return this;
//   }

//   public request() {

//   }

// }

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private API_BASE_URL = environment.API_BASE_URL;

  constructor(private readonly http: HttpClient) {}

  loginUser(username: string, password: string) {
    return this.http.post(
      `${this.API_BASE_URL}/login`,
      {
        username,
        password,
      },
      {
        headers: {
          'Content-Type': 'application/json',
        },
        withCredentials: true,
      }
    );
  }
}
