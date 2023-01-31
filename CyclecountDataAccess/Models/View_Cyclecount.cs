using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace TransferDataAccess.Models
{

    public partial class View_Cyclecount
    {

        [Key]
        public long? RowIndex { get; set; }

        public Guid CycleCount_Index { get; set; }


        public string CycleCount_No { get; set; }

        public DateTime? CycleCount_Date { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public int? Document_Status { get; set; }


        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }


    }
}
