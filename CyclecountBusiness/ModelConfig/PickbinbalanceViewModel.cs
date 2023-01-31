using System;
using System.Collections.Generic;
using System.Text;
using TransferBusiness.ConfigModel;

namespace TransferBusiness.GoodsTransfer.ViewModel
{
    public class PickbinbalanceViewModel
    {
        public string binbalance_Index { get; set; }
        public string goodsReceive_Index { get; set; }
        public string goodsReceive_No { get; set; }
        public string goodsReceive_date { get; set; }
        public string tagItem_Index { get; set; } // add new
        public string tag_Index { get; set; }
        public string tag_No { get; set; }
        public string product_Lot { get; set; }
        public string documentType_Index { get; set; }
        public string documentType_Id { get; set; }
        public string documentType_Name { get; set; }
        public string owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public decimal? weight { get; set; }
        public string productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public string status_Index { get; set; }
        public string status_Id { get; set; }
        public string status_Name { get; set; }
        public string location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public decimal? pick { get; set; }

        public string goodsTransfer_Index { get; set; }
        public string goodsTransfer_No { get; set; }
        public string goodsTransferItem_Index { get; set; }

        // Ref
        public string ref_Document_Index { get; set; }
        public string ref_DocumentItem_Index { get; set; }

        public string create_By { get; set; }
        public string process_Index { get; set; }
        public string binCardReserve_Index { get; set; }

        public string binCard_Index { get; set; }

        public string isuse { get; set; }
        public bool isActive { get; set; }

        public string udf_1 { get; set; }
        public string udf_2 { get; set; }
        public string udf_3 { get; set; }
        public string udf_4 { get; set; }
        public string udf_5 { get; set; }

        public ProductConversionViewModelDoc unit { get; set; }
        //public im_GoodsIssueItemLocation GIIL { get; set; }
        public string itemStatus_Index { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }


        public string goodsReceive_Date { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public string warehouse_Index { get; set; }
        public string warehouse_Id { get; set; }
        public string warehouse_Name { get; set; }
        public string zone_Index { get; set; }
        public string zone_Id { get; set; }
        public string zone_Name { get; set; }

        public decimal? binBalance_UnitWeightBal { get; set; }
        public Guid? binBalance_UnitWeightBal_Index { get; set; }
        public string binBalance_UnitWeightBal_Id { get; set; }
        public string binBalance_UnitWeightBal_Name { get; set; }
        public decimal? binBalance_UnitWeightBalRatio { get; set; }

        public decimal? binBalance_UnitNetWeightBal { get; set; }
        public Guid? binBalance_UnitNetWeightBal_Index { get; set; }
        public string binBalance_UnitNetWeightBal_Id { get; set; }
        public string binBalance_UnitNetWeightBal_Name { get; set; }
        public decimal? binBalance_UnitNetWeightBalRatio { get; set; }

        public decimal? binBalance_UnitGrsWeightBal { get; set; }
        public Guid? binBalance_UnitGrsWeightBal_Index { get; set; }
        public string binBalance_UnitGrsWeightBal_Id { get; set; }
        public string binBalance_UnitGrsWeightBal_Name { get; set; }
        public decimal? binBalance_UnitGrsWeightBalRatio { get; set; }

        public decimal? binBalance_UnitWidthBal { get; set; }
        public Guid? binBalance_UnitWidthBal_Index { get; set; }
        public string binBalance_UnitWidthBal_Id { get; set; }
        public string binBalance_UnitWidthBal_Name { get; set; }
        public decimal? binBalance_UnitWidthBalRatio { get; set; }

        public decimal? binBalance_UnitLengthBal { get; set; }
        public Guid? binBalance_UnitLengthBal_Index { get; set; }
        public string binBalance_UnitLengthBal_Id { get; set; }
        public string binBalance_UnitLengthBal_Name { get; set; }
        public decimal? binBalance_UnitLengthBalRatio { get; set; }

        public decimal? binBalance_UnitHeightBal { get; set; }
        public Guid? binBalance_UnitHeightBal_Index { get; set; }
        public string binBalance_UnitHeightBal_Id { get; set; }
        public string binBalance_UnitHeightBal_Name { get; set; }
        public decimal? binBalance_UnitHeightBalRatio { get; set; }
        public decimal? unitWeight { get; set; }
    }

    public class actionResultPickbinbalanceViewModel : Result
    {
        public PickbinbalanceViewModel items { get; set; }
        public List<PickbinbalanceViewModel> Listitems { get; set; }
        public ResultPickbinbalanceViewModel resultBinbalance { get; set; }
    }
    public class ResultPickbinbalanceViewModel
    {
        public string binbalance_Index { get; set; }
        public string goodsReceive_Index { get; set; }
        public string goodsReceive_No { get; set; }
        public string tag_Index { get; set; }
        public string tag_No { get; set; }
        public string product_Lot { get; set; }
        public string documentType_Index { get; set; }
        public string documentType_Id { get; set; }
        public string documentType_Name { get; set; }
        public string owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public decimal? weight { get; set; }
        public string productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public string status_Index { get; set; }
        public string status_Id { get; set; }
        public string status_Name { get; set; }
        public string location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public decimal? pick { get; set; }

        public string goodsTransfer_Index { get; set; }
        public string goodsTransfer_No { get; set; }
        public string goodsTransfer_Date { get; set; }

        public string goodsTransferItem_Index { get; set; }

        public string create_By { get; set; }

        public string process_Index { get; set; }

        public string binCardReserve_Index { get; set; }

        public string binCard_Index { get; set; }

    }

    public class ListPickbinbalanceViewModel
    {
        public List<PickbinbalanceViewModel> items { get; set; }
    }

}
