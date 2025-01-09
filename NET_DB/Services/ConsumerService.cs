using NET_DB.DataModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NET_DB.Services
{
    //Should probably use an interface..
    public class ConsumerService
    {
        private IProducerService _producerService;
        public ConsumerService(IProducerService producerService)
        {
            _producerService = producerService;
        }
        
        public void GetTaxRate(string municipality, DateTime date)
        {
            TaxRequestResult<decimal?> result = _producerService.GetTaxRateOnDate(municipality, date);

            if (result.Success)
            {
                Console.WriteLine($"Successfully got taxrate {result.Result} for municipality {municipality} and date {date}");
            }
            else
            {
                Console.WriteLine($"Failed to get taxrate. Producer returned error: {result.ErrorMessage}");
            }
        }

        public void AddTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency) 
        {
            TaxRequestResult<bool?> result = _producerService.AddTaxRecord(municipality, startDate, endDate, taxRate, frequency);

            if (result.Success)
            {
                Console.WriteLine($"Successfully added new record.");
            }
            else
            {
                Console.WriteLine($"Failed to add new record. Producer returned error: {result.ErrorMessage}");
            }
        }

        public void RemoveTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency) 
        {
            TaxRequestResult<bool?> result = _producerService.RemoveTaxRecord(municipality, startDate, endDate, taxRate, frequency);

            if (result.Success)
            {
                Console.WriteLine($"Successfully removed record.");
            }
            else
            {
                Console.WriteLine($"Failed to remove record. Producer returned error: {result.ErrorMessage}");
            }
        }

    }
}
