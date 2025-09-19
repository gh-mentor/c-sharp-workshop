# Bank Transaction Monitoring System - Exercise Overview

This scenario challenges you to design, implement, and test a robust transaction monitoring system for a modern bank. The exercise is designed to strengthen your skills in object-oriented design, concurrency, extensibility, and testing in C#.

---

## Objective

Build a modular, scalable, and secure system that manages bank accounts, processes transactions, detects suspicious activity, and provides reporting and auditing capabilities.

---

## Key Components

1. **Account Management**
   - Abstract and derived account classes (Checking, Savings, Business)
   - Account lifecycle operations and business rules

2. **Transaction Processing**
   - Thread-safe handling of deposits, withdrawals, and transfers
   - Transaction logging with unique IDs and timestamps

3. **Suspicious Activity Detection**
   - Configurable detection rules
   - Analyst review and escalation workflow

4. **Reporting and Auditing**
   - Automated report generation (daily, weekly, monthly)
   - Comprehensive audit trail

5. **Extensibility**
   - Interfaces and dependency injection for easy expansion

6. **Testing**
   - Unit and integration tests
   - Simulated concurrent transactions

---

## Getting Started

- Review the scenario and requirements in `bank-scenario.md`.
- Plan your architecture and identify key classes and interfaces.
- Implement core features incrementally, testing as you go.
- Document your code and design decisions.

---

## Deliverables

- Complete, well-documented C# codebase
- Unit and integration tests
- Sample data and instructions for running the system

---

## Tips

- Apply SOLID principles and clean architecture
- Use C# features such as async/await, LINQ, and generics
- Focus on thread safety and data integrity
- Make your design extensible for future requirements

---

For full details and breakdowns, see `bank-scenario.md`.

---

## Sample Copilot Prompts (SDLC)

Use these prompts to guide your work throughout the software development lifecycle:

### Requirements & Analysis
- "Generate C# class diagrams for a bank account system with Checking, Savings, and Business accounts."
- "List business rules for account creation and transaction processing in a bank."

### Design
- "Create an abstract BankAccount class and derived classes for CheckingAccount, SavingsAccount, and BusinessAccount."
- "Design a thread-safe TransactionProcessor service in C#."

### Implementation
- "Implement a method to process deposits and withdrawals with proper validation."
- "Write C# code to log all transactions with unique IDs and timestamps."

### Testing
- "Generate unit tests for the SuspiciousActivityDetector class."
- "Simulate concurrent transfers between accounts and check for race conditions."

### Deployment & Maintenance
- "Suggest ways to make the system extensible for new account types or detection rules."
- "How can I document the API and usage instructions for this banking system?"