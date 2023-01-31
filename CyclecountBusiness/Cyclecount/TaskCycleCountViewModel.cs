using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class TaskCycleCountViewModel
    {

        [Key]
        public Guid task_Index { get; set; }

        public string task_No { get; set; }

        public Guid? location_Index { get; set; }

        public string location_Id { get; set; }

        public string location_Name { get; set; }

        public Guid? ref_Document_Index { get; set; }

        public Guid? ref_DocumentItem_Index { get; set; }

        public string userAssign { get; set; }

        public string userCount { get; set; }

        public int? document_Status { get; set; }

        public int? count { get; set; }

        public DateTime? startCount { get; set; }

        public DateTime? endCount { get; set; }
        public Guid? taskGroup_Index { get; set; }

    }
}
