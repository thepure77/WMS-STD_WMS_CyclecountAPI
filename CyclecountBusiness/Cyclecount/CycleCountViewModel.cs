using CyclecountBusiness.ViewModels;
using System;
using System.Collections.Generic;
using TransferBusiness;
using TransferBusiness.Transfer;

namespace CyclecountBusiness.Transfer
{
    public partial class CycleCountViewModel : Pagination
    {
        public Guid? cycleCount_Index { get; set; }


        public string cycleCount_No { get; set; }

        public Guid? owner_Index { get; set; }


        public string owner_Id { get; set; }


        public string owner_Name { get; set; }

        public Guid? warehouse_Index { get; set; }


        public string warehouse_Id { get; set; }


        public string warehouse_Name { get; set; }

        public Guid? documentType_Index { get; set; }


        public string documentType_Id { get; set; }


        public string documentType_Name { get; set; }


        public string cycleCount_Date { get; set; }

        public string cycleCount_Date_To { get; set; }

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

        public Boolean isSku { get; set; }

        public string processStatus_Name { get; set; }

        public string key { get; set; }

        public List<BinBalanceLocationViewModel> listBinLocation { get; set; }

        public List<CycleCountViewModel> listCycleCount { get; set; }
        public List<CycleCountItemViewModel> listCycleCountItem { get; set; }
        public List<statusViewModel> status { get; set; }
        public List<sortViewModel> sort { get; set; }

        public class actionResult
        {
            public IList<CycleCountViewModel> items { get; set; }
            public CycleCountViewModel listHeader { get; set; }


            public IList<CycleCountItemViewModel> listItem { get; set; }
            public IList<CycleCountDetailViewModel> listDetail { get; set; }

            public Pagination pagination { get; set; }
            public Boolean Message { get; set; }

            public string document_No { get; set; }

        }
        public class statusViewModel
        {
            public int? value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
        public class sortViewModel
        {
            public string value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
        public class SortModel
        {
            public string ColId { get; set; }
            public string Sort { get; set; }

            public string PairAsSqlExpression
            {
                get
                {
                    return $"{ColId} {Sort}";
                }
            }
        }
    }
}
