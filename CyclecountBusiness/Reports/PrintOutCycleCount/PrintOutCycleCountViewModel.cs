using System;
using System.Collections.Generic;
using System.Text;

namespace CyclecountBusiness.Reports.PrintOutCycleCount
{
    public class PrintOutCycleCountViewModel
    {
        public string cyclecount_No { get; set; }
        public string cyclecount_Date { get; set; }
        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty_Bal { get; set; }
        public decimal? qty_Count { get; set; }
        public decimal? qty_Count1 { get; set; }
        public decimal? qty_Count2 { get; set; }
        public decimal? qty_Count3 { get; set; }
        public decimal? qty_Diff { get; set; }
        public string barcode { get; set; }
        public string unit { get; set; }
        public int? counting { get; set; }
        public string location_Prefix_desc { get; set; }
        public string location_Aisle { get; set; }
        public string status { get; set; }
    }
}
