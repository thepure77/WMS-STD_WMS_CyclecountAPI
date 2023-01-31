using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CyclecountBusiness.Transfer
{
    public partial class CycleCountItemViewModel
    {

        public Guid cycleCountItem_Index { get; set; }

        public Guid cycleCount_Index { get; set; }


        public string cycleCount_No { get; set; }

        public Guid? location_Index { get; set; }


        public string location_Id { get; set; }


        public string location_Name { get; set; }
        public Guid? zone_Index { get; set; }


        public string zone_Id { get; set; }

        public string zone_Name { get; set; }



        public Guid? product_Index { get; set; }


        public string product_Id { get; set; }


        public string product_Name { get; set; }


        public string product_SecondName { get; set; }


        public string product_ThirdName { get; set; }


        public string product_Lot { get; set; }

        public int? count { get; set; }

        public int? cycleCount_Status { get; set; }


        public string documentRef_No1 { get; set; }


        public string documentRef_No2 { get; set; }


        public string documentRef_No3 { get; set; }


        public string documentRef_No4 { get; set; }


        public string documentRef_No5 { get; set; }

        public int? document_Status { get; set; }


        public string uDF_1 { get; set; }


        public string uDF_2 { get; set; }


        public string uDF_3 { get; set; }


        public string uDF_4 { get; set; }


        public string uDF_5 { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }


        public string cycleCount_By { get; set; }


        public DateTime? cycleCount_Date { get; set; }
        public string processStatus_Name { get; set; }

        
    }
}
