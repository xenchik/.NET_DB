using NET_DB.Services;
using NET_DB.DataModel;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Danske Bank 2.0 Starting...");

//Init
Database.ImportTaxesFromCSV("Data/taxes.csv");
var producerService = new ProducerService();
var consumerService = new ConsumerService(producerService);

//Setup dates used in example
DateTime Y2016M01D01 = DateTime.Parse("2016-01-01");
DateTime Y2016M03D16 = DateTime.Parse("2016-03-16");
DateTime Y2016M05D02 = DateTime.Parse("2016-05-02");
DateTime Y2016M07D10 = DateTime.Parse("2016-07-10");
DateTime Y2016M12D25 = DateTime.Parse("2016-01-31");

//Monthly and Yearly rates from task description added through csv file.
consumerService.AddTaxRecord("Copenhagen", Y2016M01D01, Y2016M01D01, 0.1M, TaxFrequency.Daily);
consumerService.AddTaxRecord("Copenhagen", Y2016M12D25, Y2016M12D25, 0.1M, TaxFrequency.Daily);

//Get results from task example.
consumerService.GetTaxRate("Copenhagen", Y2016M01D01);
consumerService.GetTaxRate("Copenhagen", Y2016M05D02);
consumerService.GetTaxRate("Copenhagen", Y2016M07D10);
consumerService.GetTaxRate("Copenhagen", Y2016M03D16);

Console.WriteLine("Danske Bank 2.0 going back to sleep...");