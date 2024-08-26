import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../../../Models/Book';
import configurl from '../../assets/config/config.json'
@Injectable({
  providedIn: 'root'
})
export class BookService {
  url = configurl.apiServer.url + '/api/book/';
  constructor(private http: HttpClient) { }

  getBookList(): Observable<Book[]> {
    return this.http.get<Book[]>(this.url + 'list');
  }

  addBook(BookData: Book): Observable<Book> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Book>(this.url + 'add', BookData, httpHeaders);
  }

  updateBook(id: number, Book: Book): Observable<Book> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Book>(this.url + Book.Id + '/update' , Book, httpHeaders);
  }
  deleteBook(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'DeleteBook?id=' + id, null);
  }
  borrowBook(id: number): Observable<Book> {
    return this.http.post<Book>(this.url + id + '/borrow', null);
  }
  
  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(this.url + 'BookDetail?id=' + id);
  }
}