import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BookService } from '../services/book.service';
import { Book } from '../../../Models/Book';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent implements OnInit {
  BookList?: Observable<Book[]>;  
  bookForm: any;
  massage = "";
  prodCategory = "";
  bookId = 0;
  constructor(private formbulider: FormBuilder,
    private bookService: BookService,private router: Router,
    private jwtHelper : JwtHelperService) { }
    ngOnInit() {
      this.prodCategory = "0";
      this.bookForm = this.formbulider.group({
        Name: ['', [Validators.required]],
        Synopsys: ['', [Validators.required]],
        AuthorName: ['', [Validators.required]],
        IsBorrowed: [false]
      });
      this.getBookList();
    }
    
    getBookList() {
      this.BookList = this.bookService.getBookList();    
    }
    
    Addbook(book: Book) {
      const book_Master = this.bookForm.value;
      this.bookService.addBook(book_Master).subscribe(
        () => {
          this.getBookList();
          this.bookForm.reset();
        }
      );
    }
    
    bookDetailsToEdit(id: number) {
      this.bookService.getBookById(id).subscribe(bookResult => {
        this.bookForm.controls['name'].setValue(bookResult.Name);
        this.bookForm.controls['synopsys'].setValue(bookResult.Synopsis);
        this.bookForm.controls['authorName'].setValue(bookResult.AuthorName);
        this.bookForm.controls['isBorrowed'].setValue(bookResult.IsBorrowed);
      });
    }
    
    Updatebook(id: number,book: Book) {
      const book_Master = this.bookForm.value;
      this.bookService.updateBook(id, book_Master).subscribe(() => {      
        this.bookForm.reset();
        this.getBookList();
      });
    }
    Deletebook(id: number) {
      if (confirm('Do you want to delete this book?')) {
        this.bookService.deleteBook(id).subscribe(() => {
          this.getBookList();
        });
      }
    }
    
    Borrowbook(id: number) {    
      this.bookService.borrowBook(id).subscribe(() => {
        this.getBookList();
      });   
    }
    
    Clear(book: Book){
      this.bookForm.reset();
    }
    public logOut = () => {
      localStorage.removeItem("jwt");
      this.router.navigate(["/"]);
    }
    isUserAuthenticated() {
      const token = localStorage.getItem("jwt");
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        return true;
      }
      else {
        return false;
      }
    }
  }
  