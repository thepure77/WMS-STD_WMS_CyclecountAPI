using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferBusiness.Transfer
{
    public partial class BinBalanceLocationManualViewModel
    {

        public Guid? location_Index { get; set; }

        public string location_Id { get; set; }

        public string location_Name { get; set; }

        public Guid? zone_Index { get; set; }

        public string zone_Id { get; set; }

        public string zone_Name { get; set; }

        public Guid? locationType_Index { get; set; }

        public string locationType_Id { get; set; }

        public string locationType_Name { get; set; }

        public Guid? product_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }
        public string product_SecondName { get; set; }
        public string product_ThirdName { get; set; }
        public string product_Lot { get; set; }
        public Guid? tag_Index { get; set; }

        public string tag_No { get; set; }
        public Guid? warehouse_Index { get; set; }

        public string warehouse_Id { get; set; }
        public string warehouse_Name { get; set; }

        public string create_By { get; set; }

        public List<BinBalanceLocationViewModel> listBinLocation { get; set; }

        public class actionResultBinBalanceLocation
        {
            public IList<BinBalanceLocationViewModel> items { get; set; }
            public Pagination pagination { get; set; }
            public string document_Result { get; set; }

            public string document_No { get; set; }

        }
    }
}
