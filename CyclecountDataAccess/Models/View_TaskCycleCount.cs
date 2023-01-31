using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferDataAccess.Models
{
    public partial class View_TaskCycleCount
    {
        [Key]
        public Guid? Task_Index { get; set; }

        public string Task_No { get; set; }

        public Guid? CycleCount_Index { get; set; }

        public Guid? CycleCountItem_Index { get; set; }

        public string CycleCount_No { get; set; }

        public int? Count { get; set; }
        public Guid? TaskGroup_Index { get; set; }
        public Guid? Location_Index { get; set; }


        public string Location_Id { get; set; }


        public string Location_Name { get; set; }
        public string UserAssign { get; set; }

        public string UserCount { get; set; }

        public int? Document_Status { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Name { get; set; }

    }
}
