using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreDashboard {
    public class SalesData {
        public string Country { get; set; }
        public int Quantity { get; set; }

        public static List<SalesData> GetSalesData() {
            List<SalesData> data = new List<SalesData>();

            data.Add(new SalesData() { Country = "China", Quantity = 320866959 });
            data.Add(new SalesData() { Country = "India", Quantity = 340419115 });
            data.Add(new SalesData() { Country = "United States", Quantity = 58554755 });
            data.Add(new SalesData() { Country = "Indonesia", Quantity = 68715705 });
            data.Add(new SalesData() { Country = "Brazil", Quantity = 50278034 });
            data.Add(new SalesData() { Country = "Russia", Quantity = 26465156 });

            return data;
        }

        public static List<SalesData> GetSalesDataLimited() {
            return SalesData.GetSalesData().Where(sd => sd.Country == "United States").ToList();
        }
    }
}