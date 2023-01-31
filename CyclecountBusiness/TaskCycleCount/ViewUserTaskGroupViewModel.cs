using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TransferBusiness;

namespace transferBusiness.Transfer
{
    public partial class ViewUserTaskGroupViewModel: Pagination
    {
        public Guid? user_Index { get; set; }

        public string user_Id { get; set; }

        public string user_Name { get; set; }

        public Guid? taskGroup_Index { get; set; }

        public string taskGroup_Id { get; set; }

        public string taskGroup_Name { get; set; }

        public Guid? taskGroupUser_Index { get; set; }

        public string taskGroupUser_Id { get; set; }

        //public class actionResultUserTaskGroup
        //{
        //    public ViewUserTaskGroupViewModel result { get; set; }

        //    public IList<ViewTaskCycleCountViewModel> items { get; set; }
        //    public IList<ViewTaskCycleCountViewModel> resultAssign { get; set; }



        //    public Pagination pagination { get; set; }

        //    public Boolean Message { get; set; }

        //    public string document_No { get; set; }

        //}

    }
}
