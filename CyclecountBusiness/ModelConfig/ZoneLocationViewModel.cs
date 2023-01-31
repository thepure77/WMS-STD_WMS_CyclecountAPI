using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class ZoneLocationViewModel
    {

        public Guid zoneLocation_Index { get; set; }
        public string zoneLocation_Id { get; set; }

        public Guid? zone_Index { get; set; }
        public string zone_Id { get; set; }
        public string zone_Name { get; set; }

        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }

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

        public string key2 { get; set; }

        //[StringLength(200)]
        //public string ZoneName { get; set; }

        //[StringLength(200)]
        //public string LocationName { get; set; }

        //[StringLength(200)]
        //public string ZoneLocationId { get; set; }

        //public Guid ZoneLocationIndex { get; set; }

        //public Guid? ZoneIndex { get; set; }

        //public Guid? LocationIndex { get; set; }

        //public int? IsActive { get; set; }

        //public int? IsDelete { get; set; }

        //public int? IsSystem { get; set; }

        //public int? StatusId { get; set; }


        //[StringLength(200)]
        //public string CreateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime CreateDate { get; set; }

        //[StringLength(200)]
        //public string UpdateBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? UpdateDate { get; set; }

        //[StringLength(200)]
        //public string CancelBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CancelDate { get; set; }
    }
}
