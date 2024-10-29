using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class ProductFetcher
{
    private static ConcurrentBag<string> products = new ConcurrentBag<string>();

    public static void FetchProducts()
    {
        var tasks = new Task[]
        {
            Task.Run(() => AddProduct("Product from Source 1")),
            Task.Run(() => AddProduct("Product from Source 2")),
            Task.Run(() => AddProduct("Product from Source 3"))
        };

        Task.WaitAll(tasks);

        Console.WriteLine("Fetched products:");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }

    private static void AddProduct(string product)
    {
        products.Add(product);
        Console.WriteLine($"{product} fetched and added.");
    }
}

class ExternalIntegration
{
    public static void RunTasksWithExceptionHandling()
    {
        var task1 = Task.Run(() => { throw new InvalidOperationException("Invalid operation in Task 1"); });
        var task2 = Task.Run(() => { throw new AccessViolationException("Access violation in Task 2"); });
        
        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException ae)
        {
            foreach (var ex in ae.InnerExceptions)
            {
                Console.WriteLine($"Caught exception: {ex.GetType()} - {ex.Message}");
            }
        }
    }
}

class AccountBalance
{
    private static int _balance;

    public static void UpdateBalance()
    {
        var tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    Interlocked.Add(ref _balance, 100);  // Aktualizacja salda
                    Interlocked.Add(ref _balance, -100); // Wypłata
                }
            });
        }
        
        Task.WaitAll(tasks);
        Console.WriteLine($"Final balance is {_balance}");
    }
}

class OrderProcessing
{
    private static BlockingCollection<string> _orders = new BlockingCollection<string>(5);
    private static CancellationTokenSource _cts = new CancellationTokenSource();

    public static void StartProcessing()
    {
        var producerTask = Task.Run(() => Producer());
        var consumerTask = Task.Run(() => Consumer());

        Console.WriteLine("Press any key to stop processing...");
        Console.ReadKey();
        
        // Anulowanie produkcji i oczekiwanie na zakończenie `Producer`
        _cts.Cancel();
        producerTask.Wait(); 

        // Dopiero po zakończeniu `Producer`, ustawiamy CompleteAdding
        _orders.CompleteAdding();

        // Oczekiwanie na zakończenie `Consumer`
        consumerTask.Wait();

        Console.WriteLine("Processing stopped.");
    }

    private static void Producer()
    {
        int orderNumber = 1;
        while (!_cts.Token.IsCancellationRequested)
        {
            string order = $"Order {orderNumber++}";
            _orders.Add(order);
            Console.WriteLine($"Produced {order}");
            Thread.Sleep(500); // Symulacja czasu produkcji
        }
    }

    private static void Consumer()
    {
        // Pobiera elementy, dopóki nie zakończono dodawania do kolekcji
        foreach (var order in _orders.GetConsumingEnumerable())
        {
            Console.WriteLine($"Consumed {order}");
            Thread.Sleep(1000); // Symulacja czasu konsumpcji
        }
    }
}