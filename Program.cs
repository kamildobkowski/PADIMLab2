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

		Console.WriteLine("\nAll operations completed.");
	}
}