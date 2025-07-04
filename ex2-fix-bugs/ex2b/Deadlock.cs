using System;
using System.Threading;

public class BankAccount
{
  private readonly string _name;
  private double _balance;
  private readonly object _lock = new object();

  public BankAccount(string name, double balance)
  {
    _name = name;
    _balance = balance;
  }

  /// <summary>
  /// Transfers money from this account to another account.
  /// </summary>
  /// <param name="toAccount">The account to transfer money to.</param>
  /// <param name="amount">The amount to transfer.</param>
  public void Transfer(BankAccount toAccount, double amount)
  {
    lock (_lock)
    {
      Thread.Sleep(10); // Simulate processing delay
      lock (toAccount._lock)
      {
        if (_balance >= amount)
        {
          _balance -= amount;
          toAccount._balance += amount;
          Console.WriteLine($"Transferred {amount} from {_name} to {toAccount._name}.");
        }
        else
        {
          Console.WriteLine($"Insufficient funds in {_name} to transfer {amount}.");
        }
      }
    }
  }

  /// <summary>
  /// Prints the current balance of the account.
  /// </summary>
  public void PrintBalance()
  {
    Console.WriteLine($"{_name} balance: {_balance}");
  }
}

public class Program
{
  /// <summary>
  /// Handles money transfer between two accounts using threads.
  /// </summary>
  /// <param name="account1">The first account.</param>
  /// <param name="account2">The second account.</param>
  private static void PerformTransfers(BankAccount account1, BankAccount account2)
  {
    var thread1 = new Thread(() => account1.Transfer(account2, 100.0));
    var thread2 = new Thread(() => account2.Transfer(account1, 200.0));

    thread1.Start();
    thread2.Start();

    thread1.Join();
    thread2.Join();
  }

  public static void Main()
  {
    var account1 = new BankAccount("Account1", 1000.0);
    var account2 = new BankAccount("Account2", 1000.0);

    PerformTransfers(account1, account2);

    account1.PrintBalance();
    account2.PrintBalance();
  }
}
