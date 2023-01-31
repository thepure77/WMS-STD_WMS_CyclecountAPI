using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyclecountBusiness.Transfer;
using MasterBusiness.PlanGoodsIssue;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TransferBusiness.GoodIssue;
using TransferBusiness.Transfer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransferAPI.Controllers
{
    [Route("api/AssignJob")]
    [ApiController]
    public class AssignJobController : ControllerBase
    {
        #region Assign
        [HttpPost("assign")]
        public IActionResult autobasicSuggestion([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new AssignJobViewModel();
                Models = JsonConvert.DeserializeObject<AssignJobViewModel>(body.ToString());
                var result = service.assign(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region taskfilter
        [HttpPost("taskfilter")]
        public IActionResult taskfilter([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new TaskfilterViewModel();
                Models = JsonConvert.DeserializeObject<TaskfilterViewModel>(body.ToString());
                var result = service.taskfilter(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region confirmTask
        [HttpPost("confirmTask")]
        public IActionResult confirmTask([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new TaskfilterViewModel();
                Models = JsonConvert.DeserializeObject<TaskfilterViewModel>(body.ToString());
                var result = service.confirmTask(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region taskPopup
        [HttpPost("taskPopup")]
        public IActionResult taskPopup([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new CycleCountItemViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountItemViewModel>(body.ToString());
                var result = service.taskPopup(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region autoTaskCyclecountNo
        [HttpPost("autoTaskCyclecountNo")]
        public IActionResult autoTaskCyclecountNo([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoTaskCyclecountNo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoCyclecountNo
        [HttpPost("autoCyclecountNo")]
        public IActionResult autoCyclecountNo([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoCyclecountNo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region dropdownUser
        [HttpPost("dropdownUser")]
        public IActionResult dropdownUser([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new UserViewModel();
                Models = JsonConvert.DeserializeObject<UserViewModel>(body.ToString());
                var result = service.dropdownUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region popupCyclecountfilter
        [HttpPost("popupCyclecountfilter")]
        public IActionResult popupGoodsIssuefilter([FromBody]JObject body)
        {
            try
            {
                var service = new AssignService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.popupCyclecountfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


    }
}
