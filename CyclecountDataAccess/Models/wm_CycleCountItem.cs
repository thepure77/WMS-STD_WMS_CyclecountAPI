using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferDataAccess.Models
{


    public partial class wm_CycleCountItem
    {
        [Key]
        public Guid CycleCountItem_Index { get; set; }

        public Guid CycleCount_Index { get; set; }


        public string CycleCount_No { get; set; }

        public Guid? Location_Index { get; set; }


        public string Location_Id { get; set; }


        public string Location_Name { get; set; }
        public Guid? Zone_Index { get; set; }


        public string Zone_Id { get; set; }


        public string Zone_Name { get; set; }


        public Guid? Product_Index { get; set; }


        public string Product_Id { get; set; }


        public string Product_Name { get; set; }


        public string Product_SecondName { get; set; }


        public string Product_ThirdName { get; set; }


        public string Product_Lot { get; set; }

        public int? Count { get; set; }

        public int? CycleCount_Status { get; set; }


        public string DocumentRef_No1 { get; set; }


        public string DocumentRef_No2 { get; set; }


        public string DocumentRef_No3 { get; set; }


        public string DocumentRef_No4 { get; set; }


        public string DocumentRef_No5 { get; set; }

        public int? Document_Status { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }


        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }


        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }


        public DateTime? Cancel_Date { get; set; }


        public string CycleCount_By { get; set; }


        public DateTime? CycleCount_Date { get; set; }
    }
}
