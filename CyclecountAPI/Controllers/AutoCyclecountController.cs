using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyclecountBusiness.Transfer;
using MasterBusiness.PlanGoodsIssue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using TransferBusiness.ConfigModel;
//using TransferBusiness.GoodsTransfer.ViewModel;

namespace TransferAPI.Controllers
{
    [Route("api/AutoCyclecount")]
    public class AutoCyclecountController : Controller
    {


        #region AutoZone
        [HttpPost("autoZonefilter")]
        public IActionResult autoProdutfilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.AutoZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationTypFilter
        [HttpPost("autoLocationTypFilter")]
        public IActionResult autoLocationTypFilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationTypFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoLocationFilter
        [HttpPost("autoLocationFilter")]
        public IActionResult autoLocationFilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoLocationFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductFilter
        [HttpPost("autoProductFilter")]
        public IActionResult autoProductFilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductFilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoCycleCountNo
        [HttpPost("autoCycleCountNo")]
        public IActionResult autoCycleCountNo([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoCycleCountNo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoOwnerfilter
        [HttpPost("autoOwnerfilter")]
        public IActionResult autoOwnerfilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerfilter(Models);
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