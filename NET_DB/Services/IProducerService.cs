using NET_DB.DataModel;

namespace NET_DB.Services
{
    public interface IProducerService
    {
        /// <summary>
        /// Add a tax record to the database
        /// </summary>
        /// <param name="municipality">Municipality to add record for. Can't be null or empty.</param>
        /// <param name="startDate">Start date of tax period. Can be same as endDate.</param>
        /// <param name="endDate">Last date of tax period. Can't be earlier than startDate.</param>
        /// <param name="taxRate">Decimal tax rate</param>
        /// <param name="frequency">Frequency. Currently supports Daily, Weekly, Monthly, Yearly.</param>
        /// <returns>true if successfull</returns>
        public TaxRequestResult<bool?> AddTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency);

        /// <summary>
        /// Remove a tax record from the database
        /// </summary>
        /// <param name="municipality">Municipality to add record for. Can't be null or empty.</param>
        /// <param name="startDate">Start date of tax period. Can be same as endDate.</param>
        /// <param name="endDate">Last date of tax period. Can't be earlier than startDate.</param>
        /// <param name="taxRate">Decimal tax rate</param>
        /// <param name="frequency">Frequency. Currently supports Daily, Weekly, Monthly, Yearly.</param>
        /// <returns>true if successfull</returns>
        public TaxRequestResult<bool?> RemoveTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency frequency);
        
        /// <summary>
        /// Get the tax rate for a municipality at a given date
        /// </summary>
        /// <param name="municipality">Municapality to find tax rate for</param>
        /// <param name="date">Date which tax period must cover.</param>
        /// <returns>Tax rate at given date in given municipality. If multiple exist, return first in ascending order (Daily, Weekly, Monthly, Yearly).</returns>
        public TaxRequestResult<decimal?> GetTaxRateOnDate(string municipality, DateTime date);
    }
}
