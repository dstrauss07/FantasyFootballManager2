using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace FantasyFootballManagerWebApp.Methods
{
    public class ImportCsvMethod
    {
        public static IEnumerable<PlayerToImport> ImportCsv(string absolutePath)
        {
            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<PlayerToImport>();
                return records;
            }
        }  
    }

    public class PlayerToImport
    {
        [Index(0)]
        public int PlayerName { get; set; }
        [Index(1)]
        public string PlayerTeam { get; set; }
        [Index(2)]
        public int PlayerPos { get; set; }

    }



}
