using CyclecountBusiness.ViewModels;
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

namespace CyclecountAPI.Controllers
{
    [Route("api/TaskCycleCount")]
    public class TaskCycleCountController : ControllerBase
    {


        [HttpPost("userfilter")]
        public IActionResult userfilter([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new TaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<TaskCycleCountViewModel>(body.ToString());
                var result = service.userfilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("scanSearch")]
        public IActionResult scanSearch([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new ViewTaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<ViewTaskCycleCountViewModel>(body.ToString());
                var result = service.ScanSearch(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }


        [HttpPost("find")]
        public IActionResult find([FromBody]JObject body)
        {
            try
            {
                var service = new TaskCycleCountService();
                var Models = new ViewTaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<ViewTaskCycleCountViewModel>(body.ToString());
                var result = service.find(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ScanLoc")]
        public IActionResult ScanLoc([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new TaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<TaskCycleCountViewModel>(body.ToString());
                var result = service.ScanLoc(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("ScanLpn")]
        public IActionResult ScanLpn([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new TaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<TaskCycleCountViewModel>(body.ToString());
                var result = service.ScanLpn(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("ScanBarcode")]
        public IActionResult ScaScanBarcodenLoc([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new TaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<TaskCycleCountViewModel>(body.ToString());
                var result = service.ScanBarcode(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("ScanCount")]
        public IActionResult ScanCount([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new CycleCountDetailViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountDetailViewModel>(body.ToString());
                var result = service.ScanCount(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("ConfirmTask")]
        public IActionResult ConfirmTask([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new ViewTaskCycleCountViewModel();
                Models = JsonConvert.DeserializeObject<ViewTaskCycleCountViewModel>(body.ToString());
                var result = service.ConfirmTask(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }



        [HttpPost("product")]
        public IActionResult product([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new productViewModel();
                Models = JsonConvert.DeserializeObject<productViewModel>(body.ToString());
                var result = service.product(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }



    }
}
