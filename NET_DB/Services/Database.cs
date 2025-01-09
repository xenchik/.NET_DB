using NET_DB.DataModel;
using NET_DB.Helpers;

namespace NET_DB.Services
{
    //Should probably use an interface.
    public static class Database
    {
        public static List<MunicipalityTaxRecord> _taxes { get; private set; } = new();

        public static void AddTaxRecord(MunicipalityTaxRecord tax)
        {
            if (!_taxes.Contains(tax))
            {
                _taxes.Add(tax);
            }
            else
            {
                throw new Exception("Tax record with given arguments already exist.");
            }
        }

        public static void RemoveTaxRecord(MunicipalityTaxRecord tax)
        {
            if (_taxes.Contains(tax))
            {
                _taxes.Remove(tax);
            }
            else
            {
                throw new Exception("Tax record with given arguments does not exist.");
            }
        }

        public static decimal GetTaxRateOnDate(string municipality, DateTime date)
        {
            //Check if any municipality exist with given name
            var municipalityTaxes = _taxes.Where(t => t.Municipality == municipality);
            if (!municipalityTaxes.Any())
            {
                throw new Exception("No municipality with given name found.");
            }

            //Check that one or more tax records exist that matches given date
            var taxesWithinDate = municipalityTaxes.Where(t => t.StartDate <= date && t.EndDate >= date);
            if (!taxesWithinDate.Any())
            {
                throw new Exception("No tax period matches given date.");
            }

            //Order any returned taxes in ascending order: daily, weekly, monthly, yearly..
            var taxRatesOrdered = taxesWithinDate.OrderBy(t => t.Frequency);

            return taxRatesOrdered.First().TaxRate;
        }

        //Needs additional error handling...
        public static void ImportTaxesFromCSV(string filePath)
        {
            var importedTaxes = FileImporter.ImportFromCSV(filePath);
            foreach (var tax in importedTaxes)
            {
                AddTaxRecord(tax);
            }

        }
    }
}
