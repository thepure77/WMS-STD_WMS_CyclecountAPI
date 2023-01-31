using CyclecountBusiness.Transfer;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using transferBusiness.Transfer;
using TransferBusiness.Transfer;
using TransferBusiness.Transfer;
using TaskCycleCountViewModel = transferBusiness.Transfer.TaskCycleCountViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GRAPI.Controllers
{
    [Route("api/CountManual")]
    public class CountManualController : ControllerBase
    {


        [HttpPost("scanLoc")]
        public IActionResult scanLoc([FromBody]JObject body)
        {
            try
            {
                var service = new CountManualService();
                var Models = new BinBalanceLocationViewModel();
                Models = JsonConvert.DeserializeObject<BinBalanceLocationViewModel>(body.ToString());
                var result = service.scanLoc(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("scanLpn")]
        public IActionResult scanLpn([FromBody]JObject body)
        {
            try
            {
                var service = new CountManualService();
                var Models = new BinBalanceLocationManualViewModel();
                Models = JsonConvert.DeserializeObject<BinBalanceLocationManualViewModel>(body.ToString());
                var result = service.scanLpn(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveCycleCount")]
        public IActionResult SaveCycleCount([FromBody]JObject body)
        {
            try
            {


                var service = new CountManualService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.SaveCycleCount(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}
