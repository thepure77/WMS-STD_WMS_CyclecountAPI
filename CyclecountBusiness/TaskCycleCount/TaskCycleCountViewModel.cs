using System;
using System.Collections.Generic;
using TransferBusiness;

namespace transferBusiness.Transfer
{
    public partial class TaskCycleCountViewModel : Pagination
    {
        public Guid task_Index { get; set; }

        public string task_No { get; set; }

        public Guid? location_Index { get; set; }

        public string location_Id { get; set; }

        public string location_Name { get; set; }

        public Guid? ref_Document_Index { get; set; }

        public string ref_Document_No { get; set; }

        public Guid? ref_DocumentItem_Index { get; set; }

        public string userAssign { get; set; }

        public string userCount { get; set; }

        public int? document_Status { get; set; }

        public int? count { get; set; }

        public DateTime? startCount { get; set; }

        public DateTime? endCount { get; set; }
        public Guid? taskGroup_Index { get; set; }
        public string create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }

        public string productConvertionBarcode { get; set; }

        public string lpn_no { get; set; }
        public string user_Name { get; set; }

        public class ResultTaskCycleCount
        {
            public TaskCycleCountViewModel result { get; set; }

            public IList<BinBalanceCycleCountViewModel> listBinresult { get; set; }
            public BinBalanceCycleCountViewModel Binresult { get; set; }


            public Pagination pagination { get; set; }

            public Boolean Message { get; set; }
            public Boolean Active { get; set; }

            public string document_No { get; set; }
            public Guid? loc_Index { get; set; }

        }

        public class actionResultUserTaskGroup
        {
            public TaskCycleCountViewModel result { get; set; }

            public IList<TaskCycleCountViewModel> items { get; set; }
            public IList<TaskCycleCountViewModel> resultAssign { get; set; }



            public Pagination pagination { get; set; }

            public Boolean Message { get; set; }

            public string document_No { get; set; }

        }

    }
}
