import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7236/api/Book';

  constructor(private http: HttpClient) {}

  getBooks(): Observable<any> {    
    return this.http.get(`${this.apiUrl}/list`);
  }

  getBooksByName(name: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/findBooksByName`, { params: { name } });
  }

  getBookById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  addBook(book: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, book);
  }

  updateBook(id: number, book: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/update`, book);
  }

  borrowBook(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/borrow`, {});
  }

  returnBook(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/return`, {});
  }

  deleteBook(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}/delete`);
  }
}
