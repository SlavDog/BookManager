## Requirements
- .NET 9.0
- SQL Server Express LocalDB

## Help section

### Filtering

To filter out the grid contents, you can use this notation in the search bar:

*please omit the curly braces*

- Include **Title="{Title}"** to filter by the title.
- Include **Author="{Author}"** to filter by the author.
- Include **Finish="{FinishDate}"** to filter by the date you finished reading the book.
- Include **Rating="{Rating}"** to filter by the rating you gave the book.
- Include **Bookshelf="{Bookshelf}"** to filter by the bookshelf (Completed, Want To Read, ...).
- Include **Genre="{Genre}"** to filter by the genre of the book.

If you do not use these filters, the search will be performed on Titles.

### Change the book details

To change the details of a book, you can double-click on a cell in the grid and change the value.
After you are done, deselect the cell and hit the **Save Changes** button.

### CSV import

The application supports CSV import and export. The import supports CSV file format of an export from GoodReads. The CSV must contain these columns:

*Title, Author, My Rating, Date Read, Exclusive Shelf, Genre*

The application ignores the rest of the columns.

If there is a book with the same name and author belonging to current user, the application will not import it again.