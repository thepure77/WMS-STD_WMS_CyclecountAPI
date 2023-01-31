using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TransferBusiness;

namespace transferBusiness.Transfer
{
    public partial class ViewTaskCycleCountViewModel : Pagination
    {
        public Guid? task_Index { get; set; }

        public string task_No { get; set; }

        public Guid? cycleCount_Index { get; set; }

        public Guid? cycleCountItem_Index { get; set; }

        public string cycleCount_No { get; set; }

        public int? count { get; set; }
        public Guid? taskGroup_Index { get; set; }
        public Guid? location_Index { get; set; }


        public string location_Id { get; set; }


        public string location_Name { get; set; }

        public string userAssign { get; set; }

        public string userCount { get; set; }

        public int? document_Status { get; set; }

        public Guid? product_Index { get; set; }
        public string product_Name { get; set; }
        public class actionResultTaskCycleCount
        {

            public IList<ViewTaskCycleCountViewModel> items { get; set; }
            public ViewTaskCycleCountViewModel Result { get; set; }
            public Pagination pagination { get; set; }

            public Boolean Message { get; set; }

            public string document_No { get; set; }

        }

    }
}
