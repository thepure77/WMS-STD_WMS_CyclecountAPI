using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace TransferDataAccess.Models
{

    public partial class View_TaskCyclecountfilter
    {
        [Key]
        public Guid Task_Index { get; set; }
        public string Task_No { get; set; }

        public string Ref_Document_No { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public string UserAssign { get; set; }
        public string Update_By { get; set; }
        public int? Document_Status { get; set; }

    }
}
