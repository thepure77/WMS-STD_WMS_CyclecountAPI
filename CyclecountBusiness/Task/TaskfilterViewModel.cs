﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TransferBusiness.GoodIssue
{


    public class TaskfilterViewModel 
    {
        public TaskfilterViewModel()
        {
            listTaskViewModel = new List<listTaskViewModel>();

        }

        public Guid? task_Index { get; set; }


        public string task_No { get; set; }

        public string cycleCount_No { get; set; }

        public Guid? cycleCount_Index { get; set; }
        public string userAssign { get; set; }
        public string update_By { get; set; }

        

        public List<listTaskViewModel> listTaskViewModel { get; set; }

    }

}
