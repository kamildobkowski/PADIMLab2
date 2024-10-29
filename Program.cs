using PADIMLab2;

class Program
{
	static void Main()
	{
		Console.WriteLine("Starting Image Processing:");
		ImageProcessing.ProcessImages();
		Task.Delay(2000).Wait(); // Krótkie opóźnienie przed następną akcją

		Console.WriteLine("\nStarting Report Generation:");
		ReportGeneration.GenerateReports();
		Task.Delay(2000).Wait();

		Console.WriteLine("\nStarting Ticket Booking:");
		TicketBooking.BookSeats();
		Task.Delay(2000).Wait();

		Console.WriteLine("\nUpdating Session Cache:");
		SessionCache.UpdateSessions();
		Task.Delay(2000).Wait();

		Console.WriteLine("\nHandling Support Queue:");
		SupportQueue.HandleTickets();
		
		Console.WriteLine("\nStarting Sending Emails:");
		EmailSender.ForEach();

		 Console.WriteLine("\nFetching Products:");
        ProductFetcher.FetchProducts();
        Task.Delay(2000).Wait();

        Console.WriteLine("\nRunning External Integration with Exception Handling:");
        ExternalIntegration.RunTasksWithExceptionHandling();
        Task.Delay(2000).Wait();

        Console.WriteLine("\nStarting Account Balance Update:");
        AccountBalance.UpdateBalance();
        Task.Delay(2000).Wait();

        Console.WriteLine("\nStarting Order Processing with Producer-Consumer Pattern:");
        OrderProcessing.StartProcessing();

		Console.WriteLine("\nAll operations completed.");
	}
}