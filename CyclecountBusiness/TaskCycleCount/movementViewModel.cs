using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace transferBusiness.Transfer
{
    public partial class movementViewModel
    {
        public Guid? location_Index { get; set; }
    }
}
