using System;
using System.ComponentModel.DataAnnotations;
namespace GIDataAccess.Models
{

    public partial class View_BinBalanceLocation
    {
        [Key]
        public long? RowIndex { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public Guid? Zone_Index { get; set; }

        public string Zone_Id { get; set; }

        public string Zone_Name { get; set; }

        public Guid? LocationType_Index { get; set; }

        public string LocationType_Id { get; set; }

        public string LocationType_Name { get; set; }

        public Guid? Product_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_SecondName { get; set; }
        public string Product_ThirdName { get; set; }
        public string Product_Lot { get; set; }
        //public Guid? Tag_Index { get; set; }

        //public string Tag_No { get; set; }
        public Guid? Warehouse_Index { get; set; }

        public string Warehouse_Id { get; set; }
        public string Warehouse_Name { get; set; }

    }
}
