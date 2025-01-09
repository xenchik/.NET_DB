using NET_DB.DataModel;

namespace NET_DB.Services
{
    public class ProducerService : IProducerService
    {

        public TaxRequestResult<bool?> AddTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency)
        {
            try
            {
                var newTaxRecord = new MunicipalityTaxRecord(municipality, startDate, endDate, taxRate, frequency);
                Database.AddTaxRecord(newTaxRecord);
                var resultObject = new TaxRequestResult<bool?>()
                {
                    Success = true,
                    Result = true,
                    ErrorMessage = null
                };
                return resultObject;
            }
            catch (Exception ex)
            {
                var resultObject = new TaxRequestResult<bool?>()
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = ex.Message
                };
                return resultObject;
            }
        }

        public TaxRequestResult<bool?> RemoveTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency)
        {
            try
            {
                var newTaxRecord = new MunicipalityTaxRecord(municipality, startDate, endDate, taxRate, frequency);
                Database.RemoveTaxRecord(newTaxRecord);
                var resultObject = new TaxRequestResult<bool?>()
                {
                    Success = true,
                    Result = true,
                    ErrorMessage = null
                };
                return resultObject;
            }   
            catch (Exception ex)
            {
                var resultObject = new TaxRequestResult<bool?>()
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = ex.Message
                };
                return resultObject;
            }
        }

        //get tax for municipality? <- specific date and municipality?
        public TaxRequestResult<decimal?> GetTaxRateOnDate(string municipality, DateTime date)
        {
            try
            {
                decimal taxRate = Database.GetTaxRateOnDate(municipality, date); 
                var resultObject = new TaxRequestResult<decimal?>()
                {
                    Success = true,
                    Result = taxRate,
                    ErrorMessage = null
                };
                return resultObject;
            }
            catch (Exception ex)
            {
                var resultObject = new TaxRequestResult<decimal?>()
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = ex.Message
                };
                return resultObject;
            }
        }
    }
}