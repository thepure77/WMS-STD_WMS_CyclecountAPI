using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferDataAccess.Models
{
    public partial class View_Movement90
    {
        [Key]
        public long? RowIndex { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public string Location_Aisle { get; set; }
        public string Location_Bay_Desc { get; set; }
        public string Location_Level_Desc { get; set; }
        public string Location_Prefix_Desc { get; set; }

    }
}
