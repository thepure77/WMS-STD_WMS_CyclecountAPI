using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public class View_TaskGroupLocationWorkAreaViewModel
    {

        public Guid? taskGroup_Index { get; set; }
        public string taskGroup_Id { get; set; }
        public string taskGroup_Name { get; set; }
        public Guid? taskGroupWorkArea_Index { get; set; }
        public string taskGroupWorkArea_Id { get; set; }
        public Guid? workArea_Index { get; set; }
        public string workArea_Id { get; set; }
        public string workArea_Name { get; set; }
        public int? task_IsActive { get; set; }
        public int? taskGroupWorkArea_IsActive { get; set; }
        public int? workArea_IsActive { get; set; }
        public Guid? location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public Guid? locationWorkArea_Index { get; set; }
        public string locationWorkArea_Id { get; set; }


    }
}
