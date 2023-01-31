using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyclecountBusiness.ViewModels
{


    public  class View_UserTaskGroupViewModel
    {

        public Guid? user_Index { get; set; }

        public string user_Id { get; set; }

        public string user_Name { get; set; }

        public Guid? taskGroup_Index { get; set; }

        public string taskGroup_Id { get; set; }

        public string taskGroup_Name { get; set; }

        public Guid? taskGroupUser_Index { get; set; }

        public string taskGroupUser_Id { get; set; }
    }


}
