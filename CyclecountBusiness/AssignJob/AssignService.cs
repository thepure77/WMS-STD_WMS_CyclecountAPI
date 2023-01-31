using Comone.Utils;
using CyclecountBusiness.Transfer;
using DataAccess;
using GIDataAccess.Models;
using MasterBusiness.PlanGoodsIssue;
using MasterDataBusiness.AutoNumber;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TransferBusiness.GoodIssue;
using TransferBusiness.Library;
using TransferDataAccess.Models;

namespace TransferBusiness.Transfer
{
    public class AssignService
    {
        private CyclecountDbContext db;

        public AssignService()
        {
            db = new CyclecountDbContext();
        }
        public AssignService(CyclecountDbContext db)
        {
            this.db = db;
        }

        #region CreateDataTable
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));

            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion

        #region assign
        public String assign(AssignJobViewModel data)
        {


            String State = "Start";
            String msglog = "";
            var olog = new logtxt();


            try
            {

                var CyclecountItem = db.wm_CycleCountItem.Where(c => data.listCyclecountViewModel.Select(s => s.cycleCount_Index).Contains(c.CycleCount_Index)).ToList();

                var Cyclecount = db.wm_CycleCount.Where(c => data.listCyclecountViewModel.Select(s => s.cycleCount_Index).Contains(c.CycleCount_Index)).ToList();

                #region 1 : 1

                if (data.Template == "1")
                {

                    var ViewJoin = (from CYI in CyclecountItem
                                    join CY in Cyclecount on CYI.CycleCount_Index equals CY.CycleCount_Index

                                    select new View_AssignJobViewModel
                                    {
                                        cycleCount_Index = CY.CycleCount_Index,
                                        cycleCount_No = CY.CycleCount_No,
                                        cycleCountItem_Index = CYI.CycleCountItem_Index,
                                        cycleCount_Date = CY.CycleCount_Date,
                                        //qty = CYI.Qty,
                                        //totalQty = CYI.TotalQty,

                                    }).AsQueryable();



                    var ResultGroup = ViewJoin.GroupBy(c => new { c.cycleCount_Index, c.cycleCount_Date })
                     .Select(group => new
                     {
                         CY = group.Key.cycleCount_Index,
                         CYD = group.Key.cycleCount_Date,
                         ResultItem = group.OrderByDescending(o => o.location_Id).ThenByDescending(o => o.product_Id).ThenByDescending(o => o.qty).ToList()
                     }).ToList();

                    foreach (var item in ResultGroup)
                    {
                        this.CreateTask(item.CY, item.CYD, data.Create_By, data.Template);
                    }

                }

                #endregion

                #region Update Status  

                foreach (var ResultCyclecount in Cyclecount)
                {
                    var FindCyclecount = db.wm_CycleCount.Find(ResultCyclecount.CycleCount_Index);
                    FindCyclecount.Document_Status = 2;
                }

                #endregion


                var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveCyclecountTask", msglog);
                    transaction.Rollback();

                    return exy.ToString();

                }

                return "true";

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CreateTask
        public String CreateTask(Guid? Index, DateTime? GID, String Create_By, String Tempalate)
        {
            decimal CYIQty = 0;
            decimal CountQty = 0;
            decimal QtyBreak = 5;
            String TaskTransferIndex = "";
            String TaskCycleCountNo = "";

            try
            {
                var FindCY = db.wm_CycleCount.Where(c => c.CycleCount_Index == Index).ToList();

                foreach (var item in FindCY)
                {
                    #region Create Task Header

                    var result = new wm_TaskCycleCount();


                    var Gen = new List<GenDocumentTypeViewModel>();

                    var filterModel = new GenDocumentTypeViewModel();


                    filterModel.process_Index = new Guid("64E325DA-3952-4C7A-8595-472C5132828D");
                    filterModel.documentType_Index = new Guid("67EEF1BA-51E5-4009-B23F-D58203A6FADD");
                    //GetConfig
                    Gen = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                    DataTable resultDocumentType = CreateDataTable(Gen);

                    var genDoc = new AutoNumberService();
                    string DocNo = "";
                    DateTime DocumentDate = (DateTime)item.CycleCount_Date;
                    DocNo = genDoc.genAutoDocmentNumber(Gen, DocumentDate);
                    TaskCycleCountNo = DocNo;

                    result.Task_Index = Guid.NewGuid();
                    result.Task_No = TaskCycleCountNo;
                    result.Create_By = Create_By;
                    result.Create_Date = DateTime.Now;

                    db.wm_TaskCycleCount.Add(result);

                    #endregion

                    #region Create TaskItem

                    var FindCYI = db.wm_CycleCountItem.Where(c => c.CycleCount_Index == Index && c.Document_Status != -1).ToList();

                    var TaskItem = new List<wm_TaskCycleCountItem>();


                    foreach (var listCYI in FindCYI)
                    {
                        var resultItem = new wm_TaskCycleCountItem();

                        resultItem.TaskItem_Index = Guid.NewGuid();
                        resultItem.Task_Index = result.Task_Index;
                        resultItem.Task_No = TaskCycleCountNo;
                        resultItem.Product_Index = listCYI.Product_Index;
                        resultItem.Product_Id = listCYI.Product_Id;
                        resultItem.Product_Name = listCYI.Product_Name;
                        resultItem.Product_SecondName = listCYI.Product_SecondName;
                        resultItem.Product_ThirdName = listCYI.Product_ThirdName;
                        resultItem.Product_Lot = listCYI.Product_Lot;
                        resultItem.Location_Index = listCYI.Location_Index;
                        resultItem.Location_Id = listCYI.Location_Id;
                        resultItem.Location_Name = listCYI.Location_Name;
                        resultItem.DocumentRef_No1 = listCYI.DocumentRef_No1;
                        resultItem.DocumentRef_No2 = listCYI.DocumentRef_No2;
                        resultItem.DocumentRef_No3 = listCYI.DocumentRef_No3;
                        resultItem.DocumentRef_No4 = listCYI.DocumentRef_No4;
                        resultItem.DocumentRef_No5 = listCYI.DocumentRef_No5;
                        resultItem.Document_Status = 0;
                        resultItem.UDF_1 = listCYI.UDF_1;
                        resultItem.UDF_2 = listCYI.UDF_2;
                        resultItem.UDF_3 = listCYI.UDF_3;
                        resultItem.UDF_4 = listCYI.UDF_2;
                        resultItem.UDF_5 = listCYI.UDF_5;
                        resultItem.Ref_Process_Index = new Guid("64E325DA-3952-4C7A-8595-472C5132828D");
                        resultItem.Ref_Document_Index = listCYI.CycleCount_Index;
                        resultItem.Ref_Document_No = item.CycleCount_No;
                        resultItem.Ref_DocumentItem_Index = listCYI.CycleCountItem_Index;
                        resultItem.Create_By = Create_By;
                        resultItem.Create_Date = DateTime.Now;
                        db.wm_TaskCycleCountItem.Add(resultItem);
                    }

                    #endregion


                }

                return "success";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region taskfilter
        public List<TaskfilterViewModel> taskfilter(TaskfilterViewModel model)
        {
            try
            {
                var query = db.View_TaskCyclecountfilter.Where(c => c.Document_Status != 3).AsQueryable();

                var result = new List<TaskfilterViewModel>();


                if (model.listTaskViewModel.Count != 0)
                {

                    query = query.Where(c => model.listTaskViewModel.Select(s => s.cycleCount_No).Contains(c.Ref_Document_No));

                    var queryresult = query.ToList();

                    foreach (var itemResult in queryresult)
                    {

                        var resultItem = new TaskfilterViewModel();

                        resultItem.task_Index = itemResult.Task_Index;
                        resultItem.task_No = itemResult.Task_No;
                        resultItem.cycleCount_No = itemResult.Ref_Document_No;
                        resultItem.cycleCount_Index = itemResult.Ref_Document_Index;
                        resultItem.userAssign = itemResult.UserAssign;
                        resultItem.update_By = itemResult.Update_By;
                        result.Add(resultItem);

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.cycleCount_No))
                    {
                        query = query.Where(c => c.Ref_Document_No.Contains(model.cycleCount_No));
                    }
                    else
                    {
                        return result;
                    }

                    var queryresult = query.ToList();


                    foreach (var item in queryresult)
                    {
                        var resultItem = new TaskfilterViewModel();

                        resultItem.task_Index = item.Task_Index;
                        resultItem.task_No = item.Task_No;
                        resultItem.cycleCount_No = item.Ref_Document_No;
                        resultItem.cycleCount_Index = item.Ref_Document_Index;
                        resultItem.userAssign = item.UserAssign;
                        resultItem.update_By = item.Update_By;

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
        #endregion

        #region confirmTask
        public String confirmTask(TaskfilterViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            string msg = "Done";
            bool isuserDup = false;

            try
            {
                foreach (var item in data.listTaskViewModel)
                {
                    var isUserTaskitem = db.wm_TaskCycleCountItem.Where(c => c.Ref_Document_Index == item.cycleCount_Index && c.Document_Status != -1).ToList();
                    string msgdup = "";
                    if (isUserTaskitem.Count() > 0)
                    {
                        var isUserTask = db.wm_TaskCycleCount.Where(c => isUserTaskitem.Select(s => s.Task_Index).Contains(c.Task_Index) && c.Document_Status == 3).ToList();

                        foreach (var iut in isUserTask)
                        {
                            if (iut.UserAssign == item.userAssign)
                            {
                                isuserDup = true;
                                msgdup += item.task_No + " นี้ เคยจ่ายงานให้กับคน " + item.userAssign + " แล้ว,";
                            }

                        }
                        msg = !string.IsNullOrEmpty(msgdup) ? msgdup : msg;
                    }
                   
                    if (!isuserDup)
                    {
                        var Task = db.wm_TaskCycleCount.Find(item.task_Index);

                        if (Task != null)
                        {
                            Task.Document_Status = 1;
                            Task.Update_By = item.update_By;
                            Task.Update_Date = DateTime.Now;
                            Task.UserAssign = item.userAssign;
                        }
                    }
                }

                var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("confirmTask", msglog);
                    transaction.Rollback();
                    throw exy;

                }

                return msg;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region taskPopup
        public List<CycleCountItemViewModel> taskPopup(CycleCountItemViewModel model)
        {
            try
            {
                var query = db.wm_CycleCountItem.AsQueryable();


                query = query.Where(c => c.CycleCount_Index == model.cycleCount_Index);



                var Item = query.ToList();

                var result = new List<CycleCountItemViewModel>();


                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");

                var CYquery = db.wm_CycleCount.Where(c => c.CycleCount_Index == model.cycleCount_Index).FirstOrDefault();

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("ProcessStatus"), filterModel.sJson());


                foreach (var item in Item)
                {
                    String Statue = "";
                    Statue = CYquery.Document_Status.ToString();
                    var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();


                    var resultItem = new CycleCountItemViewModel();

                    resultItem.cycleCount_Index = item.CycleCount_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.processStatus_Name = ProcessStatusName?.processStatus_Name;

                    result.Add(resultItem);

                }

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region  autoTaskCyclecountNo
        public List<ItemListViewModel> autoTaskCyclecountNo(ItemListViewModel data)
        {
            try
            {
                var query = db.View_TaskCyclecountfilter.AsQueryable();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Ref_Document_No.Contains(data.key));

                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.Ref_Document_No }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        name = item.Ref_Document_No
                    };
                    items.Add(resultItem);

                }

                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  autoCyclecountNo
        public List<ItemListViewModel> autoCyclecountNo(ItemListViewModel data)
        {
            try
            {
                var query = db.wm_CycleCount.AsQueryable();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.CycleCount_No.Contains(data.key));

                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.CycleCount_No }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        name = item.CycleCount_No
                    };
                    items.Add(resultItem);

                }

                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region DropdownUser
        public List<UserViewModel> dropdownUser(UserViewModel data)
        {
            try
            {
                var result = new List<UserViewModel>();

                //GetConfig
                result = utils.SendDataApi<List<UserViewModel>>(new AppSettingConfig().GetUrl("dropdownUser"), new { userGroupString = new AppSettingConfig().GetUrl("ConfigUserAssign") }.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region popupCyclecountfilter
        public List<CycleCountViewModel> popupCyclecountfilter(CycleCountViewModel data)
        {
            try
            {

                var items = new List<CycleCountViewModel>();



                var query = db.View_Cyclecount.AsQueryable();



                if (!string.IsNullOrEmpty(data.cycleCount_No))
                {
                    query = query.Where(c => c.CycleCount_No == data.cycleCount_No);
                }


                var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("processStatus"), filterModel.sJson());


                foreach (var item in result)
                {
                    var resultItem = new CycleCountViewModel();

                    String Statue = "";
                    Statue = item.Document_Status.ToString();
                    var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();


                    resultItem.cycleCount_Index = item.CycleCount_Index;
                    resultItem.cycleCount_No = item.CycleCount_No;
                    resultItem.cycleCount_Date = item.CycleCount_Date.toString();
                    resultItem.owner_Index = item.Owner_Index.GetValueOrDefault(); ;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.processStatus_Name = ProcessStatusName?.processStatus_Name;

                    items.Add(resultItem);
                }


                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
