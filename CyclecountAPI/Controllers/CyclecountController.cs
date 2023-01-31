using CyclecountBusiness.Transfer;
using CyclecountBusiness.ViewModels;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TransferBusiness.Transfer;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CyclecountAPI.Controllers
{
    [Route("api/Cyclecount")]
    public class CyclecountController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public CyclecountController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {


                var service = new CyclecountService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.filter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }




        [HttpPost("BinSearch")]
        public IActionResult BinSearch([FromBody]JObject body)
        {
            try
            {


                var service = new CyclecountService();
                var Models = new BinBalanceLocationViewModel();
                Models = JsonConvert.DeserializeObject<BinBalanceLocationViewModel>(body.ToString());
                var result = service.BinSearch(Models);

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


                var service = new CyclecountService();
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

        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new CyclecountService();
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.Delete(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region PrintOutCycleCount
        [HttpPost("PrintOutCycleCount")]
        public IActionResult PrintReceipt([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new CyclecountService();
                var Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                localFilePath = service.PrintOutCycleCount(Models, _hostingEnvironment.ContentRootPath);
                if (!System.IO.File.Exists(localFilePath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally
            {
                System.IO.File.Delete(localFilePath);
            }
        }
        #endregion

        #region PrintOutCycleCount_excel
        [HttpPost("PrintOutCycleCount_excel")]
        public IActionResult PrintOutCycleCount_excel([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new CyclecountService();
                var Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                localFilePath = service.PrintOutCycleCount_excel(Models, _hostingEnvironment.ContentRootPath);
                if (!System.IO.File.Exists(localFilePath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally
            {
                System.IO.File.Delete(localFilePath);
            }
        }
        
        #endregion


        #region popupProductfilter

        [HttpPost("popupProductfilter")]
        public IActionResult popupProductfilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new SearchProductViewModel();
                Models = JsonConvert.DeserializeObject<SearchProductViewModel>(body.ToString());
                var result = service.popupProductfilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region popupZonefilter

        [HttpPost("popupZonefilter")]
        public IActionResult popupZonefilter([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new SearchZoneViewModel();
                Models = JsonConvert.DeserializeObject<SearchZoneViewModel>(body.ToString());
                var result = service.popupZonefilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPost]
        [Route("ExportCycleCountDetail")]
        //[ResponseType(typeof(int))]
        public IActionResult ExportCycleCountDetail([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                CyclecountService _appService = new CyclecountService();
                var Models = new CycleCountDetailViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountDetailViewModel>(body.ToString());
                StockMovementPath = _appService.ExportCycleCountDetail(Models, _hostingEnvironment.ContentRootPath);

                if (!System.IO.File.Exists(StockMovementPath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(StockMovementPath), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                System.IO.File.Delete(StockMovementPath);
            }
        }

        [HttpPost("confirmStatus")]
        public IActionResult confirmStatus([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.confirmStatus(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Confirm")]
        public IActionResult Confirm([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.Confirm(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AdjustStock")]
        public IActionResult AdjustStock([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new CycleCountViewModel();
                Models = JsonConvert.DeserializeObject<CycleCountViewModel>(body.ToString());
                var result = service.AdjustStock(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
