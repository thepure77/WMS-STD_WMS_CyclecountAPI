using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransferBusiness;

namespace CyclecountBusiness.ViewModels
{


    public  class CycleCountDetailViewModel : Pagination
    {

        public Guid? cycleCountDetail_Index { get; set; }

        public Guid? cycleCountItem_Index { get; set; }

        public Guid? cycleCount_Index { get; set; }


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


        public decimal? qty_Bal { get; set; }


        public decimal? qty_Count { get; set; }


        public decimal? qty_Diff { get; set; }

        public Guid? tag_Index { get; set; }

        public Guid? tagItem_Index { get; set; }


        public string tag_No { get; set; }


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

        public Guid?  itemStatus_Index { get; set; }


        public string  itemStatus_Id { get; set; }


        public string  itemStatus_Name { get; set; }

        public int? attibute_Count { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string mFG_Date { get; set; }

        public string eXP_Date { get; set; }

        public decimal? sumCountQty { get; set; }

        public decimal? count { get; set; }

        public Guid? task_Index { get; set; }
        public int? isExpDate { get; set; }
        public int? isMfgDate { get; set; }

        public string task_No { get; set; }

        public class ResultCycleCountDetailViewModel
        {
            public CycleCountDetailViewModel result { get; set; }

            public Pagination pagination { get; set; }

            public Boolean Message { get; set; }
            public Boolean Active { get; set; }

            public string document_No { get; set; }

        }
    }
}
