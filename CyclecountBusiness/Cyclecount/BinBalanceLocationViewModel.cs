using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferBusiness.Transfer
{
    public partial class BinBalanceLocationViewModel 
    {

        public BinBalanceLocationViewModel()
        {
            listProductViewModel = new List<ProductViewModel>();

            listZoneViewModel = new List<View_LocatinCyclecountViewModel>();

        }


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
        public Guid?tag_Index { get; set; }

        public string tag_No { get; set; }
        public Guid? warehouse_Index { get; set; }

        public string warehouse_Id { get; set; }
        public string warehouse_Name { get; set; }

        public string create_By { get; set; }
        public bool isSku { get; set; }
        public string movement { get; set; }

        public Guid? cycleCountItem_Index { get; set; }

        public Guid? owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string location_Aisle { get; set; }
        public string location_Bay_Desc { get; set; }
        public string location_Level_Desc { get; set; }
        public string location_Prefix_Desc { get; set; }


        public List<BinBalanceLocationViewModel> listBinLocation { get; set; }
        public List<ProductViewModel> listProductViewModel { get; set; }
        public List<View_LocatinCyclecountViewModel> listZoneViewModel { get; set; }
        public bool isStaging { get; set; }


        public class actionResultBinBalanceLocation
        {
            public IList<BinBalanceLocationViewModel> items { get; set; }
            public Pagination pagination { get; set; }
            public string document_Result { get; set; }

            public string document_No { get; set; }

        }

        public class View_ZoneLocation
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

            public List<View_ZoneLocation> ResultItem { get; set; }


        }
    }
}
