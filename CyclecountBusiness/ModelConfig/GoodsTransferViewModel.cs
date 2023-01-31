using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TransferBusiness.GoodsTransfer.ViewModel;

namespace TransferBusiness.Transfer
{
    public partial class GoodsTransferViewModel
    {
        [Key]
        public Guid goodsTransfer_Index { get; set; }

        public Guid owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public Guid documentType_Index { get; set; }

        public string documentType_Id { get; set; }

        public string documentType_Name { get; set; }

        public int? documentPriority_Status { get; set; }

        public int? document_Status { get; set; }

        public string goodsTransfer_No { get; set; }

        public string goodsTransfer_Date { get; set; }

        public string goodsTransfer_Time { get; set; }
        public string goodsTransfer_Doc_Date { get; set; }

        public string goodsTransfer_Doc_Time { get; set; }

        public string documentRef_No1 { get; set; }

        public string documentRef_No2 { get; set; }

        public string documentRef_No3 { get; set; }

        public string documentRef_No4 { get; set; }

        public string documentRef_No5 { get; set; }

        public string udf_1 { get; set; }

        public string udf_2 { get; set; }

        public string udf_3 { get; set; }

        public string udf_4 { get; set; }

        public string udf_5 { get; set; }

        public string create_By { get; set; }

        public string create_Date { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string cancel_By { get; set; }

        public string cancel_Date { get; set; }

        public List<PickbinbalanceViewModel> lstPickProduct { get; set; }

        public List<GoodsTransferItemViewModel> listGoodsTransferItemViewModel { get; set; }

        public string tagNoNew { get; set; }

        public string locationNew { get; set; }

        public bool isUseDocumentType { get; set; }
        public string processStatus_Name { get; set; }
        public bool selected { get; set; }


    }

    public partial class ListGoodsTransferViewModel
    {
        public List<GoodsTransferViewModel> items { get; set; }
    }
}
