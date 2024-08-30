export class Book {
    id: number;
    name: string;
    synopsis: string;
    authorName: string;
    isBorrowed: boolean;

    constructor(private book : Book){
        this.id = book.id,
        this.name = book.name,
        this.synopsis = book.synopsis,
        this.authorName = book.authorName,
        this.isBorrowed = book.isBorrowed
    }
  }