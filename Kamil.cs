namespace PADIMLab2;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class ImageProcessing
{
    public static void ProcessImages()
    {
        string[] files = { "image1.jpg", "image2.jpg", "image3.jpg" };

        foreach (var file in files)
        {
            Task.Factory.StartNew(() => ProcessImage(file));
        }

        Console.WriteLine("Started processing images.");
    }

    private static void ProcessImage(string fileName)
    {
        Console.WriteLine($"Processing {fileName}");
        Task.Delay(1000).Wait(); // Symulacja przetwarzania
        Console.WriteLine($"Finished processing {fileName}");
    }
}

class ReportGeneration
{
    public static void GenerateReports()
    {
        var cts = new CancellationTokenSource();
        var reportTask = Task.Run(() => GenerateReport(cts.Token), cts.Token);

        Task.Delay(2000).Wait(); // Symulacja opóźnienia przed anulowaniem
        cts.Cancel();
        Console.WriteLine("Report generation cancelled.");
    }

    private static void GenerateReport(CancellationToken token)
    {
        for (int i = 0; i < 5; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Report generation was cancelled.");
                return;
            }
            Console.WriteLine($"Generating report... Step {i + 1}");
            Thread.Sleep(1000); // Symulacja przetwarzania
        }
        Console.WriteLine("Report generated successfully.");
    }
}

class TicketBooking
{
    private static object padlock = new object();
    private static int availableSeats = 10;

    public static void BookSeats()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 15; i++) // Więcej prób niż miejsc
        {
            tasks.Add(Task.Run(() => BookSeat()));
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine("All booking attempts completed.");
    }

    private static void BookSeat()
    {
        lock (padlock)
        {
            if (availableSeats > 0)
            {
                Console.WriteLine($"Seat booked. Remaining seats: {--availableSeats}");
            }
            else
            {
                Console.WriteLine("No seats available.");
            }
        }
    }
}

class SessionCache
{
    private static ConcurrentDictionary<string, string> userSessions = new ConcurrentDictionary<string, string>();

    public static void UpdateSessions()
    {
        Parallel.Invoke(
            () => UpdateSession("user1", "SessionData1"),
            () => UpdateSession("user2", "SessionData2"),
            () => UpdateSession("user1", "SessionData1Updated")
        );

        foreach (var session in userSessions)
        {
            Console.WriteLine($"User: {session.Key}, Session: {session.Value}");
        }
    }

    private static void UpdateSession(string userId, string sessionData)
    {
        userSessions.AddOrUpdate(userId, sessionData, (key, oldValue) => sessionData);
        Console.WriteLine($"Session updated for user: {userId}");
    }
}

class SupportQueue
{
    private static ConcurrentQueue<string> ticketQueue = new ConcurrentQueue<string>();

    public static void HandleTickets()
    {
        Parallel.Invoke(
            () => AddTicket("Ticket 1"),
            () => AddTicket("Ticket 2"),
            () => AddTicket("Ticket 3")
        );

        Task.Run(() => ProcessTickets()).Wait();
        Console.WriteLine("All tickets processed.");
    }

    private static void AddTicket(string ticket)
    {
        ticketQueue.Enqueue(ticket);
        Console.WriteLine($"Ticket added: {ticket}");
    }

    private static void ProcessTickets()
    {
        while (!ticketQueue.IsEmpty)
        {
            if (ticketQueue.TryDequeue(out string ticket))
            {
                Console.WriteLine($"Processing ticket: {ticket}");
                Task.Delay(500).Wait(); // Symulacja czasu obsługi
            }
        }
    }
}

class EmailSender
{
    public static void ForEach()
    {
        //Zastosowanie Parallel.ForEach to równoległe wykonywanie niepowiązanych ze sobą operacji.
		
        //Tu mamy worker, który wysyła do x użytkowników emaila z wiadomością "Hello".
        var userIds = Enumerable.Range(0, 10);
        Parallel.ForEach(userIds, x =>
        {
            SendEmail(x, "Hello");
        });
    }

    private static void SendEmail(int id, string message)
    {
        Thread.Sleep(1000);
    }
}