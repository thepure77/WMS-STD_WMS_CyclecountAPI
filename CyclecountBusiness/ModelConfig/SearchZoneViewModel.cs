using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransferBusiness;

namespace MasterDataBusiness.ViewModels
{


    public  class SearchZoneViewModel : Pagination
    {
        public Guid zone_Index { get; set; }

        public string zone_Id { get; set; }

        public string zone_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }
        
        public DateTime? create_Date { get; set; }
        
        public string update_By { get; set; }
        
        public DateTime? update_Date { get; set; }
        
        public string cancel_By { get; set; }
        
        public DateTime? cancel_Date { get; set; }
        
        public string key { get; set; }
    }
    public class actionResultZoneViewModel
    {
        public IList<SearchZoneViewModel> itemsZone { get; set; }
        public Pagination pagination { get; set; }
    }
}
