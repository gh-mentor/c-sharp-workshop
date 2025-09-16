# Advanced C# Copilot Training Scenario: Bank Transaction Monitoring System

## Scenario Overview

You are part of a software engineering team at a large bank. The bank wants to modernize its transaction monitoring system to detect suspicious activities, ensure compliance, and provide real-time insights to analysts. The new system must be robust, scalable, and secure.

---

## Exercise Breakdown

### 1. Account Management
- **Design**: Create an abstract `BankAccount` class and derive `CheckingAccount`, `SavingsAccount`, and `BusinessAccount`.
- **Features**:
  - Account creation, closure, and updates
  - Enforce business rules (e.g., minimum balance, overdraft protection)
  - Unique account numbers

### 2. Transaction Processing
- **Design**: Implement a `Transaction` class and a `TransactionProcessor` service.
- **Features**:
  - Handle deposits, withdrawals, and transfers
  - Ensure thread-safe processing (e.g., using locks or concurrent collections)
  - Log all transactions with timestamps and unique IDs

### 3. Suspicious Activity Detection
- **Design**: Create a `SuspiciousActivityDetector` with configurable rules.
- **Features**:
  - Flag transactions based on criteria (e.g., large amounts, rapid activity)
  - Allow analysts to review and mark transactions
  - Escalate suspicious cases

### 4. Reporting and Auditing
- **Design**: Implement reporting services and an audit log.
- **Features**:
  - Generate daily, weekly, and monthly reports
  - Provide a complete audit trail for all actions

### 5. Extensibility
- **Design**: Use interfaces and dependency injection to allow new account types and detection rules.
- **Features**:
  - Add new account types or rules with minimal code changes

### 6. Testing
- **Design**: Write unit and integration tests for all major components.
- **Features**:
  - Simulate concurrent transactions
  - Test business rules and detection logic

---

## Optional Extensions
- Integrate with an external API for currency exchange rates
- Implement a simple UI (console or web) for analysts
- Add support for scheduled/recurring transactions

---

## Deliverables
- Well-documented C# codebase
- Unit and integration tests
- Sample data and usage instructions

---

## Tips for Success
- Focus on clean architecture and SOLID principles
- Use C# features such as async/await, LINQ, and generics
- Document your design decisions and assumptions

---

Feel free to expand or modify this scenario to fit your class needs!