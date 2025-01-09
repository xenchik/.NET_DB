using NET_DB.DataModel;

namespace NET_DB.Helpers
{
    public class FileImporter
    {
        //This class needs additional error handling
        public static List<MunicipalityTaxRecord> ImportFromCSV(string filePath)
        {
            var lines = File.ReadAllLines(filePath); 

            return lines.Skip(1) 
                .Select(ParseCSVLine)
                .ToList();
        }

        private static MunicipalityTaxRecord ParseCSVLine(string line)
        {
            var cols = line.Split(',');

            if (cols.Length != 5)
            {
                throw new Exception("Incorrect number of columns parsed.");
            }

            TaxFrequency taxFrequency;
            //This section could be more scaleable with more frequency options..
            if (cols[4].ToLower() == "daily") { taxFrequency = TaxFrequency.Daily; }
            else if (cols[4].ToLower() == "weekly") { taxFrequency = TaxFrequency.Weekly; }
            else if (cols[4].ToLower() == "monthly") { taxFrequency = TaxFrequency.Monthly; }
            else if (cols[4].ToLower() == "yearly") { taxFrequency = TaxFrequency.Yearly; }
            else { throw new Exception($"Failed to parse frequency {cols[4]}."); }

            return new MunicipalityTaxRecord
            (
                cols[0],
                DateTime.Parse(cols[1]),
                DateTime.Parse(cols[2]),
                decimal.Parse(cols[3]),
                taxFrequency
            );
        }
    }
}
