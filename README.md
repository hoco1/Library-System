# Library System Program in C#

## Introduction
This program provides a comprehensive solution for managing a library system, allowing for easy management of books, patrons, and transactions. 
The program includes various features, such as the ability to add, display, and update information and validation checks to ensure data accuracy. 
The program also uses SQL Server for efficient data storage and retrieval, making it a robust and reliable solution. 
In addition, the late fee calculation using the strategy pattern allows for flexibility in the late fee calculation based on the borrowing period.

## Features

- Add books, patrons, and transactions.
- Transactions include damage fee, late fee, return date, due date, patron ID, and book ID.
- Display a list of books, patrons, and transactions.
- Display books borrowed by a specific patron.
- Update patron membership and transaction when patrons return the book.
- Validation checks for patrons when subscriptions expire or have unpaid fees and also checks inputs.
- Late fee calculation using the daily, weekly, monthly, and fixed fee strategy pattern

## Menu Options
1. Add Book
2. Add Patron
3. Add Transaction
4. List Books
5. List Patrons
6. List Transactions
7. Display Books Borrowed by Patron
8. Update Patron Membership
9. Update Transaction Return Date

## Input Validation
- Patron ID and Book ID must be 4-digit numbers.
- Patron and Book names cannot be empty or contain numbers.
- Patrons with expired subscriptions or unpaid fees cannot borrow books.
## Late Fee Calculation
- Daily late fee: $0.50 per day
- Weekly late price: $2.00 per week
- Monthly late price: $5.00 per month
- Fixed late price: $100.00
