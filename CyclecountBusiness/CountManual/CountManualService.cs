using Comone.Utils;
using CyclecountBusiness.Transfer;
using DataAccess;
using MasterDataBusiness.AutoNumber;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TransferBusiness.Library;
using TransferDataAccess.Models;
using static CyclecountBusiness.Transfer.CycleCountViewModel;

namespace TransferBusiness.Transfer
{
    public class CountManualService
    {
        private CyclecountDbContext db;

        public CountManualService()
        {
            db = new CyclecountDbContext();
        }

        public CountManualService(CyclecountDbContext db)
        {
            this.db = db;
        }

        public List<BinBalanceLocationViewModel> scanLoc(BinBalanceLocationViewModel data)
        {
            try
            {


                string pwhereFilter = "";

                var result = new List<BinBalanceLocationViewModel>();


                if (data.location_Name != "" && data.location_Name != null)
                {


                    var resultView = new List<View_LocatinCyclecountViewModel>();
                    var filterModel = new View_LocatinCyclecountViewModel();

                    filterModel.location_Name = data.location_Name;
                    resultView = utils.SendDataApi<List<View_LocatinCyclecountViewModel>>(new AppSettingConfig().GetUrl("ConfigViewCyclecount"), filterModel.sJson());

                    var query = db.wm_BinBalance.Where(c => resultView.Select(s => s.location_Index).Contains(c.Location_Index))
                                .GroupBy(c => new
                                {
                                    c.Location_Index,
                                    c.Location_Id,
                                    c.Location_Name,
                                    c.Product_Index,
                                    c.Product_Id,
                                    c.Product_Name,
                                    c.Product_SecondName
                                })
                                .Select(c => new
                                {
                                    c.Key.Location_Index,
                                    c.Key.Location_Id,
                                    c.Key.Location_Name,
                                    c.Key.Product_Index,
                                    c.Key.Product_Id,
                                    c.Key.Product_Name,
                                    c.Key.Product_SecondName
                                }).ToList();

                    foreach (var item in query)
                    {
                        var resultItem = new BinBalanceLocationViewModel();

                        var where = resultView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();


                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.locationType_Index = where.locationType_Index;
                        resultItem.locationType_Id = where.locationType_Id;
                        resultItem.locationType_Name = where.locationType_Name;
                        resultItem.zone_Index = where.zone_Index;
                        resultItem.zone_Id = where.zone_Id;
                        resultItem.zone_Name = where.zone_Name;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.warehouse_Index = where.warehouse_Index;
                        resultItem.warehouse_Id = where.warehouse_Id;
                        resultItem.warehouse_Name = where.warehouse_Name;

                        result.Add(resultItem);
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BinBalanceLocationManualViewModel> scanLpn(BinBalanceLocationManualViewModel data)
        {
            try
            {



                var result = new List<BinBalanceLocationManualViewModel>();

                var resultView = new List<View_LocatinCyclecountViewModel>();
                var filterModel = new View_LocatinCyclecountViewModel();

                if (data.tag_No != "" && data.tag_No != null)
                {

                    var query = db.wm_BinBalance.Where(c => c.Tag_No == data.tag_No)
                                .GroupBy(c => new
                                {
                                    c.Location_Index,
                                    c.Location_Id,
                                    c.Location_Name,
                                    c.Product_Index,
                                    c.Product_Id,
                                    c.Product_Name,
                                    c.Product_SecondName,
                                    c.Tag_Index,
                                    c.Tag_No
                                })
                                .Select(c => new
                                {
                                    c.Key.Location_Index,
                                    c.Key.Location_Id,
                                    c.Key.Location_Name,
                                    c.Key.Product_Index,
                                    c.Key.Product_Id,
                                    c.Key.Product_Name,
                                    c.Key.Product_SecondName,
                                    c.Key.Tag_Index,
                                    c.Key.Tag_No
                                }).ToList();




                    foreach (var item in query)
                    {
                        var resultItem = new BinBalanceLocationManualViewModel();



                        filterModel.location_Index = item.Location_Index;
                        resultView = utils.SendDataApi<List<View_LocatinCyclecountViewModel>>(new AppSettingConfig().GetUrl("ConfigViewCyclecount"), filterModel.sJson());

                        var where = resultView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();



                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.locationType_Index = where.locationType_Index;
                        resultItem.locationType_Id = where.locationType_Id;
                        resultItem.locationType_Name = where.locationType_Name;
                        resultItem.zone_Index = where.zone_Index;
                        resultItem.zone_Id = where.zone_Id;
                        resultItem.zone_Name = where.zone_Name;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_SecondName = item.Product_SecondName;
                        resultItem.warehouse_Index = where.warehouse_Index;
                        resultItem.warehouse_Id = where.warehouse_Id;
                        resultItem.warehouse_Name = where.warehouse_Name;

                        result.Add(resultItem);
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public actionResult SaveCycleCount(CycleCountViewModel data)
        {

            Guid CycleCountIndex = new Guid();
            String CycleCountNo = "";
            String User = "";

            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            User = data.create_By;
            try
            {

                var actionResult = new actionResult();

                var itemDetail = new List<wm_CycleCountItem>();


                foreach (var item in data.listCycleCount)
                {

                    CycleCountIndex = Guid.NewGuid();

                    //var DocumentType_Index = new SqlParameter("@DocumentType_Index", new Guid("A0C555F8-029B-481B-85D0-257797CD1C52"));

                    //var DocDate = new SqlParameter("@DocDate", DateTime.Now);

                    //var resultParameter = new SqlParameter("@txtReturn", SqlDbType.NVarChar);
                    //resultParameter.Size = 2000; // some meaningfull value
                    //resultParameter.Direction = ParameterDirection.Output;
                    //context.Database.ExecuteSqlCommand("EXEC sp_Gen_DocumentNumber @DocumentType_Index , @DocDate ,@txtReturn OUTPUT", DocumentType_Index, DocDate, resultParameter);
                    ////var result = resultParameter.Value;
                    //CycleCountNo = resultParameter.Value.ToString();

                    var result = new List<GenDocumentTypeViewModel>();
                    var filterModel = new GenDocumentTypeViewModel();

                    filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");
                    filterModel.documentType_Index = new Guid("A0C555F8-029B-481B-85D0-257797CD1C52");
                    //GetConfig
                    result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                    var genDoc = new AutoNumberService();
                    string DocNo = "";
                    DateTime DocumentDate = DateTime.Now;
                    DocNo = genDoc.genAutoDocmentNumber(result, DocumentDate);
                    CycleCountNo = DocNo;

                    wm_CycleCount itemHeader = new wm_CycleCount();

                    itemHeader.CycleCount_Index = CycleCountIndex;
                    itemHeader.CycleCount_No = CycleCountNo;
                    itemHeader.Owner_Index = item.owner_Index;
                    itemHeader.Owner_Id = item.owner_Id;
                    itemHeader.Owner_Name = item.owner_Name;
                    itemHeader.Warehouse_Index = item.warehouse_Index;
                    itemHeader.Warehouse_Id = item.warehouse_Id;
                    itemHeader.Warehouse_Name = item.warehouse_Name;
                    itemHeader.DocumentType_Index = new Guid("A0C555F8-029B-481B-85D0-257797CD1C52");
                    itemHeader.DocumentType_Id = "CY-003";
                    itemHeader.DocumentType_Name = "Manual";
                    itemHeader.CycleCount_Date = DateTime.Now;
                    itemHeader.Document_Status = 0;
                    itemHeader.Create_By = User;
                    itemHeader.Create_Date = DateTime.Now;

                    db.wm_CycleCount.Add(itemHeader);

                    foreach (var listitem in item.listCycleCountItem)
                    {
                        wm_CycleCountItem resultItem = new wm_CycleCountItem();

                        resultItem.CycleCountItem_Index = Guid.NewGuid();
                        resultItem.CycleCount_Index = CycleCountIndex;
                        resultItem.CycleCount_No = CycleCountNo;
                        resultItem.Location_Index = listitem.location_Index;
                        resultItem.Location_Id = listitem.location_Id;
                        resultItem.Location_Name = listitem.location_Name;
                        resultItem.Zone_Index = listitem.zone_Index;
                        resultItem.Zone_Id = listitem.zone_Id;
                        resultItem.Zone_Name = listitem.zone_Name;
                        resultItem.Count = 1;
                        resultItem.Document_Status = 0;
                        resultItem.Create_By = User;
                        resultItem.Create_Date = DateTime.Now;
                        itemDetail.Add(resultItem);

                        db.wm_CycleCountItem.Add(resultItem);
                    }
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }
                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveCycleCount", msglog);
                    transactionx.Rollback();

                    throw exy;

                }


                actionResult.document_No = CycleCountNo;
                actionResult.Message = true;


                return actionResult;




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public String GetTask(List<wm_CycleCountItem> datalist)
        {

            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {


                foreach (var dataItem in datalist)
                {

                    var item = new wm_TaskCycleCount();
                    var result = new List<GenDocumentTypeViewModel>();
                    var filterModel = new GenDocumentTypeViewModel();

                    filterModel.process_Index = new Guid("64E325DA-3952-4C7A-8595-472C5132828D");
                    filterModel.documentType_Index = new Guid("67EEF1BA-51E5-4009-B23F-D58203A6FADD");
                    //GetConfig
                    result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                    var genDoc = new AutoNumberService();
                    string DocNo = "";
                    DateTime DocumentDate = DateTime.Now;
                    DocNo = genDoc.genAutoDocmentNumber(result, DocumentDate);


                    item.Task_Index = Guid.NewGuid();
                    item.Task_No = DocNo;
                    item.Location_Index = dataItem.Location_Index;
                    item.Location_Id = dataItem.Location_Id;
                    item.Location_Name = dataItem.Location_Name;
                    item.Ref_Document_Index = dataItem.CycleCount_Index;
                    item.Ref_DocumentItem_Index = dataItem.CycleCountItem_Index;


                    var resultView = new List<View_TaskGroupLocationWorkAreaViewModel>();
                    var filterModelView = new View_TaskGroupLocationWorkAreaViewModel();
                    filterModelView.location_Index = dataItem.Location_Index;
                    resultView = utils.SendDataApi<List<View_TaskGroupLocationWorkAreaViewModel>>(new AppSettingConfig().GetUrl("ConfigViewTaskGroupLocationWorkArea"), filterModelView.sJson());

                    if (resultView.Count > 0)
                    {
                        item.TaskGroup_Index = resultView.FirstOrDefault().taskGroup_Index;
                    }

                    item.Create_By = dataItem.Create_By;
                    item.Create_Date = DateTime.Now;
                    item.Document_Status = 0;

                    db.wm_TaskCycleCount.Add(item);


                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }
                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveTaskCycleCount", msglog);
                    transactionx.Rollback();

                    throw exy;

                }

                return "Done";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
