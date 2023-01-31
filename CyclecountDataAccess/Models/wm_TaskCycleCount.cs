using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferDataAccess.Models
{
    public partial class wm_TaskCycleCount
    {
        [Key]
        public Guid Task_Index { get; set; }

        public string Task_No { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public Guid? Ref_Document_Index { get; set; }

        public Guid? Ref_DocumentItem_Index { get; set; }

        public string UserAssign { get; set; }

        public string UserCount { get; set; }

        public int? Document_Status { get; set; }

        public int? Count { get; set; }

        public DateTime? StartCount { get; set; }

        public DateTime? EndCount { get; set; }
        public Guid? TaskGroup_Index { get; set; }
        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }


        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }


        public DateTime? Cancel_Date { get; set; }


    }

}
