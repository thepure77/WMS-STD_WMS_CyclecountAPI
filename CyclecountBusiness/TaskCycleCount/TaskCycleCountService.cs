using Comone.Utils;
using CyclecountBusiness.ViewModels;
using DataAccess;
using MasterDataBusiness.AutoNumber;
using MasterDataBusiness.Product;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using transferBusiness.Transfer;
using TransferBusiness.GoodIssue;
using TransferBusiness.Library;
using TransferDataAccess.Models;
using static CyclecountBusiness.ViewModels.CycleCountDetailViewModel;
using static transferBusiness.Transfer.TaskCycleCountViewModel;
using static transferBusiness.Transfer.ViewTaskCycleCountViewModel;
using static transferBusiness.Transfer.ViewUserTaskGroupViewModel;
using TaskCycleCountViewModel = transferBusiness.Transfer.TaskCycleCountViewModel;

namespace TransferBusiness.Transfer
{
    public class TaskCycleCountService
    {
        private CyclecountDbContext db;

        public TaskCycleCountService()
        {
            db = new CyclecountDbContext();
        }

        public TaskCycleCountService(CyclecountDbContext db)
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

        public actionResultUserTaskGroup userfilter(TaskCycleCountViewModel data)
        {
            try
            {
                var result = new TaskCycleCountViewModel();




                var results = new List<TaskCycleCountViewModel>();

                //var queryResultTask = db.wm_TaskCycleCount.Where(c => c.UserAssign == data.user_Name &&
                //                      c.Document_Status != 2 && c.Document_Status != 3 && c.Document_Status != -1).AsQueryable();

                var queryResultTask = (from h in db.wm_TaskCycleCount
                                       join d in db.wm_TaskCycleCountItem on h.Task_Index equals d.Task_Index
                                       where h.UserAssign == data.user_Name
                                       && h.Document_Status != 2
                                       && h.Document_Status != 3
                                       && h.Document_Status != -1
                                       group d by new
                                       {
                                           task_Index = h.Task_Index,
                                           task_No = h.Task_No,
                                           location_Index = h.Location_Index,
                                           location_Id = h.Location_Id,
                                           location_Name = h.Location_Name,
                                           count = h.Count,
                                           userAssign = h.UserAssign,
                                           userCount = h.UserCount,
                                           document_Status = h.Document_Status,
                                           ref_Document_No = d.Ref_Document_No
                                       } into gh
                                       select new TaskCycleCountViewModel
                                       {
                                           task_Index = gh.Key.task_Index
                                           ,
                                           task_No = gh.Key.task_No
                                           ,
                                           location_Index = gh.Key.location_Index
                                           ,
                                           location_Id = gh.Key.location_Id
                                           ,
                                           location_Name = gh.Key.location_Name
                                           ,
                                           count = gh.Key.count
                                           ,
                                           userAssign = gh.Key.userAssign
                                           ,
                                           userCount = gh.Key.userCount
                                           ,
                                           document_Status = gh.Key.document_Status
                                           ,
                                           ref_Document_No = gh.Key.ref_Document_No
                                       }
                                      ).AsQueryable();


                if (!string.IsNullOrEmpty(data.task_No))
                {
                    queryResultTask = queryResultTask.Where(c => c.task_No == (data.task_No) || c.ref_Document_No == data.task_No);
                }




                var Item = new List<TaskCycleCountViewModel>();

                var TotalRow = new List<TaskCycleCountViewModel>();

                //var perpages = data.PerPage == 0 ? queryResultTask.ToList() : queryResultTask.Skip((data.CurrentPage - 1) * data.PerPage).Take(data.PerPage).ToList();

                TotalRow = queryResultTask.OrderByDescending(o => o.task_No).ThenByDescending(o => o.task_No).ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    queryResultTask = queryResultTask.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    queryResultTask = queryResultTask.Take(data.PerPage);

                }

                Item = queryResultTask.OrderByDescending(o => o.task_No).ThenByDescending(o => o.task_No).ToList();

                foreach (var item in Item)
                {
                    var resultItem = new TaskCycleCountViewModel();

                    resultItem.task_Index = item.task_Index;
                    resultItem.task_No = item.task_No;
                    //resultItem.cycleCount_Index = item.CycleCount_Index;
                    //resultItem.cycleCountItem_Index = item.CycleCountItem_Index;
                    //resultItem.cycleCount_No = item.CycleCount_No;
                    resultItem.location_Index = item.location_Index;
                    resultItem.location_Id = item.location_Id;
                    resultItem.location_Name = item.location_Name;
                    resultItem.count = item.count;
                    resultItem.userAssign = item.userAssign;
                    resultItem.userCount = item.userCount;
                    resultItem.document_Status = item.document_Status;
                    //resultItem.product_Index = item.Product_Index;
                    //resultItem.product_Name = item.Product_Name;
                    resultItem.ref_Document_No = item.ref_Document_No;
                    results.Add(resultItem);
                }

                //var queryResultTask1 = db.wm_TaskCycleCount.Where(c => c.UserAssign == data.user_Name &&
                //                      c.Document_Status == 2).ToList();

                var queryResultTask1 = (from h in db.wm_TaskCycleCount
                                        join d in db.wm_TaskCycleCountItem on h.Task_Index equals d.Task_Index
                                        where h.UserAssign == data.user_Name
                                        && h.Document_Status == 2
                                        group d by new
                                        {
                                            task_Index = h.Task_Index,
                                            task_No = h.Task_No,
                                            //,cycleCount_Index = h.CycleCount_Index
                                            //,cycleCountItem_Index = h.CycleCountItem_Index
                                            //,cycleCount_No = h.CycleCount_No
                                            location_Index = h.Location_Index,
                                            location_Id = h.Location_Id,
                                            location_Name = h.Location_Name,
                                            count = h.Count,
                                            userAssign = h.UserAssign,
                                            userCount = h.UserCount,
                                            document_Status = h.Document_Status,
                                            ref_Document_No = d.Ref_Document_No
                                            //,product_Index = item.Product_Index
                                            //,product_Name = item.Product_Name
                                        } into gh
                                        select new TaskCycleCountViewModel
                                        {
                                            task_Index = gh.Key.task_Index
                                            ,
                                            task_No = gh.Key.task_No
                                            //,cycleCount_Index = gh.key.CycleCount_Index
                                            //,cycleCountItem_Index = gh.key.CycleCountItem_Index
                                            //,cycleCount_No = gh.key.CycleCount_No
                                            ,
                                            location_Index = gh.Key.location_Index
                                            ,
                                            location_Id = gh.Key.location_Id
                                            ,
                                            location_Name = gh.Key.location_Name
                                            ,
                                            count = gh.Key.count
                                            ,
                                            userAssign = gh.Key.userAssign
                                            ,
                                            userCount = gh.Key.userCount
                                            ,
                                            document_Status = gh.Key.document_Status
                                            ,
                                            ref_Document_No = gh.Key.ref_Document_No
                                            //,product_Index = ggh.key.key.Product_Index
                                            //,product_Name = gh.key.Product_Name
                                        }
                                    ).ToList();

                var listAssign = new List<TaskCycleCountViewModel>();


                foreach (var item in queryResultTask1)
                {
                    var result1 = new TaskCycleCountViewModel();

                    result1.task_Index = item.task_Index;
                    result1.task_No = item.task_No;
                    //result1.cycleCount_Index = item.CycleCount_Index;
                    //result1.cycleCountItem_Index = item.CycleCountItem_Index;
                    //result1.cycleCount_No = item.CycleCount_No;
                    result1.location_Index = item.location_Index;
                    result1.location_Id = item.location_Id;
                    result1.location_Name = item.location_Name;
                    result1.count = item.count;
                    result1.userAssign = item.userAssign;
                    result1.userCount = item.userCount;
                    result1.document_Status = item.document_Status;
                    //result1.product_Index = item.Product_Index;
                    //result1.product_Name = item.Product_Name;
                    result1.ref_Document_No = item.ref_Document_No;

                    listAssign.Add(result1);
                }



                var actionResult = new actionResultUserTaskGroup();
                var count = TotalRow.Count;

                actionResult.result = result;
                actionResult.items = results.ToList();
                actionResult.resultAssign = listAssign.ToList();

                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResult;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public actionResultTaskCycleCount ScanSearch(ViewTaskCycleCountViewModel data)
        {
            try
            {

                var result = new List<ViewTaskCycleCountViewModel>();


                var query = db.View_TaskCycleCount.AsQueryable();

                query = query.Where(c => c.Document_Status != -1);

                if (!string.IsNullOrEmpty(data.cycleCount_No))
                {
                    query = query.Where(c => c.CycleCount_No == (data.cycleCount_No));
                }

                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    query = query.Where(c => c.Location_Name == (data.location_Name));
                }
                if (!string.IsNullOrEmpty(data.taskGroup_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    query = query.Where(c => c.TaskGroup_Index == (data.taskGroup_Index));
                }


                var Item = new List<View_TaskCycleCount>();

                var TotalRow = new List<View_TaskCycleCount>();


                TotalRow = query.OrderByDescending(o => o.Task_No).ThenByDescending(o => o.Task_No).ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderByDescending(o => o.Task_No).ThenByDescending(o => o.Task_No).ToList();


                foreach (var item in Item)
                {
                    var resultItem = new ViewTaskCycleCountViewModel();

                    resultItem.task_Index = item.Task_Index;
                    resultItem.task_No = item.Task_No;
                    resultItem.cycleCount_Index = item.CycleCount_Index;
                    resultItem.cycleCountItem_Index = item.CycleCountItem_Index;
                    resultItem.cycleCount_No = item.CycleCount_No;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.count = item.Count;
                    resultItem.userAssign = item.UserAssign;
                    resultItem.userCount = item.UserCount;
                    resultItem.document_Status = item.Document_Status;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.product_Name = item.Product_Name;


                    result.Add(resultItem);

                }


                var actionResult = new actionResultTaskCycleCount();
                var count = TotalRow.Count;
                actionResult.items = result.ToList();

                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage };

                return actionResult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public actionResultTaskCycleCount find(ViewTaskCycleCountViewModel data)
        {

            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var actionResult = new actionResultTaskCycleCount();


                var result = new ViewTaskCycleCountViewModel();




                var queryResultCheck = db.wm_TaskCycleCount.Where(c => c.UserAssign == data.userAssign && c.Document_Status == 2).FirstOrDefault();

                if (queryResultCheck == null)
                {


                    var queryResult = db.wm_TaskCycleCount.Where(c => c.Task_Index == data.task_Index).FirstOrDefault();


                    if (queryResult != null)
                    {
                        var Update = db.wm_TaskCycleCount.Find(queryResult.Task_Index);

                        Update.Document_Status = 2;
                        Update.UserAssign = data.userAssign;

                        //result.cycleCount_Index = queryResult.CycleCount_Index;
                        //result.cycleCount_No = queryResult.CycleCount_No;
                        result.task_No = queryResult.Task_No;
                        result.task_Index = queryResult.Task_Index;

                        var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            db.SaveChanges();
                            transaction.Commit();

                            actionResult.Result = result;
                            actionResult.Message = true;
                        }

                        catch (Exception exy)
                        {
                            msglog = State + " ex Rollback " + exy.Message.ToString();
                            olog.logging("UpdateTaskCycleCount", msglog);
                            transaction.Rollback();
                            throw exy;
                        }
                    }
                }
                else
                {
                    actionResult.Message = false;
                }

                return actionResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResultTaskCycleCount ScanLoc(TaskCycleCountViewModel data)
        {
            try
            {
                var actionResult = new ResultTaskCycleCount();



                var queryResult = db.wm_TaskCycleCountItem.Where(c => c.Task_Index == data.task_Index && c.Location_Name == data.location_Name).FirstOrDefault();


                if (queryResult == null)
                {
                    actionResult.Message = false;
                }
                else
                {
                    actionResult.Message = true;
                    actionResult.loc_Index = queryResult.Location_Index;

                    //var queryResultLoT = new List<LocationTypeViewModel>();
                    //var filterModel = new LocationConfigViewModel();

                    //filterModel.location_Index = queryResult.Location_Index;
                    //queryResultLoT = utils.SendDataApi<List<LocationTypeViewModel>>(new AppSettingConfig().GetUrl("ConfigfindlocationType"), filterModel.sJson());

                    //if (queryResultLoT.FirstOrDefault().locationType_Name == "Active")
                    //{
                    //    actionResult.Active = true;
                    //    actionResult.Message = true;
                    //}
                    //else
                    //{
                    //    actionResult.Active = false;
                    //    actionResult.Message = true;
                    //}
                }

                return actionResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ResultTaskCycleCount ScanLpn(TaskCycleCountViewModel data)
        {
            try
            {

                var actionResult = new ResultTaskCycleCount();


                var result = new BinBalanceCycleCountViewModel();


                var itemBi = db.wm_BinBalance.Where(c => c.Tag_No == data.lpn_no && c.Location_Index == data.location_Index).FirstOrDefault();


                if (itemBi == null)
                {
                    actionResult.Message = false;
                }
                else
                {
                    result.binBalance_Index = itemBi.BinBalance_Index;

                    actionResult.Binresult = result;
                    actionResult.Message = true;
                }


                return actionResult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ResultTaskCycleCount ScanBarcode(TaskCycleCountViewModel data)
        {
            try
            {

                var actionResult = new ResultTaskCycleCount();


                var result = new List<BinBalanceCycleCountViewModel>();


                var queryResult = new List<BarcodeViewModel>();
                var filterModel = new BarcodeViewModel();

                filterModel.productConversionBarcode = data.productConvertionBarcode;
                queryResult = utils.SendDataApi<List<BarcodeViewModel>>(new AppSettingConfig().GetUrl("configBarcode"), filterModel.sJson());



                if (queryResult.Count <= 0)
                {
                    actionResult.Message = false;
                }

                else
                {

                    var chkProduct = db.wm_TaskCycleCountItem.Where(c => c.Task_Index == data.task_Index && c.Location_Index == data.location_Index && (c.Product_Index == queryResult.FirstOrDefault().product_Index || c.Product_Index == null)).Count();
                    if (chkProduct == 0)
                    {
                        actionResult.Message = false;
                        return actionResult;
                    }


                    var itemBi = db.wm_BinBalance.Where(c => c.Product_Index == queryResult.FirstOrDefault().product_Index && c.Location_Index == data.location_Index)
                         .GroupBy(c => new
                         {
                             c.Product_Index,
                             c.Product_Id,
                             c.Product_Name,
                             c.Product_SecondName,
                             c.Product_ThirdName,
                             c.ProductConversion_Index,
                             c.ProductConversion_Id,
                             c.ProductConversion_Name
                         })
                         .Select(c => new
                         {
                             c.Key.Product_Index,
                             c.Key.Product_Id,
                             c.Key.Product_Name,
                             c.Key.Product_SecondName,
                             c.Key.Product_ThirdName,
                             c.Key.ProductConversion_Index,
                             c.Key.ProductConversion_Id,
                             c.Key.ProductConversion_Name,
                             SumQty = c.Sum(s => s.BinBalance_QtyBal)
                         }).ToList();

                    if (itemBi.Count <= 0)
                    {
                        foreach (var listitem in queryResult)
                        {
                            var resultItem = new BinBalanceCycleCountViewModel();

                            resultItem.product_Index = listitem.product_Index;
                            resultItem.product_Id = listitem.product_Id;
                            resultItem.product_Name = listitem.product_Name;
                            resultItem.productConversion_Index = listitem.productConversion_Index;
                            resultItem.productConversion_Id = listitem.productConversion_Id;
                            resultItem.productConversion_Name = listitem.productConversion_Name;
                            resultItem.product_SecondName = listitem.product_SecondName;
                            resultItem.product_ThirdName = listitem.product_ThirdName;
                            resultItem.SumCountQty = 0;

                            result.Add(resultItem);
                        }
                        actionResult.Message = true;

                    }

                    else
                    {
                        foreach (var listitem in itemBi)
                        {
                            var resultItem = new BinBalanceCycleCountViewModel();

                            resultItem.product_Index = listitem.Product_Index;
                            resultItem.product_Id = listitem.Product_Id;
                            resultItem.product_Name = listitem.Product_Name;
                            resultItem.productConversion_Index = listitem.ProductConversion_Index;
                            resultItem.productConversion_Id = listitem.ProductConversion_Id;
                            resultItem.productConversion_Name = listitem.ProductConversion_Name;
                            resultItem.product_SecondName = listitem.Product_SecondName;
                            resultItem.product_ThirdName = listitem.Product_ThirdName;

                            resultItem.SumCountQty = listitem.SumQty;

                            result.Add(resultItem);
                        }
                        actionResult.Message = true;
                    }


                    actionResult.listBinresult = result.ToList();
                }


                return actionResult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResultCycleCountDetailViewModel ScanCount(CycleCountDetailViewModel data)
        {

            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            decimal? sumCountQty = 0;


            var actionResult = new ResultCycleCountDetailViewModel();

            try
            {

                var TaskCycleCountOld = db.wm_TaskCycleCount.Find(data.task_Index);

                if (TaskCycleCountOld != null)
                {
                    TaskCycleCountOld.UserCount = data.create_By;
                    TaskCycleCountOld.StartCount = DateTime.Now;
                    TaskCycleCountOld.Update_By = data.create_By;
                    TaskCycleCountOld.Update_Date = DateTime.Now;
                }

                var itemBi = db.wm_BinBalance.Where(c => c.Product_Index == data.product_Index
                                                          && c.Location_Index == data.location_Index)

                         .GroupBy(c => new
                         {
                             c.Product_Index,
                             c.Product_Id,
                             c.Product_Name,
                             c.Product_SecondName,
                             c.ProductConversion_Index,
                             c.ProductConversion_Id,
                             c.ProductConversion_Name,
                             c.ItemStatus_Index,
                             c.ItemStatus_Id,
                             c.ItemStatus_Name,
                         }).Select(c => new
                         {
                             c.Key.Product_Index,
                             c.Key.Product_Id,
                             c.Key.Product_Name,
                             c.Key.Product_SecondName,
                             c.Key.ProductConversion_Index,
                             c.Key.ProductConversion_Id,
                             c.Key.ProductConversion_Name,
                             c.Key.ItemStatus_Index,
                             c.Key.ItemStatus_Id,
                             c.Key.ItemStatus_Name,
                             SumQty = c.Sum(s => s.BinBalance_QtyBal)
                         }).FirstOrDefault();


                if (itemBi != null)
                {
                    sumCountQty = itemBi.SumQty;
                }

                var queryResultTaskItem = db.wm_TaskCycleCountItem.Where(c => c.Task_No == data.task_No && c.Location_Index == data.location_Index && (c.Product_Index == data.product_Index || c.Product_Index == null)).FirstOrDefault();

                //var queryResultDetail = db.wm_CycleCountDetail.Where(c => c.CycleCountItem_Index == queryResultTaskItem.Ref_DocumentItem_Index
                //                                                      && c.Product_Index == data.product_Index).FirstOrDefault();

                var queryResultDetail = db.wm_CycleCountDetail.Where(c => c.Ref_Document_Index == queryResultTaskItem.Task_Index && c.Ref_DocumentItem_Index == queryResultTaskItem.TaskItem_Index
                                                                   && c.Product_Index == data.product_Index).FirstOrDefault();

                if (queryResultDetail != null)
                {
                    var CyDetailOld = db.wm_CycleCountDetail.Find(queryResultDetail.CycleCountDetail_Index);

                    CyDetailOld.Qty_Bal = sumCountQty;
                    CyDetailOld.Qty_Count = data.count;
                    CyDetailOld.Qty_Diff = CyDetailOld.Qty_Bal - CyDetailOld.Qty_Count;
                    CyDetailOld.Update_By = data.create_By;
                    CyDetailOld.Update_Date = DateTime.Now;
                }
                else
                {
                    var queryResultItem = db.wm_CycleCountItem.Where(c => c.CycleCountItem_Index == queryResultTaskItem.Ref_DocumentItem_Index).FirstOrDefault();

                    wm_CycleCountDetail CyDetail = new wm_CycleCountDetail();

                    CyDetail.CycleCount_Index = queryResultItem.CycleCount_Index;
                    CyDetail.CycleCountItem_Index = queryResultItem.CycleCountItem_Index;
                    CyDetail.CycleCountDetail_Index = Guid.NewGuid();
                    CyDetail.CycleCount_No = queryResultItem.CycleCount_No;
                    CyDetail.Location_Index = queryResultItem.Location_Index;
                    CyDetail.Location_Id = queryResultItem.Location_Id;
                    CyDetail.Location_Name = queryResultItem.Location_Name;
                    CyDetail.Zone_Index = queryResultItem.Zone_Index;
                    CyDetail.Zone_Id = queryResultItem.Zone_Id;
                    CyDetail.Zone_Name = queryResultItem.Zone_Name;
                    CyDetail.Product_Index = data.product_Index;
                    CyDetail.Product_Id = data.product_Id;
                    CyDetail.Product_Name = data.product_Name;
                    CyDetail.Product_SecondName = data.product_SecondName;
                    CyDetail.Product_ThirdName = data.product_ThirdName;
                    CyDetail.Product_Lot = data.product_Lot;
                    CyDetail.Qty_Bal = sumCountQty;
                    CyDetail.Qty_Count = data.count;
                    CyDetail.Qty_Diff = CyDetail.Qty_Bal - CyDetail.Qty_Count;
                    CyDetail.Tag_Index = data.tag_Index;
                    CyDetail.TagItem_Index = data.tagItem_Index;
                    CyDetail.Tag_No = data.tag_No;
                    CyDetail.DocumentRef_No1 = data.documentRef_No1;
                    CyDetail.DocumentRef_No2 = data.documentRef_No2;
                    CyDetail.DocumentRef_No3 = data.documentRef_No3;
                    CyDetail.DocumentRef_No4 = data.documentRef_No4;
                    CyDetail.DocumentRef_No5 = data.documentRef_No5;
                    CyDetail.Document_Status = 0;
                    CyDetail.UDF_1 = data.uDF_1;
                    CyDetail.UDF_2 = data.uDF_2;
                    CyDetail.UDF_3 = data.uDF_3;
                    CyDetail.UDF_4 = data.uDF_4;
                    CyDetail.UDF_5 = data.uDF_5;
                    CyDetail.ItemStatus_Index = itemBi.ItemStatus_Index;
                    CyDetail.ItemStatus_Id = itemBi.ItemStatus_Id;
                    CyDetail.ItemStatus_Name = itemBi.ItemStatus_Name;
                    CyDetail.MFG_Date = data.mFG_Date.toDate();
                    CyDetail.EXP_Date = data.eXP_Date.toDate();
                    CyDetail.Create_By = data.create_By;
                    CyDetail.Create_Date = DateTime.Now;
                    CyDetail.Ref_Document_No = queryResultTaskItem.Task_No;
                    CyDetail.Ref_Document_Index = queryResultTaskItem.Task_Index;
                    CyDetail.Ref_DocumentItem_Index = queryResultTaskItem.TaskItem_Index;
                    CyDetail.Counting = db.wm_TaskCycleCountItem.Where(c => c.Ref_Document_Index == queryResultItem.CycleCount_Index).GroupBy(g => g.Task_Index).Count();

                    db.wm_CycleCountDetail.Add(CyDetail);
                }



                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    actionResult.Message = true;
                    db.SaveChanges();
                    transactionx.Commit();
                }
                catch (Exception exy)
                {
                    actionResult.Message = false;

                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SaveCycleCountDetail", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return actionResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public actionResultTaskCycleCount ConfirmTask(ViewTaskCycleCountViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var actionResult = new actionResultTaskCycleCount();

                //var taskitemdetail = db.wm_CycleCountDetail.Where(c => c.Ref_Document_Index == data.task_Index).Count();

                //if (taskitemdetail == 0)
                //{
                //    actionResult.Message = false;
                //    return actionResult;
                //}

                var TaskCycleCountOld = db.wm_TaskCycleCount.Find(data.task_Index);

                if (TaskCycleCountOld != null)
                {
                    TaskCycleCountOld.Document_Status = 3;
                    TaskCycleCountOld.EndCount = DateTime.Now;
                    TaskCycleCountOld.Update_By = data.userAssign;
                    TaskCycleCountOld.Update_Date = DateTime.Now;


                    var taskItem = db.wm_TaskCycleCountItem.Where(c => c.Task_Index == TaskCycleCountOld.Task_Index).FirstOrDefault();

                    var CycleCountOld = db.wm_CycleCount.Find(taskItem.Ref_Document_Index);

                    if (CycleCountOld != null)
                    {

                        var CyclecountItem = db.wm_CycleCountItem.Where(c => c.CycleCount_Index == CycleCountOld.CycleCount_Index && c.Count < 3).ToList();


                        if (!string.IsNullOrEmpty(CyclecountItem?.FirstOrDefault()?.Product_Id))
                        {
                            var CountCyclecountDatail = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == CycleCountOld.CycleCount_Index && c.Ref_Document_Index == TaskCycleCountOld.Task_Index).ToList();

                            var ViewJoinDetail = CyclecountItem.Where(c => !CountCyclecountDatail.Select(s => s.CycleCountItem_Index).Contains(c.CycleCountItem_Index)).ToList();

                            foreach (var vjd in ViewJoinDetail)
                            {
                                var dataDetail =  new CycleCountDetailViewModel
                                {
                                    cycleCountItem_Index = vjd.CycleCountItem_Index,
                                    cycleCount_Index = vjd.CycleCount_Index,
                                    cycleCount_No = vjd.CycleCount_No,
                                    location_Index = vjd.Location_Index,
                                    location_Id = vjd.Location_Id,
                                    location_Name = vjd.Location_Name,
                                    zone_Index = vjd.Zone_Index,
                                    zone_Id = vjd.Zone_Id,
                                    zone_Name = vjd.Zone_Name,
                                    product_Index = vjd.Product_Index,
                                    product_Id = vjd.Product_Id,
                                    product_Name = vjd.Product_Name,
                                    product_SecondName = vjd.Product_SecondName,
                                    product_ThirdName = vjd.Product_ThirdName,
                                    product_Lot = vjd.Product_Lot,
                                    documentRef_No1 = vjd.DocumentRef_No1,
                                    documentRef_No2 = vjd.DocumentRef_No2,
                                    documentRef_No3 = vjd.DocumentRef_No3,
                                    documentRef_No4 = vjd.DocumentRef_No4,
                                    documentRef_No5 = vjd.DocumentRef_No5,
                                    uDF_1 = vjd.UDF_1,
                                    uDF_2 = vjd.UDF_2,
                                    uDF_3 = vjd.UDF_3,
                                    uDF_4 = vjd.UDF_4,
                                    uDF_5 = vjd.UDF_5,
                                    count = null,
                                    task_Index = TaskCycleCountOld.TaskGroup_Index,
                                    task_No = TaskCycleCountOld.Task_No
                                };

                                ScanCount(dataDetail);
                            }
                        }

                        var CyclecountDatail = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == CycleCountOld.CycleCount_Index && c.Ref_Document_Index == TaskCycleCountOld.Task_Index && c.Qty_Diff != 0).ToList();


                        var Cyclecount = db.wm_CycleCount.Where(c => c.CycleCount_Index == CycleCountOld.CycleCount_Index).ToList();


                        if (CyclecountDatail.Count > 0 && CyclecountItem.Count > 0)
                        {
                            var ViewJoin = (from CYI in CyclecountItem
                                            join CY in Cyclecount on CYI.CycleCount_Index equals CY.CycleCount_Index

                                            select new View_AssignJobViewModel
                                            {
                                                cycleCount_Index = CY.CycleCount_Index,
                                                cycleCount_No = CY.CycleCount_No,
                                                cycleCountItem_Index = CYI.CycleCountItem_Index,
                                                cycleCount_Date = CY.CycleCount_Date,
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
                                this.CreateTask(item.CY, item.CYD, data.userAssign, "1");
                            }

                            #region Update count in CyclecountItem

                            foreach (var resultCyclecountItem in CyclecountItem)
                            {
                                var ItemOld = db.wm_CycleCountItem.Find(resultCyclecountItem.CycleCountItem_Index);

                                if (ItemOld != null)
                                {
                                    ItemOld.Count = ItemOld.Count + 1;
                                    ItemOld.Update_By = data.userAssign;
                                    ItemOld.Update_Date = DateTime.Now;
                                }

                                #endregion

                            }
                        }
                        else
                        {
                            CycleCountOld.Document_Status = 3;
                            CycleCountOld.Update_By = data.userAssign;
                            CycleCountOld.Update_Date = DateTime.Now;

                        }


                    }

                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    actionResult.Message = true;
                    db.SaveChanges();
                    transactionx.Commit();
                }
                catch (Exception exy)
                {
                    actionResult.Message = false;
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("ConfirmTask", msglog);
                    transactionx.Rollback();

                    throw exy;
                }

                return actionResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ItemStatusViewModel> dropdownItemStatus(ItemStatusViewModel data)
        {
            try
            {
                var result = new List<ItemStatusViewModel>();

                var filterModel = new ItemStatusDocViewModel();

                //GetConfig
                result = utils.SendDataApi<List<ItemStatusViewModel>>(new AppSettingConfig().GetUrl("dropdownItemStatus"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<productViewModel> product(productViewModel data)
        {
            try
            {

                var result = new List<productViewModel>();

                var resultProduct = new List<ProductViewModel>();
                var filterModelProduct = new ProductViewModel();

                filterModelProduct.product_Index = data.product_Index;
                resultProduct = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("GetProduct"), filterModelProduct.sJson());




                foreach (var item in resultProduct)
                {
                    var resultItem = new productViewModel();

                    resultItem.product_Index = item.product_Index;
                    resultItem.product_Id = item.product_Id;
                    resultItem.product_Name = item.product_Name;
                    resultItem.isExpDate = item.isExpDate;
                    resultItem.isMfgDate = item.isMfgDate;
                    resultItem.isLot = item.isLot;
                    result.Add(resultItem);

                }

                return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TaskGroupViewModel> dropdownTaskGroup(TaskGroupViewModel data)
        {
            try
            {
                var result = new List<TaskGroupViewModel>();

                var filterModel = new TaskGroupViewModel();


                //GetConfig
                result = utils.SendDataApi<List<TaskGroupViewModel>>(new AppSettingConfig().GetUrl("TaskGroup"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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



    }

}
