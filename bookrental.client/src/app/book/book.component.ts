import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Book } from '../model/book';
import { BookService } from '../services/book/book.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})

export class BookComponent implements OnInit {
  books: Book[] = [];
  selectedBook: any;
  addBookForm!: FormGroup;

  constructor(private fb: FormBuilder, private bookService: BookService, private jwtHelper: JwtHelperService, private router: Router) {}

  ngOnInit(): void {
    this.loadBooks();
    this.addBookForm = this.fb.group({
      name: ['', Validators.required],
      authorName: ['', Validators.required],
      synopsis: ['', Validators.required],
    });
  }

  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {      
      this.router.navigate(['/login']);
      return false;
    }
  }
   
  loadBooks() {
    this.bookService.getBooks().subscribe(
      (data) => {
        console.log('Books:', data)
        this.books = data
      },
      (error) => console.error('Error loading books', error)
    );
  }

  searchBooksByName(name: string) {
    this.bookService.getBooksByName(name).subscribe(
      (data) => this.books = data,
      (error) => console.error('Error searching books', error)
    );
  }

  addBook(): void {
    if (this.addBookForm.valid) {
      this.bookService.addBook(this.addBookForm.value).subscribe(
        () => this.loadBooks(),
        (error) => console.error('Error adding book', error)
      );     
    }    
  }

  updateBook(id: number, book: any) {
    this.bookService.updateBook(id, book).subscribe(
      () => this.loadBooks(),
      (error) => console.error('Error updating book', error)
    );
  }

  borrowBook(id: number) {
    this.bookService.borrowBook(id).subscribe(
      () => this.loadBooks(),
      (error) => console.error('Error borrowing book', error)
    );
  }

  returnBook(id: number) {
    this.bookService.returnBook(id).subscribe(
      () => this.loadBooks(),
      (error) => console.error('Error returning book', error)
    );
  }

  deleteBook(id: number) {
    this.bookService.deleteBook(id).subscribe(
      () => this.loadBooks(),
      (error) => console.error('Error deleting book', error)
    );
  }
}