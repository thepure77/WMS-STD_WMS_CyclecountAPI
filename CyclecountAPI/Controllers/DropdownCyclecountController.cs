using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyclecountBusiness.Transfer;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using transferBusiness.Transfer;
using TransferBusiness.Transfer;
//using TransferBusiness.ConfigModel;
//using TransferBusiness.GoodsTransfer.ViewModel;

namespace TransferAPI.Controllers
{
    [Route("api/DropdownCyclecount")]
    public class DropdownCyclecountController : Controller
    {
        #region DropdownDocumentType
        [HttpPost("dropdownDocumentType")]
        public IActionResult dropdownDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.DropdownDocumentType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPost("dropdownProcess")]
        public IActionResult DropdownProcess([FromBody]JObject body)
        {
            try
            {
                var service = new CyclecountService();
                var Models = new ProcessStatusViewModel();
                Models = JsonConvert.DeserializeObject<ProcessStatusViewModel>(body.ToString());
                var result = service.DropdownProcess(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost("dropdownItemStatus")]
        public IActionResult dropdownItemStatus([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new ItemStatusViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusViewModel>(body.ToString());
                var result = service.dropdownItemStatus(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("dropdownTaskGroup")]
        public IActionResult dropdownTaskGroup([FromBody]JObject body)
        {
            try
            {

                var service = new TaskCycleCountService();
                var Models = new TaskGroupViewModel();
                Models = JsonConvert.DeserializeObject<TaskGroupViewModel>(body.ToString());
                var result = service.dropdownTaskGroup(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("dropdownAisle")]
        public IActionResult dropdownAisle([FromBody]JObject body)
        {
            try
            {

                var service = new CyclecountService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.documentTypefilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("dropdownBay")]
        public IActionResult dropdownBay([FromBody]JObject body)
        {
            try
            {

                var service = new CyclecountService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.Bayfilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("dropdownLevel")]
        public IActionResult dropdownLevel([FromBody]JObject body)
        {
            try
            {

                var service = new CyclecountService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.Levelfilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost("dropdownPrefix")]
        public IActionResult dropdownPrefix([FromBody]JObject body)
        {
            try
            {

                var service = new CyclecountService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.Prefixfilter(Models);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}