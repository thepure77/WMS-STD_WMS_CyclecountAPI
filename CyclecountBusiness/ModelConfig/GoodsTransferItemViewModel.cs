using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferBusiness.Transfer
{
    public partial class GoodsTransferItemViewModel
    {
        //[Key]
        //public Guid? GoodsTransferItemIndex { get; set; }

        //public Guid? GoodsTransferIndex { get; set; }

        //public Guid? GoodsReceiveIndex { get; set; }

        //public Guid? GoodsReceiveItemIndex { get; set; }

        //public Guid? GoodsReceiveItemLocationIndex { get; set; }

        //[StringLength(50)]
        //public string LineNum { get; set; }

        //public Guid? TagIndex { get; set; }

        //public Guid? TagIndexTo { get; set; }

        //public Guid? ProductIndex { get; set; }

        //public string product_Id { get; set; }

        //public string product_Name { get; set; }

        //public string product_SecondName { get; set; }

        //public Guid? ProductIndexTo { get; set; }

        //public Guid? TagItemIndex { get; set; }

        //[StringLength(200)]
        //public string ProductLot { get; set; }

        //[StringLength(200)]
        //public string ProductLotTo { get; set; }

        //public Guid? ItemStatusIndex { get; set; }

        //public Guid? ItemStatusIndexTo { get; set; }

        //public Guid? ProductConversionIndex { get; set; }

        //public Guid? OwnerIndex { get; set; }

        //public Guid? OwnerIndexTo { get; set; }

        //public Guid? LocationIndex { get; set; }

        //public Guid? LocationIndexTo { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? GoodsReceiveEXPDate { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? GoodsReceiveEXPDateTo { get; set; }

        //public decimal? qty { get; set; }

        //public decimal? TotalQty { get; set; }

        //public decimal? Weight { get; set; }

        //public decimal? Volume { get; set; }

        //public Guid? RefProcessIndex { get; set; }

        //public Guid? RefDocumentIndex { get; set; }

        //[StringLength(200)]
        //public string RefDocumentNo { get; set; }

        //public Guid? RefDocumentItemIndex { get; set; }      

        //[StringLength(200)]
        //public string CreateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CreateDate { get; set; }

        //[StringLength(200)]
        //public string UpdateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? UpdateDate { get; set; }

        //[StringLength(200)]
        //public string CancelBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CancelDate { get; set; }


        public Guid? goodsTransferItem_Index { get; set; }
        public Guid? goodsTransfer_Index { get; set; }
        public Guid? goodsReceive_Index { get; set; }
        public Guid? goodsReceiveItem_Index { get; set; }
        public Guid? goodsReceiveItemLocation_Index { get; set; }
        public string lineNum { get; set; }
        public Guid? tagItem_Index { get; set; }
        public Guid? tag_Index { get; set; }
        public string tag_No { get; set; }
        public Guid? tag_Index_To { get; set; }
        public Guid? product_Index { get; set; }
        public Guid? product_Index_To { get; set; }
        public string product_Lot { get; set; }
        public string product_Lot_To { get; set; }
        public Guid? itemStatus_Index { get; set; }
        public Guid? itemStatus_Index_To { get; set; }
        public Guid? productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public Guid? owner_Index { get; set; }
        public Guid? owner_Index_To { get; set; }
        public Guid? location_Index { get; set; }
        public Guid? location_Index_To { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_MFG_Date_To { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public string goodsReceive_EXP_Date_To { get; set; }
        public decimal? pick { get; set; }
        public decimal? qty { get; set; }
        public decimal? ratio { get; set; }
        public decimal? totalQty { get; set; }

        public decimal? unitWeight { get; set; }
        public Guid? unitWeight_Index { get; set; }
        public string unitWeight_Id { get; set; }
        public string unitWeight_Name { get; set; }
        public decimal? unitWeightRatio { get; set; }

        public decimal? weight { get; set; }
        public Guid? weight_Index { get; set; }
        public string weight_Id { get; set; }
        public string weight_Name { get; set; }
        public decimal? weightRatio { get; set; }

        public decimal? unitNetWeight { get; set; }
        public Guid? unitNetWeight_Index { get; set; }
        public string unitNetWeight_Id { get; set; }
        public string unitNetWeight_Name { get; set; }
        public decimal? unitNetWeightRatio { get; set; }

        public decimal? netWeight { get; set; }
        public Guid? netWeight_Index { get; set; }
        public string netWeight_Id { get; set; }
        public string netWeight_Name { get; set; }
        public decimal? netWeightRatio { get; set; }

        public decimal? unitGrsWeight { get; set; }
        public Guid? unitGrsWeight_Index { get; set; }
        public string unitGrsWeight_Id { get; set; }
        public string unitGrsWeight_Name { get; set; }
        public decimal? unitGrsWeightRatio { get; set; }

        public decimal? grsWeight { get; set; }
        public Guid? grsWeight_Index { get; set; }
        public string grsWeight_Id { get; set; }
        public string grsWeight_Name { get; set; }
        public decimal? grsWeightRatio { get; set; }

        public decimal? unitWidth { get; set; }
        public Guid? unitWidth_Index { get; set; }
        public string unitWidth_Id { get; set; }
        public string unitWidth_Name { get; set; }
        public decimal? unitWidthRatio { get; set; }

        public decimal? width { get; set; }
        public Guid? width_Index { get; set; }
        public string width_Id { get; set; }
        public string width_Name { get; set; }
        public decimal? widthRatio { get; set; }

        public decimal? unitLength { get; set; }
        public Guid? unitLength_Index { get; set; }
        public string unitLength_Id { get; set; }
        public string unitLength_Name { get; set; }
        public decimal? unitLengthRatio { get; set; }

        public decimal? length { get; set; }
        public Guid? length_Index { get; set; }
        public string length_Id { get; set; }
        public string length_Name { get; set; }
        public decimal? lengthRatio { get; set; }

        public decimal? unitHeight { get; set; }
        public Guid? unitHeight_Index { get; set; }
        public string unitHeight_Id { get; set; }
        public string unitHeight_Name { get; set; }
        public decimal? unitHeightRatio { get; set; }

        public decimal? height { get; set; }
        public Guid? height_Index { get; set; }
        public string height_Id { get; set; }
        public string height_Name { get; set; }
        public decimal? heightRatio { get; set; }

        public decimal? unitVolume { get; set; }

        public decimal? volume { get; set; }
        public string documentRef_No1 { get; set; }
        public string documentRef_No2 { get; set; }
        public string documentRef_No3 { get; set; }
        public string documentRef_No4 { get; set; }
        public string documentRef_No5 { get; set; }
        public int? document_Status { get; set; }
        public string udf_1 { get; set; }
        public string udf_2 { get; set; }
        public string udf_3 { get; set; }
        public string udf_4 { get; set; }
        public string udf_5 { get; set; }
        public Guid? ref_Process_Index { get; set; }
        public string ref_Document_No { get; set; }
        public Guid? ref_Document_Index { get; set; }
        public Guid? ref_DocumentItem_Index { get; set; }
        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string update_By { get; set; }
        public string update_Date { get; set; }
        public string cancel_By { get; set; }
        public string cancel_Date { get; set; }
        public string tag_No_To { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_SecondName { get; set; }
        public string product_ThirdName { get; set; }
        public string product_Id_To { get; set; }
        public string product_Name_To { get; set; }
        public string product_SecondName_To { get; set; }
        public string product_ThirdName_To { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public string itemStatus_Id_To { get; set; }
        public string itemStatus_Name_To { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string owner_Id_To { get; set; }
        public string owner_Name_To { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string location_Id_To { get; set; }
        public string location_Name_To { get; set; }

        public DateTime? transfer_Date { get; set; }
        public Guid? binBalance_Index { get; set; }
        public string username { get; set; }
        public string processStatus_Name { get; set; }

    }
}
