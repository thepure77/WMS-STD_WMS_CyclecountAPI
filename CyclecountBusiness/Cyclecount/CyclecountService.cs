using AspNetCore.Reporting;
using BinBalanceDataAccess.Models;
using Comone.Utils;
using CyclecountBusiness.Reports.PrintOutCycleCount;
using CyclecountBusiness.ViewModels;
using DataAccess;
using LogDataAccess.Models;
using MasterBusiness.PlanGoodsIssue;
using MasterDataAccess;
using MasterDataBusiness.AutoNumber;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using PlanGIBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using TransferBusiness;
using TransferBusiness.ConfigModel;
using TransferBusiness.GoodsTransfer.ViewModel;
using TransferBusiness.Library;
using TransferBusiness.Transfer;
using TransferDataAccess.Models;
using static CyclecountBusiness.Transfer.CycleCountViewModel;

namespace CyclecountBusiness.Transfer
{
    public class CyclecountService
    {
        private CyclecountDbContext db;
        private MasterDbContext dbmaster;

        public CyclecountService()
        {
            db = new CyclecountDbContext();
            dbmaster = new MasterDbContext();
        }

        public CyclecountService(CyclecountDbContext db, MasterDbContext dbmaster)
        {
            this.db = db;
            this.dbmaster = dbmaster;
        }


        public actionResult filter(CycleCountViewModel data)
        {
            try
            {

                var query = db.wm_CycleCount.AsQueryable();


                if (data.AdvanceSearch == false)
                {
                    if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.CycleCount_No == (data.key));
                    }

                    if (!string.IsNullOrEmpty(data.cycleCount_Date))
                    {
                        var cycleCount_Date = data.cycleCount_Date.toBetweenDate();

                        var dateStart = data.cycleCount_Date.toBetweenDate();
                        var dateEnd = data.cycleCount_Date_To.toBetweenDate();


                        query = query.Where(c => c.CycleCount_Date >= dateStart.start && c.CycleCount_Date <= dateEnd.end);
                    }

                    var statusModels = new List<int?>();
                    var sortModels = new List<SortModel>();
                    if (data?.status?.Count > 0)
                    {
                        foreach (var item in data.status)
                        {
                            if (item.value == 0)
                            {
                                statusModels.Add(0);
                            }
                            if (item.value == 2)
                            {
                                statusModels.Add(2);
                            }
                            if (item.value == 3)
                            {
                                statusModels.Add(3);
                            }
                            if (item.value == -1)
                            {
                                statusModels.Add(-1);
                            }
                        }
                        query = query.Where(c => statusModels.Contains(c.Document_Status));
                    }

                    if (data?.sort?.Count > 0)
                    {
                        foreach (var item in data.sort)
                        {

                            if (item.value == "cycleCount_No")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "CycleCount_No",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "cycleCount_Date")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "CycleCount_Date",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "documentType_Name")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "documentType_Name",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "document_Status")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Document_Status",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "Create_By")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Create_By",
                                    Sort = "desc"
                                });

                            }
                        }
                        query = query.KWOrderBy(sortModels);

                    }
                }

                else
                {
                    if (!string.IsNullOrEmpty(data.cycleCount_No))
                    {
                        query = query.Where(c => c.CycleCount_No == (data.cycleCount_No));
                    }

                    if (!string.IsNullOrEmpty(data.owner_Name))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(data.owner_Name));
                    }

                    if (!string.IsNullOrEmpty(data.document_Status.ToString()))
                    {
                        query = query.Where(c => c.Document_Status == (data.document_Status));
                    }

                    if (!string.IsNullOrEmpty(data.documentType_Index.ToString()) && data.documentType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.DocumentType_Index == (data.documentType_Index));
                    }

                    if (!string.IsNullOrEmpty(data.cycleCount_Date))
                    {
                        var cycleCount_Date = data.cycleCount_Date.toBetweenDate();

                        var dateStart = data.cycleCount_Date.toBetweenDate();
                        var dateEnd = data.cycleCount_Date_To.toBetweenDate();


                        query = query.Where(c => c.CycleCount_Date >= dateStart.start && c.CycleCount_Date <= dateEnd.end);
                    }

                }




                var Item = new List<wm_CycleCount>();



                var count = query.Count();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.ToList();

                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("ProcessStatus"), filterModel.sJson());

                String Statue = "";
                var result = new List<CycleCountViewModel>();

                if ((data?.sort?.Count ?? 0) == 0)
                {
                    Item = Item.OrderByDescending(o => o.CycleCount_No).OrderByDescending(o => o.CycleCount_Date).ToList();
                }


                foreach (var item in Item)
                {
                    var resultItem = new CycleCountViewModel();

                    resultItem.cycleCount_Index = item.CycleCount_Index;
                    resultItem.cycleCount_No = item.CycleCount_No;
                    resultItem.documentType_Index = item.DocumentType_Index;
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;
                    resultItem.document_Status = item.Document_Status;
                    resultItem.documentRef_No1 = item.DocumentRef_No1;
                    resultItem.cycleCount_Date = item.CycleCount_Date.toString();
                    Statue = item.Document_Status.ToString();
                    var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();
                    resultItem.processStatus_Name = ProcessStatusName.processStatus_Name;
                    resultItem.create_By = item.Create_By;
                    resultItem.update_By = item.Update_By;
                    resultItem.cancel_By = item.Cancel_By;

                    result.Add(resultItem);
                }


                var actionResult = new actionResult();
                actionResult.items = result.OrderByDescending(o => o.create_Date).ThenByDescending(o => o.create_Date).ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> DropdownDocumentType(DocumentTypeViewModel data)
        {
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var filterModel = new DocumentTypeViewModel();


                filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");

                //GetConfig
                result = utils.SendDataApi<List<DocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentTypeViewModel> documentTypefilter(DocumentTypeViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                    var result = new List<DocumentTypeViewModel>();

                    var query = dbmaster.MS_Location.GroupBy(o => o.Location_Aisle).Select(c=> new { Location_Aisle = c.Key }).ToList();
                                 
                   var queryResult = query.OrderBy(o => o.Location_Aisle).ToList();

                    foreach (var item in queryResult)
                    {
                        msglog = "by values";
                        var resultItem = new DocumentTypeViewModel();
                        
                        resultItem.location_Aisle = item.Location_Aisle;
                        //resultItem.location_Bay_Desc = item.Location_Bay_Desc;
                        //resultItem.location_Level_Desc = item.Location_Level_Desc;
                        //resultItem.location_Prefix_Desc = item.Location_Prefix_Desc;
                        
                        result.Add(resultItem);
                    }
                    return result;
                
            }
            catch (Exception ex)
            {
                olog.logging("documentTypefilter", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> Bayfilter(DocumentTypeViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var query = dbmaster.MS_Location.GroupBy(o => o.Location_Bay_Desc).Select(c => new { Location_Bay_Desc = c.Key }).ToList();

                var queryResult = query.OrderBy(o => o.Location_Bay_Desc).ToList();

                foreach (var item in queryResult)
                {
                    msglog = "by values";
                    var resultItem = new DocumentTypeViewModel();

                    //resultItem.location_Aisle = item.Location_Aisle;
                    resultItem.location_Bay_Desc = item.Location_Bay_Desc;
                    //resultItem.location_Level_Desc = item.Location_Level_Desc;
                    //resultItem.location_Prefix_Desc = item.Location_Prefix_Desc;

                    result.Add(resultItem);
                }
                return result;

            }
            catch (Exception ex)
            {
                olog.logging("documentTypefilter", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> Levelfilter(DocumentTypeViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var query = dbmaster.MS_Location.GroupBy(o => o.Location_Level_Desc).Select(c => new { Location_Level_Desc = c.Key }).ToList();

                var queryResult = query.OrderBy(o => o.Location_Level_Desc).ToList();

                foreach (var item in queryResult)
                {
                    msglog = "by values";
                    var resultItem = new DocumentTypeViewModel();

                    //resultItem.location_Aisle = item.Location_Aisle;
                    //resultItem.location_Bay_Desc = item.Location_Bay_Desc;
                    resultItem.location_Level_Desc = item.Location_Level_Desc;
                    //resultItem.location_Prefix_Desc = item.Location_Prefix_Desc;

                    result.Add(resultItem);
                }
                return result;

            }
            catch (Exception ex)
            {
                olog.logging("documentTypefilter", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> Prefixfilter(DocumentTypeViewModel data)
        {
            var olog = new logtxt();
            String msglog = "";
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var query = dbmaster.MS_Location.GroupBy(o => o.Location_Prefix_Desc).Select(c => new { Location_Prefix_Desc = c.Key }).ToList();

                var queryResult = query.OrderBy(o => o.Location_Prefix_Desc).ToList();

                foreach (var item in queryResult)
                {
                    msglog = "by values";
                    var resultItem = new DocumentTypeViewModel();

                    //resultItem.location_Aisle = item.Location_Aisle;
                    //resultItem.location_Bay_Desc = item.Location_Bay_Desc;
                    //resultItem.location_Level_Desc = item.Location_Level_Desc;
                    resultItem.location_Prefix_Desc = item.Location_Prefix_Desc;

                    result.Add(resultItem);
                }
                return result;

            }
            catch (Exception ex)
            {
                olog.logging("documentTypefilter", msglog + " ex Rollback " + ex.Message.ToString());
                throw ex;
            }
        }



        public List<ProcessStatusViewModel> DropdownProcess(ProcessStatusViewModel data)
        {
            try
            {
                var result = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();


                filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");

                //GetConfig
                result = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("ProcessStatus"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> AutoZone(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoZoneFilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoLocationTypFilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                var tpyeStaging = new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3");
                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoLocationTypFilter"), filterModel.sJson()).ToList();//.Where(c => c.index != tpyeStaging).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoLocationFilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                var tpyeStaging = new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3");
                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoLocationFilter"), filterModel.sJson()).Where(c => c.value5 != tpyeStaging).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemListViewModel> autoProductFilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoProductFilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<BinBalanceLocationViewModel> BinSearch(BinBalanceLocationViewModel data)
        {
            try
            {

                var result = new List<BinBalanceLocationViewModel>();

                var resultView = new List<View_LocatinCyclecountViewModel>();

                var queryView = new List<View_LocatinCyclecountViewModel>();

                var resultProduct = new List<ProductViewModel>();


                var filterModel = new View_LocatinCyclecountViewModel();

                if (data.listZoneViewModel.Count > 0)
                {
                    foreach (var item in data.listZoneViewModel)
                    {
                        var listZone = new List<listZoneViewModel> { new listZoneViewModel { zone_Index = item.zone_Index } };
                        filterModel.listZoneViewModel = listZone;
                    }
                }

                if (!string.IsNullOrEmpty(data.locationType_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    filterModel.locationType_Index = data.locationType_Index;
                }

                if (!string.IsNullOrEmpty(data.location_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    filterModel.location_Index = data.location_Index;
                }

                resultView = utils.SendDataApi<List<View_LocatinCyclecountViewModel>>(new AppSettingConfig().GetUrl("ConfigViewCyclecount"), filterModel.sJson());

                if (data.isStaging)
                {
                    resultView = resultView.Where(c => !(new List<Guid?> { new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"), new Guid("2E9338D3-0931-4E36-B240-782BF9508641"), new Guid("65A2D25D-5520-47D3-8776-AE064D909285") }.Contains(c.locationType_Index))).ToList();
                }

                queryView = resultView.ToList();

                if (!string.IsNullOrEmpty(data.movement))
                {
                    if (data.movement == "30")
                    {
                        var queryMovemet = db.View_Movement30.ToList();
                        //if (!string.IsNullOrEmpty(data.location_Aisle))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Aisle == data.location_Aisle).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Bay_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Bay_Desc == data.location_Bay_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Level_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Level_Desc == data.location_Level_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Prefix_Desc == data.location_Prefix_Desc).ToList();
                        //}

                        queryView = resultView.Where(c => queryMovemet.Select(s => s.Location_Index).Contains(c.location_Index)).ToList();

                       
                    }
                    if (data.movement == "60")
                    {
                        var queryMovemet = db.View_Movement60.ToList();
                        //if (!string.IsNullOrEmpty(data.location_Aisle))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Aisle == data.location_Aisle).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Bay_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Bay_Desc == data.location_Bay_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Level_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Level_Desc == data.location_Level_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Prefix_Desc == data.location_Prefix_Desc).ToList();
                        //}
                        queryView = resultView.Where(c => queryMovemet.Select(s => s.Location_Index).Contains(c.location_Index)).ToList();

                        

                    }
                    if (data.movement == "90")
                    {
                        var queryMovemet = db.View_Movement90.ToList();
                        //if (!string.IsNullOrEmpty(data.location_Aisle))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Aisle == data.location_Aisle).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Bay_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Bay_Desc == data.location_Bay_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Level_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Level_Desc == data.location_Level_Desc).ToList();
                        //}
                        //if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                        //{
                        //    queryMovemet = queryMovemet.Where(c => c.Location_Prefix_Desc == data.location_Prefix_Desc).ToList();
                        //}
                        queryView = resultView.Where(c => queryMovemet.Select(s => s.Location_Index).Contains(c.location_Index)).ToList();

                    }
                    
                }

                var queryLO = dbmaster.MS_Location.AsQueryable();
                bool Aisle = false;
                bool Bay = false;
                bool Level = false;
                bool Prefix = false;
                if (!string.IsNullOrEmpty(data.location_Aisle))
                {
                    queryLO = queryLO.Where(c => c.Location_Aisle == data.location_Aisle);
                    Aisle = true;
                }
                if (!string.IsNullOrEmpty(data.location_Bay_Desc))
                {
                    queryLO = queryLO.Where(c => c.Location_Bay_Desc == data.location_Bay_Desc);
                    Bay = true;
                }
                if (!string.IsNullOrEmpty(data.location_Level_Desc))
                {
                    queryLO = queryLO.Where(c => c.Location_Level_Desc == data.location_Level_Desc);
                    Level = true;
                }
                if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                {
                    queryLO = queryLO.Where(c => c.Location_Prefix_Desc == data.location_Prefix_Desc);
                    Prefix = true;
                }

                var select_location = queryLO.Select(c => c.Location_Index).ToList();

                if (Aisle || Bay || Level || Prefix)
                {
                    queryView = queryView.Where(c => select_location.Contains(c.location_Index)).ToList();
                }

                if (data.isSku == false)
                {
                    var query = queryView.GroupBy(c => new
                    {
                        c.location_Index,
                        c.location_Id,
                        c.location_Name,
                        c.locationType_Index,
                        c.locationType_Id,
                        c.locationType_Name,
                        c.zone_Index,
                        c.zone_Id,
                        c.zone_Name
                        
                })
                    .Select(c => new
                    {
                        c.Key.location_Index,
                        c.Key.location_Id,
                        c.Key.location_Name,
                        c.Key.locationType_Index,
                        c.Key.locationType_Id,
                        c.Key.locationType_Name,
                        c.Key.zone_Index,
                        c.Key.zone_Id,
                        c.Key.zone_Name
                    }).ToList();

                    foreach (var item in query)
                    {
                        var resultItem = new BinBalanceLocationViewModel();

                        var where = queryView.Where(c => c.location_Index == item.location_Index).FirstOrDefault();

                        resultItem.location_Index = where.location_Index;
                        resultItem.location_Id = where.location_Id;
                        resultItem.location_Name = where.location_Name;

                        resultItem.locationType_Index = where.locationType_Index;
                        resultItem.locationType_Id = where.locationType_Id;
                        resultItem.locationType_Name = where.locationType_Name;

                        resultItem.zone_Index = where.zone_Index;
                        resultItem.zone_Id = where.zone_Id;
                        resultItem.zone_Name = where.zone_Id;


                        resultItem.warehouse_Index = where.warehouse_Index;
                        resultItem.warehouse_Id = where.warehouse_Id;
                        resultItem.warehouse_Name = where.warehouse_Name;


                        result.Add(resultItem);
                    }

                }


                else
                {
                    #region Don't Have List Product

                    if (data.listProductViewModel.Count <= 0)
                    {

                        #region Search โดยมีเงื่อนไข Owner
                        if (!string.IsNullOrEmpty(data.owner_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                        {
                            var queryResult = db.wm_BinBalance.Where(c => queryView.Select(s => s.location_Index).Contains(c.Location_Index) && c.Owner_Index == data.owner_Index)
                               .GroupBy(c => new
                               {
                                   c.Location_Index,
                                   c.Location_Id,
                                   c.Location_Name,
                                   c.Product_Index,
                                   c.Product_Id,
                                   c.Product_Name,
                                   c.Owner_Index,
                                   c.Owner_Id,
                                   c.Owner_Name
                               })
                               .Select(c => new
                               {
                                   c.Key.Location_Index,
                                   c.Key.Location_Id,
                                   c.Key.Location_Name,
                                   c.Key.Product_Index,
                                   c.Key.Product_Id,
                                   c.Key.Product_Name,
                                   c.Key.Owner_Index,
                                   c.Key.Owner_Id,
                                   c.Key.Owner_Name
                               }).ToList();


                            foreach (var item in queryResult)
                            {
                                var resultItem = new BinBalanceLocationViewModel();

                                var where = queryView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();

                                resultItem.location_Index = where.location_Index;
                                resultItem.location_Id = where.location_Id;
                                resultItem.location_Name = where.location_Name;

                                resultItem.locationType_Index = where.locationType_Index;
                                resultItem.locationType_Id = where.locationType_Id;
                                resultItem.locationType_Name = where.locationType_Name;

                                resultItem.zone_Index = where.zone_Index;
                                resultItem.zone_Id = where.zone_Id;
                                resultItem.zone_Name = where.zone_Id;
                                
                                resultItem.product_Index = item.Product_Index;
                                resultItem.product_Id = item.Product_Id;
                                resultItem.product_Name = item.Product_Name;

                                resultItem.warehouse_Index = where.warehouse_Index;
                                resultItem.warehouse_Id = where.warehouse_Id;
                                resultItem.warehouse_Name = where.warehouse_Name;

                                resultItem.owner_Index = item.Owner_Index;
                                resultItem.owner_Id = item.Owner_Id;
                                resultItem.owner_Name = item.Owner_Name;

                                

                                result.Add(resultItem);
                            }

                        }
                        #endregion

                        #region Search โดยไม่มีเงื่อนไข Owner
                        else
                        {
                            var queryResult = db.wm_BinBalance.Where(c => queryView.Select(s => s.location_Index).Contains(c.Location_Index))
                               .GroupBy(c => new
                               {
                                   c.Location_Index,
                                   c.Location_Id,
                                   c.Location_Name,
                                   c.Product_Index,
                                   c.Product_Id,
                                   c.Product_Name
                               })
                               .Select(c => new
                               {
                                   c.Key.Location_Index,
                                   c.Key.Location_Id,
                                   c.Key.Location_Name,
                                   c.Key.Product_Index,
                                   c.Key.Product_Id,
                                   c.Key.Product_Name
                               }).ToList();


                            foreach (var item in queryResult)
                            {
                                var resultItem = new BinBalanceLocationViewModel();

                                var where = queryView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();

                                resultItem.location_Index = where.location_Index;
                                resultItem.location_Id = where.location_Id;
                                resultItem.location_Name = where.location_Name;

                                resultItem.locationType_Index = where.locationType_Index;
                                resultItem.locationType_Id = where.locationType_Id;
                                resultItem.locationType_Name = where.locationType_Name;

                                resultItem.zone_Index = where.zone_Index;
                                resultItem.zone_Id = where.zone_Id;
                                resultItem.zone_Name = where.zone_Id;

                                resultItem.product_Index = item.Product_Index;
                                resultItem.product_Id = item.Product_Id;
                                resultItem.product_Name = item.Product_Name;

                                resultItem.warehouse_Index = where.warehouse_Index;
                                resultItem.warehouse_Id = where.warehouse_Id;
                                resultItem.warehouse_Name = where.warehouse_Name;

                                result.Add(resultItem);
                            }
                        }

                        #endregion

                    }

                    #endregion


                    #region Have List Product   

                    else
                    {
                        #region Search โดยมีเงื่อนไข Owner

                        if (!string.IsNullOrEmpty(data.owner_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                        {
                            var query = db.wm_BinBalance.Where(c => queryView.Select(s => s.location_Index).Contains(c.Location_Index)
                              && data.listProductViewModel.Select(s => s.product_Index).Contains(c.Product_Index) && c.Owner_Index == data.owner_Index)
                          .GroupBy(c => new
                          {
                              c.Location_Index,
                              c.Location_Id,
                              c.Location_Name,
                              c.Product_Index,
                              c.Product_Id,
                              c.Product_Name,
                              c.Owner_Index,
                              c.Owner_Id,
                              c.Owner_Name
                          })
                          .Select(c => new
                          {
                              c.Key.Location_Index,
                              c.Key.Location_Id,
                              c.Key.Location_Name,
                              c.Key.Product_Index,
                              c.Key.Product_Id,
                              c.Key.Product_Name,
                              c.Key.Owner_Index,
                              c.Key.Owner_Id,
                              c.Key.Owner_Name
                          }).ToList();


                            foreach (var item in query)
                            {
                                var resultItem = new BinBalanceLocationViewModel();

                                var where = queryView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();

                                resultItem.location_Index = where.location_Index;
                                resultItem.location_Id = where.location_Id;
                                resultItem.location_Name = where.location_Name;

                                resultItem.locationType_Index = where.locationType_Index;
                                resultItem.locationType_Id = where.locationType_Id;
                                resultItem.locationType_Name = where.locationType_Name;

                                resultItem.zone_Index = where.zone_Index;
                                resultItem.zone_Id = where.zone_Id;
                                resultItem.zone_Name = where.zone_Id;

                                resultItem.product_Index = item.Product_Index;
                                resultItem.product_Id = item.Product_Id;
                                resultItem.product_Name = item.Product_Name;

                                resultItem.warehouse_Index = where.warehouse_Index;
                                resultItem.warehouse_Id = where.warehouse_Id;
                                resultItem.warehouse_Name = where.warehouse_Name;

                                resultItem.owner_Index = item.Owner_Index;
                                resultItem.owner_Id = item.Owner_Id;
                                resultItem.owner_Name = item.Owner_Name;

                                result.Add(resultItem);
                            }
                        }

                        #endregion

                        #region Search โดยwไม่มีมีเงื่อนไข Owner

                        else
                        {
                            var query = db.wm_BinBalance.Where(c => queryView.Select(s => s.location_Index).Contains(c.Location_Index)
                              && data.listProductViewModel.Select(s => s.product_Index).Contains(c.Product_Index))
                          .GroupBy(c => new
                          {
                              c.Location_Index,
                              c.Location_Id,
                              c.Location_Name,
                              c.Product_Index,
                              c.Product_Id,
                              c.Product_Name,
                          })
                          .Select(c => new
                          {
                              c.Key.Location_Index,
                              c.Key.Location_Id,
                              c.Key.Location_Name,
                              c.Key.Product_Index,
                              c.Key.Product_Id,
                              c.Key.Product_Name,
                          }).ToList();


                            foreach (var item in query)
                            {
                                var resultItem = new BinBalanceLocationViewModel();

                                var where = queryView.Where(c => c.location_Index == item.Location_Index).FirstOrDefault();

                                resultItem.location_Index = where.location_Index;
                                resultItem.location_Id = where.location_Id;
                                resultItem.location_Name = where.location_Name;

                                resultItem.locationType_Index = where.locationType_Index;
                                resultItem.locationType_Id = where.locationType_Id;
                                resultItem.locationType_Name = where.locationType_Name;

                                resultItem.zone_Index = where.zone_Index;
                                resultItem.zone_Id = where.zone_Id;
                                resultItem.zone_Name = where.zone_Id;

                                resultItem.product_Index = item.Product_Index;
                                resultItem.product_Id = item.Product_Id;
                                resultItem.product_Name = item.Product_Name;

                                resultItem.warehouse_Index = where.warehouse_Index;
                                resultItem.warehouse_Id = where.warehouse_Id;
                                resultItem.warehouse_Name = where.warehouse_Name;

                                result.Add(resultItem);
                            }
                        }

                        #endregion

                    }

                    #endregion


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

            var actionResult = new actionResult();

            try
            {

                var itemDetail = new List<wm_CycleCountItem>();


                var CycleCountOld = db.wm_CycleCount.Find(data.cycleCount_Index);

                if (CycleCountOld == null)
                {

                    CycleCountIndex = Guid.NewGuid();

                    var result = new List<GenDocumentTypeViewModel>();

                    var filterModel = new GenDocumentTypeViewModel();


                    filterModel.process_Index = new Guid("0794D9F7-4D44-4904-A3EB-89622F748665");
                    filterModel.documentType_Index = data.documentType_Index;
                    //GetConfig
                    result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                    var genDoc = new AutoNumberService();
                    string DocNo = "";
                    DateTime DocumentDate = (DateTime)data.cycleCount_Date.toDate();
                    DocNo = genDoc.genAutoDocmentNumber(result, DocumentDate);
                    CycleCountNo = DocNo;

                    wm_CycleCount itemHeader = new wm_CycleCount();

                    itemHeader.CycleCount_Index = CycleCountIndex;
                    itemHeader.CycleCount_No = CycleCountNo;
                    itemHeader.Owner_Index = data.owner_Index;
                    itemHeader.Owner_Id = data.owner_Id;
                    itemHeader.Owner_Name = data.owner_Name;
                    itemHeader.Warehouse_Index = data.listBinLocation.FirstOrDefault().warehouse_Index;
                    itemHeader.Warehouse_Id = data.listBinLocation.FirstOrDefault().warehouse_Id;
                    itemHeader.Warehouse_Name = data.listBinLocation.FirstOrDefault().warehouse_Name;
                    itemHeader.DocumentType_Index = data.documentType_Index;
                    itemHeader.DocumentType_Id = data.documentType_Id;
                    itemHeader.DocumentType_Name = data.documentType_Name;
                    itemHeader.CycleCount_Date = data.cycleCount_Date.toDate();
                    itemHeader.DocumentRef_No1 = data.documentRef_No1;
                    itemHeader.DocumentRef_No2 = data.documentRef_No2;
                    itemHeader.DocumentRef_No3 = data.documentRef_No3;
                    itemHeader.DocumentRef_No4 = data.documentRef_No4;
                    itemHeader.DocumentRef_No5 = data.documentRef_No5;

                    itemHeader.Document_Status = 0;
                    itemHeader.Create_By = User;
                    itemHeader.Create_Date = DateTime.Now;

                    db.wm_CycleCount.Add(itemHeader);


                    foreach (var item in data.listBinLocation)
                    {
                        wm_CycleCountItem resultItem = new wm_CycleCountItem();

                        resultItem.CycleCountItem_Index = Guid.NewGuid();
                        resultItem.CycleCount_Index = CycleCountIndex;
                        resultItem.CycleCount_No = CycleCountNo;
                        resultItem.Location_Index = item.location_Index;
                        resultItem.Location_Id = item.location_Id;
                        resultItem.Location_Name = item.location_Name;
                        resultItem.Zone_Index = item.zone_Index;
                        resultItem.Zone_Id = item.zone_Id;
                        resultItem.Zone_Name = item.zone_Name;
                        resultItem.Count = 1;
                        if (data.isSku == true)
                        {
                            resultItem.Product_Index = item.product_Index;
                            resultItem.Product_Id = item.product_Id;
                            resultItem.Product_Name = item.product_Name;
                            resultItem.Product_SecondName = item.product_SecondName;
                            resultItem.Product_ThirdName = item.product_ThirdName;
                            resultItem.Product_Lot = item.product_Lot;
                        }

                        resultItem.Document_Status = 0;
                        resultItem.Create_By = User;
                        resultItem.Create_Date = DateTime.Now;

                        itemDetail.Add(resultItem);
                        db.wm_CycleCountItem.Add(resultItem);

                    }

                }

                else
                {
                    CycleCountNo = CycleCountOld.CycleCount_No;
                    CycleCountOld.CycleCount_Date = data.cycleCount_Date.toDate();
                    CycleCountOld.DocumentType_Index = data.documentType_Index;
                    CycleCountOld.DocumentType_Id = data.documentType_Id;
                    CycleCountOld.DocumentType_Name = data.documentType_Name;
                    CycleCountOld.DocumentRef_No1 = data.documentRef_No1;
                    CycleCountOld.DocumentRef_No2 = data.documentRef_No2;
                    CycleCountOld.DocumentRef_No3 = data.documentRef_No3;
                    CycleCountOld.DocumentRef_No4 = data.documentRef_No4;
                    CycleCountOld.DocumentRef_No5 = data.documentRef_No5;
                    CycleCountOld.Update_By = User;
                    CycleCountOld.Update_Date = DateTime.Now;

                    foreach (var item in data.listBinLocation)
                    {
                        var CycleCountItemOld = db.wm_CycleCountItem.Find(item.cycleCountItem_Index);

                        if (CycleCountItemOld == null)
                        {
                            wm_CycleCountItem resultItem = new wm_CycleCountItem();

                            resultItem.CycleCountItem_Index = Guid.NewGuid();
                            resultItem.CycleCount_Index = data.cycleCount_Index.GetValueOrDefault();
                            resultItem.CycleCount_No = CycleCountNo;
                            resultItem.Location_Index = item.location_Index;
                            resultItem.Location_Id = item.location_Id;
                            resultItem.Location_Name = item.location_Name;
                            resultItem.Zone_Index = item.zone_Index;
                            resultItem.Zone_Id = item.zone_Id;
                            resultItem.Zone_Name = item.zone_Name;
                            resultItem.Count = 1;
                            if (data.isSku == true)
                            {
                                resultItem.Product_Index = item.product_Index;
                                resultItem.Product_Id = item.product_Id;
                                resultItem.Product_Name = item.product_Name;
                                resultItem.Product_SecondName = item.product_SecondName;
                                resultItem.Product_ThirdName = item.product_ThirdName;
                                resultItem.Product_Lot = item.product_Lot;
                            }

                            resultItem.Document_Status = 0;
                            resultItem.Update_By = User;
                            resultItem.Update_Date = DateTime.Now;

                            itemDetail.Add(resultItem);
                            db.wm_CycleCountItem.Add(resultItem);
                        }

                        var deleteItem = db.wm_CycleCountItem.Where(c => !data.listBinLocation.Select(s => s.cycleCountItem_Index).Contains(c.CycleCountItem_Index)
                            && c.CycleCount_Index == CycleCountOld.CycleCount_Index).ToList();

                        foreach (var c in deleteItem)
                        {
                            var delete = db.wm_CycleCountItem.Find(c.CycleCountItem_Index);

                            delete.Document_Status = -1;
                            delete.Update_By = data.update_By;
                            delete.Update_Date = DateTime.Now;

                        }

                    }

                }
                this.GetTask(itemDetail);


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

        public actionResult find(Guid id)
        {
            try
            {
                var actionResult = new actionResult();

                #region find CycleCount


                var queryResult = db.wm_CycleCount.Where(c => c.CycleCount_Index == id).FirstOrDefault();

                var result = new CycleCountViewModel();

                result.cycleCount_Index = queryResult.CycleCount_Index;
                result.cycleCount_No = queryResult.CycleCount_No;
                result.cycleCount_Date = queryResult.CycleCount_Date.toString();
                result.owner_Index = queryResult.Owner_Index;
                result.owner_Id = queryResult.Owner_Id;
                result.owner_Name = queryResult.Owner_Name;
                result.documentType_Index = queryResult.DocumentType_Index;
                result.documentType_Id = queryResult.DocumentType_Id;
                result.documentType_Name = queryResult.DocumentType_Name;
                result.documentRef_No1 = queryResult.DocumentRef_No1;
                result.documentRef_No2 = queryResult.DocumentRef_No2;
                result.documentRef_No3 = queryResult.DocumentRef_No3;
                result.documentRef_No4 = queryResult.DocumentRef_No4;
                result.documentRef_No5 = queryResult.DocumentRef_No5;
                result.document_Status = queryResult.Document_Status;

                #endregion

                #region find CycleCountItem


                var queryResultItem = db.wm_CycleCountItem.Where(c => c.CycleCount_Index == id && c.Document_Status != -1).ToList();

                var resultItem = new List<CycleCountItemViewModel>();


                foreach (var dataItem in queryResultItem)
                {
                    var item = new CycleCountItemViewModel();

                    item.cycleCount_Index = dataItem.CycleCount_Index;
                    item.cycleCountItem_Index = dataItem.CycleCountItem_Index;
                    item.location_Index = dataItem.Location_Index;
                    item.location_Id = dataItem.Location_Id;
                    item.location_Name = dataItem.Location_Name;
                    item.zone_Index = dataItem.Zone_Index;
                    item.zone_Id = dataItem.Zone_Id;
                    item.zone_Name = dataItem.Zone_Name;
                    item.product_Index = dataItem.Product_Index;
                    item.product_Id = dataItem.Product_Id;
                    item.product_Name = dataItem.Product_Name;
                    item.product_SecondName = dataItem.Product_SecondName;
                    item.product_ThirdName = dataItem.Product_ThirdName;


                    resultItem.Add(item);
                }

                #endregion

                #region find CycleCountDetail

                var queryResultDetail = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == id).ToList();


                var resultDetail = new List<CycleCountDetailViewModel>();

                if (queryResultDetail.Count > 0)
                {
                    foreach (var dataDetail in queryResultDetail.OrderBy(o => o.Counting).OrderBy(o => o.Product_Id))
                    {
                        var detail = new CycleCountDetailViewModel();

                        detail.cycleCount_Index = dataDetail.CycleCount_Index;
                        detail.cycleCountItem_Index = dataDetail.CycleCountItem_Index;
                        detail.cycleCountDetail_Index = dataDetail.CycleCountDetail_Index;
                        detail.cycleCount_No = dataDetail.CycleCount_No;
                        detail.location_Index = dataDetail.Location_Index;
                        detail.location_Id = dataDetail.Location_Id;
                        detail.location_Name = dataDetail.Location_Name;
                        detail.zone_Index = dataDetail.Zone_Index;
                        detail.zone_Id = dataDetail.Zone_Id;
                        detail.zone_Name = dataDetail.Zone_Name;
                        detail.product_Index = dataDetail.Product_Index;
                        detail.product_Id = dataDetail.Product_Id;
                        detail.product_Name = dataDetail.Product_Name;
                        detail.product_SecondName = dataDetail.Product_SecondName;
                        detail.product_ThirdName = dataDetail.Product_ThirdName;
                        detail.qty_Bal = dataDetail.Qty_Bal;
                        detail.qty_Count = dataDetail.Qty_Count;
                        detail.qty_Diff = dataDetail.Qty_Diff;
                        detail.create_By = dataDetail.Create_By;
                        detail.count = dataDetail.Counting;

                        resultDetail.Add(detail);
                    }
                }


                #endregion

                actionResult.listHeader = result;
                actionResult.listItem = resultItem.ToList();
                actionResult.listDetail = resultDetail.ToList();


                return actionResult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Delete
        public Boolean Delete(CycleCountViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {


                var CycleCount = db.wm_CycleCount.Find(data.cycleCount_Index);

                if (CycleCount != null)
                {
                    CycleCount.Document_Status = -1;
                    CycleCount.Cancel_By = data.cancel_By;
                    CycleCount.Cancel_Date = DateTime.Now;

                    var task = db.wm_TaskCycleCountItem.Where(c => c.Ref_Document_Index == CycleCount.CycleCount_Index).FirstOrDefault();
                    if (task != null)
                    {
                        var TaskOld = db.wm_TaskCycleCount.Find(task.Task_Index);

                        if (TaskOld != null)
                        {
                            TaskOld.Document_Status = -1;
                            TaskOld.Cancel_By = data.cancel_By;
                            TaskOld.Cancel_Date = DateTime.Now;
                        }
                    }


                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteCycleCount", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }

                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



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

        #region PrintOutCycleCount
        public string PrintOutCycleCount(CycleCountViewModel model, string rootPath = "")
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            Guid? GRItemIndex = new Guid();

            try
            {
                var location = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocation"), new { }.sJson());
                var result = new List<PrintOutCycleCountViewModel>();
                //var data = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == model.cycleCount_Index && c.Document_Status != -1).ToList();
                var data = (from CCI in db.wm_CycleCountItem
                            join CCD in db.wm_CycleCountDetail on CCI.CycleCountItem_Index equals CCD.CycleCountItem_Index into JCC
                            from CC in JCC.DefaultIfEmpty()
                            where CCI.CycleCount_Index == model.cycleCount_Index
                            && CCI.Document_Status != -1 && CCI.CycleCount_Status != -1
                            && (CC.Document_Status ?? 0) != -1
                            select new PrintOutCycleCountViewModel
                            {
                                cyclecount_No = model.cycleCount_No,
                                cyclecount_Date = !string.IsNullOrEmpty(model.cycleCount_Date) ? model.cycleCount_Date.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "",
                                location_Index = CCI.Location_Index,
                                location_Id = CCI.Location_Id,
                                location_Name = CCI.Location_Name.Trim(),
                                product_Id = (!string.IsNullOrEmpty(CC.Product_Id) ? CC.Product_Id : CCI.Product_Id),
                                product_Name = (!string.IsNullOrEmpty(CC.Product_Name) ? CC.Product_Name : CCI.Product_Name),
                                qty_Bal = CC.Qty_Bal,
                                qty_Count = CC.Qty_Count,
                                qty_Diff = CC.Qty_Diff,
                                barcode = new NetBarcode.Barcode(model.cycleCount_No, NetBarcode.Type.Code128B).GetBase64Image(),
                                unit = (!string.IsNullOrEmpty(CCI.Product_Id) || !string.IsNullOrEmpty(CC.Product_Id) ? "EA" : ""),
                                counting = CC.Counting
                            }).ToList();

                if (data.Count() == 0)
                {
                    return "";
                }

                var Group = data.Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = null,
                    qty_Count1 = null,
                    qty_Count2 = null,
                    qty_Count3 = null,
                    qty_Diff = null,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });

                var Group1 = data.Where(c => c.counting == 1).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = s.qty_Count,
                    qty_Count2 = null,
                    qty_Count3 = null,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });
                var Group2 = data.Where(c => c.counting == 2).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = null,
                    qty_Count2 = s.qty_Count,
                    qty_Count3 = null,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });
                var Group3 = data.Where(c => c.counting == 3).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = null,
                    qty_Count2 = null,
                    qty_Count3 = s.qty_Count,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });



                var uniondata = Group.Union(Group1).Union(Group2).Union(Group3).GroupBy(g => new
                {
                    g.cyclecount_No,
                    g.cyclecount_Date,
                    g.location_Index,
                    g.location_Id,
                    g.location_Name,
                    g.product_Id,
                    g.product_Name,
                    g.barcode,
                    g.unit,
                }).Join(location,
                             group => group.Key.location_Index,
                             Lo => Lo.location_Index,
                             (group, Lo) => new PrintOutCycleCountViewModel
                             {
                                 cyclecount_No = group.Key.cyclecount_No,
                                 cyclecount_Date = group.Key.cyclecount_Date,
                                 location_Name = group.Key.location_Name,
                                 product_Id = group.Key.product_Id,
                                 product_Name = group.Key.product_Name,
                                 qty_Bal = group.FirstOrDefault(gw => gw?.qty_Bal != null && gw?.counting == group.Max(m => m.counting))?.qty_Bal,
                                 qty_Count1 = Group1.Count() > 0 ? group.Sum(gs => gs?.qty_Count1) : null,
                                 qty_Count2 = Group2.Count() > 0 ? group.Sum(gs => gs?.qty_Count2) : null,
                                 qty_Count3 = Group3.Count() > 0 ? group.Sum(gs => gs?.qty_Count3) : null,
                                 qty_Diff = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == group.Max(m => m.counting))?.qty_Diff,
                                 barcode = group.Key.barcode,
                                 unit = group.Key.unit,
                                 location_Id = group.Key.location_Id,
                                 location_Aisle = Lo.location_Aisle,
                                 location_Prefix_desc = Lo.location_Prefix_desc,
                                 status = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == group.Max(m => m.counting))?.qty_Diff == 0 ? "Complete" : "Diff"
                             }
                             ).OrderBy(o => o.location_Aisle).ThenByDescending(td => td.location_Prefix_desc).ThenBy(t => t.location_Id).ToList();
                //.Select(s => new PrintOutCycleCountViewModel
                //{
                //    cyclecount_No = s.Key.cyclecount_No,
                //    cyclecount_Date = s.Key.cyclecount_Date,
                //    location_Name = s.Key.location_Name,
                //    product_Id = s.Key.product_Id,
                //    product_Name = s.Key.product_Name,
                //    qty_Bal = s.FirstOrDefault(gw => gw?.qty_Bal != null && gw?.counting == s.Max(m => m.counting))?.qty_Bal,
                //    qty_Count1 = Group1.Count() > 0 ? s.Sum(gs => gs?.qty_Count1) : null,
                //    qty_Count2 = Group2.Count() > 0 ? s.Sum(gs => gs?.qty_Count2) : null,
                //    qty_Count3 = Group3.Count() > 0 ? s.Sum(gs => gs?.qty_Count3) : null,
                //    qty_Diff = s.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == s.Max(m => m.counting))?.qty_Diff,
                //    barcode = s.Key.barcode,
                //    unit = s.Key.unit
                //}).ToList();


                rootPath = rootPath.Replace("\\CyclecountAPI", "");
                //var reportPath = rootPath + "\\CyclecountBusiness\\Reports\\PrintOutCycleCount\\PrintOutCycleCount.rdlc";
                var reportPath = rootPath + "\\Reports\\PrintOutCycleCount\\PrintOutCycleCount.rdlc";
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", uniondata);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region PrintOutCycleCount_excel
        public string PrintOutCycleCount_excel(CycleCountViewModel model, string rootPath = "")
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            Guid? GRItemIndex = new Guid();

            try
            {
                var location = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocation"), new { }.sJson());
                var result = new List<PrintOutCycleCountViewModel>();
                //var data = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == model.cycleCount_Index && c.Document_Status != -1).ToList();
                var data = (from CCI in db.wm_CycleCountItem
                            join CCD in db.wm_CycleCountDetail on CCI.CycleCountItem_Index equals CCD.CycleCountItem_Index into JCC
                            from CC in JCC.DefaultIfEmpty()
                            where CCI.CycleCount_Index == model.cycleCount_Index
                            && CCI.Document_Status != -1 && CCI.CycleCount_Status != -1
                            && (CC.Document_Status ?? 0) != -1
                            select new PrintOutCycleCountViewModel
                            {
                                cyclecount_No = model.cycleCount_No,
                                cyclecount_Date = !string.IsNullOrEmpty(model.cycleCount_Date) ? model.cycleCount_Date.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "",
                                location_Index = CCI.Location_Index,
                                location_Id = CCI.Location_Id,
                                location_Name = CCI.Location_Name.Trim(),
                                product_Id = (!string.IsNullOrEmpty(CC.Product_Id) ? CC.Product_Id : CCI.Product_Id),
                                product_Name = (!string.IsNullOrEmpty(CC.Product_Name) ? CC.Product_Name : CCI.Product_Name),
                                qty_Bal = CC.Qty_Bal,
                                qty_Count = CC.Qty_Count,
                                qty_Diff = CC.Qty_Diff,
                                barcode = new NetBarcode.Barcode(model.cycleCount_No, NetBarcode.Type.Code128B).GetBase64Image(),
                                unit = (!string.IsNullOrEmpty(CCI.Product_Id) || !string.IsNullOrEmpty(CC.Product_Id) ? "EA" : ""),
                                counting = CC.Counting
                            }).ToList();

                if (data.Count() == 0)
                {
                    return "";
                }

                var Group = data.Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = null,
                    qty_Count1 = null,
                    qty_Count2 = null,
                    qty_Count3 = null,
                    qty_Diff = null,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });

                var Group1 = data.Where(c => c.counting == 1).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = s.qty_Count,
                    qty_Count2 = null,
                    qty_Count3 = null,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });
                var Group2 = data.Where(c => c.counting == 2).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = null,
                    qty_Count2 = s.qty_Count,
                    qty_Count3 = null,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });
                var Group3 = data.Where(c => c.counting == 3).Select(s => new PrintOutCycleCountViewModel
                {
                    cyclecount_No = s.cyclecount_No,
                    cyclecount_Date = s.cyclecount_Date,
                    location_Index = s.location_Index,
                    location_Id = s.location_Id,
                    location_Name = s.location_Name,
                    product_Id = s.product_Id,
                    product_Name = s.product_Name,
                    qty_Bal = s.qty_Bal,
                    qty_Count1 = null,
                    qty_Count2 = null,
                    qty_Count3 = s.qty_Count,
                    qty_Diff = s.qty_Diff,
                    barcode = s.barcode,
                    unit = s.unit,
                    counting = s.counting
                });



                var uniondata = Group.Union(Group1).Union(Group2).Union(Group3).GroupBy(g => new
                {
                    g.cyclecount_No,
                    g.cyclecount_Date,
                    g.location_Index,
                    g.location_Id,
                    g.location_Name,
                    g.product_Id,
                    g.product_Name,
                    g.barcode,
                    g.unit,
                }).Join(location,
                             group => group.Key.location_Index,
                             Lo => Lo.location_Index,
                             (group, Lo) => new PrintOutCycleCountViewModel
                             {
                                 cyclecount_No = group.Key.cyclecount_No,
                                 cyclecount_Date = group.Key.cyclecount_Date,
                                 location_Name = group.Key.location_Name,
                                 product_Id = group.Key.product_Id,
                                 product_Name = group.Key.product_Name,
                                 qty_Bal = group.FirstOrDefault(gw => gw?.qty_Bal != null && gw?.counting == group.Max(m => m.counting))?.qty_Bal,
                                 qty_Count1 = Group1.Count() > 0 ? group.Sum(gs => gs?.qty_Count1) : null,
                                 qty_Count2 = Group2.Count() > 0 ? group.Sum(gs => gs?.qty_Count2) : null,
                                 qty_Count3 = Group3.Count() > 0 ? group.Sum(gs => gs?.qty_Count3) : null,
                                 qty_Diff = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == group.Max(m => m.counting))?.qty_Diff,
                                 barcode = group.Key.barcode,
                                 unit = group.Key.unit,
                                 location_Id = group.Key.location_Id,
                                 location_Aisle = Lo.location_Aisle,
                                 location_Prefix_desc = Lo.location_Prefix_desc,
                                 status = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == group.Max(m => m.counting))?.qty_Diff == 0 ? "Complete" : "Diff"
                             }
                             ).OrderBy(o => o.location_Aisle).ThenByDescending(td => td.location_Prefix_desc).ThenBy(t => t.location_Id).ToList();
                //.Select(s => new PrintOutCycleCountViewModel
                //{
                //    cyclecount_No = s.Key.cyclecount_No,
                //    cyclecount_Date = s.Key.cyclecount_Date,
                //    location_Name = s.Key.location_Name,
                //    product_Id = s.Key.product_Id,
                //    product_Name = s.Key.product_Name,
                //    qty_Bal = s.FirstOrDefault(gw => gw?.qty_Bal != null && gw?.counting == s.Max(m => m.counting))?.qty_Bal,
                //    qty_Count1 = Group1.Count() > 0 ? s.Sum(gs => gs?.qty_Count1) : null,
                //    qty_Count2 = Group2.Count() > 0 ? s.Sum(gs => gs?.qty_Count2) : null,
                //    qty_Count3 = Group3.Count() > 0 ? s.Sum(gs => gs?.qty_Count3) : null,
                //    qty_Diff = s.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.counting == s.Max(m => m.counting))?.qty_Diff,
                //    barcode = s.Key.barcode,
                //    unit = s.Key.unit
                //}).ToList();


                rootPath = rootPath.Replace("\\CyclecountAPI", "");
                //var reportPath = rootPath + "\\CyclecountBusiness\\Reports\\PrintOutCycleCount\\PrintOutCycleCount_Excel.rdlc";
                var reportPath = rootPath + "\\Reports\\PrintOutCycleCount\\PrintOutCycleCount_Excel.rdlc";
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", uniondata);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public List<ItemListViewModel> autoCycleCountNo(ItemListViewModel data)
        {
            try
            {
                var query = db.wm_CycleCount.AsQueryable();

                if (data.key == "-")
                {

                }
                else if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.CycleCount_No.Contains(data.key));
                }

                var items = new List<ItemListViewModel>();
                var result = query.Select(c => new { c.CycleCount_Index, c.CycleCount_No }).Distinct().Take(10).ToList();

                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {

                        index = item.CycleCount_Index,
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

        #region popupProductfilter
        public actionResultProductViewModel popupProductfilter(SearchProductViewModel data)
        {
            try
            {

                var dbMS = new MasterDbContext();

                var query = dbMS.MS_Product.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0).Join(dbMS.MS_ProductOwner,
                    p => p.Product_Index,
                    po => po.Product_Index,
                    (p, po) => new ProductViewModel
                    {
                        product_Index = p.Product_Index,
                        product_Id = p.Product_Id,
                        product_Name = p.Product_Name,
                        product_SecondName = p.Product_SecondName,
                        product_ThirdName = p.Product_ThirdName,
                        productConversion_Index = p.ProductConversion_Index,
                        productConversion_Id = p.ProductConversion_Id,
                        productConversion_Name = p.ProductConversion_Name,
                        productItemLife_Y = p.ProductItemLife_Y,
                        productItemLife_M = p.ProductItemLife_M,
                        productItemLife_D = p.ProductItemLife_D,
                        productImage_Path = p.ProductImage_Path,
                        isActive = p.IsActive,
                        owner_Index = po.Owner_Index,
                    }
                    ).AsQueryable();


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.product_Id.StartsWith(data.key));


                    //query = query.Where(c => c.Product_Id.EndsWith(data.key));

                    //query = query.Where(c => c.Product_Id.Contains(data.key));
                }
                if (!string.IsNullOrEmpty(data?.owner_Index?.ToString()))
                {
                    query = query.Where(c => c.owner_Index == data.owner_Index);
                }

                var Item = new List<ProductViewModel>();
                var TotalRow = new List<ProductViewModel>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }

                Item = query.OrderBy(o => o.product_Id).ToList();

                var result = new List<SearchProductViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchProductViewModel();

                    resultItem.product_Index = item.product_Index.Value;
                    resultItem.product_Id = item.product_Id;
                    resultItem.product_Name = item.product_Name;
                    resultItem.product_SecondName = item.product_SecondName;
                    resultItem.product_ThirdName = item.product_ThirdName;
                    resultItem.productConversion_Index = item.productConversion_Index.Value;
                    resultItem.productConversion_Id = item.productConversion_Id;
                    resultItem.productConversion_Name = item.productConversion_Name;
                    resultItem.productItemLife_Y = item.productItemLife_Y;
                    resultItem.productItemLife_M = item.productItemLife_M;
                    resultItem.productItemLife_D = item.productItemLife_D;
                    resultItem.productImage_Path = item.productImage_Path;
                    resultItem.isActive = item.isActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultProductViewModel = new actionResultProductViewModel();
                actionResultProductViewModel.itemsProduct = result.ToList();
                actionResultProductViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultProductViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region popupZonefilter
        public actionResultZoneViewModel popupZonefilter(SearchZoneViewModel data)
        {
            try
            {
                var dbMS = new MasterDbContext();

                var query = dbMS.MS_Zone.AsQueryable();
                query = query.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);


                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Zone_Name.StartsWith(data.key));
                }

                var Item = new List<MS_Zone>();
                var TotalRow = new List<MS_Zone>();

                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);

                }

                Item = query.OrderBy(o => o.Zone_Id).ToList();

                var result = new List<SearchZoneViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchZoneViewModel();

                    resultItem.zone_Index = item.Zone_Index;
                    resultItem.zone_Id = item.Zone_Id;
                    resultItem.zone_Name = item.Zone_Name;
                    resultItem.isActive = item.IsActive;
                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultZoneViewModel = new actionResultZoneViewModel();
                actionResultZoneViewModel.itemsZone = result.ToList();
                actionResultZoneViewModel.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };

                return actionResultZoneViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AutoOwnerfilter
        public List<ItemListViewModel> autoOwnerfilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();
                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwneIdrFilter"), filterModel.sJson());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public string ExportCycleCountDetail(CycleCountDetailViewModel data, string rootPath = "")
        {
            try
            {
                var location = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocation"), new { }.sJson());
                var result = new List<CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel>();
                rootPath = rootPath.Replace("\\CyclecountAPI", "");
                //var reportPath = rootPath + "\\CyclecountBusiness\\Reports\\CycleCount\\CycleCountDetail.rdlc";
                var reportPath = rootPath + "\\Reports\\CycleCount\\CycleCountDetail.rdlc";

                var query = db.wm_CycleCountDetail.Where(c => c.CycleCount_Index == data.cycleCount_Index).ToList();

                //var count = query.Select(s => new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel
                //{
                //    cyclecount_No = s.CycleCount_No,
                //    location_Id = s.Location_Id,
                //    location_Name = s.Location_Name,
                //    zone_Id = s.Zone_Id,
                //    zone_Name = s.Zone_Name,
                //    product_Id = s.Product_Id,
                //    product_Name = s.Product_Name,
                //    qty_Count = null,
                //    qty_Count2 = null,
                //    qty_Count3 = null,
                //    qty_Bal = s.Qty_Bal,
                //    qty_Diff = s.Qty_Diff,
                //    create_By = null,
                //    create_By2 = null,
                //    create_By3 = null,
                //    count = s.Counting,
                //}).ToList();

                var count1 = query.Where(c => c.Counting == 1).Select(s => new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel
                {
                    cyclecount_No = s.CycleCount_No,
                    location_Index = s.Location_Index,
                    location_Id = s.Location_Id,
                    location_Name = s.Location_Name,
                    zone_Id = s.Zone_Id,
                    zone_Name = s.Zone_Name,
                    product_Id = s.Product_Id,
                    product_Name = s.Product_Name,
                    qty_Count = s.Qty_Count,
                    qty_Count2 = null,
                    qty_Count3 = null,
                    qty_Bal = s.Qty_Bal,
                    qty_Diff = s.Qty_Diff,
                    create_By = s.Create_By,
                    create_By2 = null,
                    create_By3 = null,
                    count = s.Counting,
                    unit = "EA",
                }).ToList();
                var count2 = query.Where(c => c.Counting == 2).Select(s => new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel
                {
                    cyclecount_No = s.CycleCount_No,
                    location_Index = s.Location_Index,
                    location_Id = s.Location_Id,
                    location_Name = s.Location_Name,
                    zone_Id = s.Zone_Id,
                    zone_Name = s.Zone_Name,
                    product_Id = s.Product_Id,
                    product_Name = s.Product_Name,
                    qty_Count = null,
                    qty_Count2 = s.Qty_Count,
                    qty_Count3 = null,
                    qty_Bal = s.Qty_Bal,
                    qty_Diff = s.Qty_Diff,
                    create_By = null,
                    create_By2 = s.Create_By,
                    create_By3 = null,
                    count = s.Counting,
                    unit = "EA",
                }).ToList();
                var count3 = query.Where(c => c.Counting == 3).Select(s => new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel
                {
                    cyclecount_No = s.CycleCount_No,
                    location_Index = s.Location_Index,
                    location_Id = s.Location_Id,
                    location_Name = s.Location_Name,
                    zone_Id = s.Zone_Id,
                    zone_Name = s.Zone_Name,
                    product_Id = s.Product_Id,
                    product_Name = s.Product_Name,
                    qty_Count = null,
                    qty_Count2 = null,
                    qty_Count3 = s.Qty_Count,
                    qty_Bal = s.Qty_Bal,
                    qty_Diff = s.Qty_Diff,
                    create_By = null,
                    create_By2 = null,
                    create_By3 = s.Create_By,
                    count = s.Counting,
                    unit = "EA",
                }).ToList();

                var uniondata = count1.Union(count2).Union(count3).GroupBy(g => new
                {
                    g.cyclecount_No,
                    g.cyclecount_Date,
                    g.location_Index,
                    g.location_Id,
                    g.location_Name,
                    g.zone_Id,
                    g.zone_Name,
                    g.product_Id,
                    g.product_Name,
                    g.unit,
                }).Join(location,
                     count => count.Key.location_Index,
                     Lo => Lo.location_Index,
                     (group, Lo) => new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel
                     {
                         cyclecount_No = group.Key.cyclecount_No,
                         cyclecount_Date = group.Key.cyclecount_Date,
                         location_Name = group.Key.location_Name,
                         zone_Id = group.Key.zone_Id,
                         zone_Name = group.Key.zone_Name,
                         product_Id = group.Key.product_Id,
                         product_Name = group.Key.product_Name,
                         qty_Bal = group.FirstOrDefault(gw => gw?.qty_Bal != null && gw?.count == group.Max(m => m.count))?.qty_Bal,
                         qty_Count = count1.Count() > 0 ? group.Sum(gs => gs?.qty_Count) : null,
                         qty_Count2 = count2.Count() > 0 ? group.Sum(gs => gs?.qty_Count2) : null,
                         qty_Count3 = count3.Count() > 0 ? group.Sum(gs => gs?.qty_Count3) : null,
                         qty_Diff = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.count == group.Max(m => m.count))?.qty_Diff,
                         create_By = count1.Count() > 0 ? count1.FirstOrDefault()?.create_By : null,
                         create_By2 = count2.Count() > 0 ? count2.FirstOrDefault()?.create_By2 : null,
                         create_By3 = count3.Count() > 0 ? count3.FirstOrDefault()?.create_By3 : null,
                         unit = group.Key.unit,
                         location_Id = group.Key.location_Id,
                         location_Aisle = Lo.location_Aisle,
                         location_Prefix_desc = Lo.location_Prefix_desc,
                         status = group.FirstOrDefault(gw => gw?.qty_Diff != null && gw?.count == group.Max(m => m.count))?.qty_Diff == 0 ? "Complete" : "Diff"
                     }
                     ).OrderBy(o => o.location_Aisle).ThenByDescending(td => td.location_Prefix_desc).ThenBy(t => t.location_Id).ToList();


                //if (query.Count > 0)
                //{
                //    foreach (var dataDetail in query.OrderBy(o => o.Counting).OrderBy(o => o.Product_Id))
                //    {
                //        var resultItem = new CyclecountBusiness.Reports.CycleCount.CycleCountDetailViewModel();

                //        resultItem.cyclecount_No = dataDetail.CycleCount_No;
                //        resultItem.location_Id = dataDetail.Location_Id;
                //        resultItem.location_Name = dataDetail.Location_Name;
                //        resultItem.zone_Id = dataDetail.Zone_Id;
                //        resultItem.zone_Name = dataDetail.Zone_Name;
                //        resultItem.product_Id = dataDetail.Product_Id;
                //        resultItem.product_Name = dataDetail.Product_Name;
                //        resultItem.qty_Bal = dataDetail.Qty_Bal;
                //        resultItem.qty_Count = dataDetail.Qty_Count;
                //        resultItem.qty_Diff = dataDetail.Qty_Diff;
                //        resultItem.create_By = dataDetail.Create_By;
                //        resultItem.count = dataDetail.Counting;

                //        result.Add(resultItem);
                //    }
                //}

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", uniondata);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string saveReport(byte[] file, string name, string rootPath)
        {
            var saveLocation = PhysicalPath(name, rootPath);
            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                try
                {
                    bw.Write(file);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return VirtualPath(name);
        }
        public string PhysicalPath(string name, string rootPath)
        {
            var filename = name;
            var vPath = ReportPath;
            var path = rootPath + vPath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var saveLocation = System.IO.Path.Combine(path, filename);
            return saveLocation;
        }
        public string VirtualPath(string name)
        {
            var filename = name;
            var vPath = ReportPath;
            vPath = vPath.Replace("~", "");
            return vPath + filename;
        }
        private string ReportPath
        {
            get
            {
                var url = "\\ReportGenerator\\";
                return url;
            }
        }

        public List<CycleCountViewModel> confirmStatus(CycleCountViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            try
            {
                var result = new List<CycleCountViewModel>();

                #region Update CycleCount Status
                var ItemHeader = db.wm_CycleCount.Find(data.cycleCount_Index);
                ItemHeader.Document_Status = 1;
                ItemHeader.Update_By = data.update_By;
                ItemHeader.Update_Date = DateTime.Now;
                #endregion


                var transactionx = db.Database.BeginTransaction();
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }
                catch (Exception exy)
                {

                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("confirmStatus_CycleCount", msglog);
                    transactionx.Rollback();

                    throw exy;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Confirm
        public Boolean Confirm(CycleCountViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {


                var CycleCount = db.wm_CycleCount.Find(data.cycleCount_Index);


                if (CycleCount != null)
                {
                    if (CycleCount.Document_Status == -1 || CycleCount.Document_Status == 3)
                    {
                        return false;
                    }

                    CycleCount.Document_Status = 3;
                    CycleCount.Update_By = data.update_By;
                    CycleCount.Update_Date = DateTime.Now;


                    var task = db.wm_TaskCycleCountItem.Where(c => c.Ref_Document_Index == CycleCount.CycleCount_Index).ToList();
                    if (task.Count() > 0)
                    {
                        foreach (var item in task)
                        {
                            var TaskOld = db.wm_TaskCycleCount.Find(item.Task_Index);

                            if (TaskOld != null)
                            {
                                TaskOld.Document_Status = 3;
                                TaskOld.Update_By = data.update_By;
                                TaskOld.Update_Date = DateTime.Now;
                            }
                        }
                    }


                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeleteCycleCount", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }

                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public string AdjustStock(CycleCountViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            Boolean genHeaderGT = false;
            Boolean genDetailGT = false;
            var DocumentType = new DocumentTypeViewModel();
            string resultMsg = "";
            string resultMsgH = "";
            try
            {
                var result = new List<CycleCountViewModel>();

                var CycleCount = db.wm_CycleCount.Find(data.cycleCount_Index);

                if ((CycleCount.Document_Status ?? -1) < 4 && (CycleCount.Document_Status ?? -1) != -1)
                {
                    var GroupCycleCountDetail = db.wm_CycleCountDetail.Where(
                        c => c.CycleCount_Index == data.cycleCount_Index
                        && c.Document_Status != -1)
                        .GroupBy(g => new
                        {
                            g.CycleCount_Index,
                            g.Location_Index,
                            g.Location_Id,
                            g.Location_Name,
                            g.Product_Index,
                            g.Product_Id,
                            g.Product_Name
                        })
                        .Select(s => new
                        {
                            s.Key.CycleCount_Index,
                            s.Key.Location_Index,
                            s.Key.Location_Id,
                            s.Key.Location_Name,
                            s.Key.Product_Index,
                            s.Key.Product_Id,
                            s.Key.Product_Name,
                            Counting = s.Max(l => (l.Counting ?? 0))
                        }).ToList();
                    var GT = new GoodsTransferViewModel();
                    if (GroupCycleCountDetail.Count() > 0)
                    {
                        foreach (var itemDetail in GroupCycleCountDetail)
                        {
                            var CycleCountDetail = db.wm_CycleCountDetail.FirstOrDefault(c => c.CycleCount_Index == itemDetail.CycleCount_Index && c.Product_Index == itemDetail.Product_Index && c.Location_Index == itemDetail.Location_Index && c.Counting == itemDetail.Counting && c.Document_Status != -1);
                            if (CycleCountDetail.Qty_Diff > 0)
                            {
                                var qty_diff = CycleCountDetail.Qty_Diff;
                                var binbalance = db.wm_BinBalance.Where(c =>
                                                 c.Owner_Index == data.owner_Index
                                                 && c.Product_Index == CycleCountDetail.Product_Index
                                                 && c.Location_Index == CycleCountDetail.Location_Index
                                                 && (c.BinBalance_QtyBal - c.BinBalance_QtyReserve > 0)
                                                 ).OrderBy(o => o.GoodsReceive_Date).ToList();
                                if (binbalance.Count() == 0)
                                {
                                    resultMsg += CycleCountDetail.Product_Id + " : นี้ไม่สามารถปรับยอดได้เนื่องจากมีการจองทั้งหมด,";
                                }
                                else
                                {
                                    if (!genHeaderGT)
                                    {
                                        DocumentType = utils.SendDataApi<List<DocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), new { process_Index = Guid.Empty, documentType_Index = new Guid("243D5864-6F5F-41EF-B7B0-179F97F29A89") }.sJson()).FirstOrDefault();
                                        var dataGT = new GoodsTransferViewModel
                                        {
                                            goodsTransfer_Date = DateTime.Now.toString(),
                                            goodsTransfer_Time = DateTime.Now.ToString("HH:mm"),
                                            goodsTransfer_Doc_Date = DateTime.Now.toString(),
                                            goodsTransfer_Doc_Time = DateTime.Now.ToString("HH:mm"),
                                            owner_Index = data.owner_Index.Value,
                                            owner_Id = data.owner_Id,
                                            owner_Name = data.owner_Name,
                                            documentType_Index = DocumentType.documentType_Index,
                                            documentType_Id = DocumentType.documentType_Id,
                                            documentType_Name = DocumentType.documentType_Name,
                                            create_By = data.create_By
                                        };
                                        GT = utils.SendDataApi<GoodsTransferViewModel>(new AppSettingConfig().GetUrl("CreateGTHeader"), dataGT.sJson());
                                        genHeaderGT = true;
                                        resultMsgH = "ปรับยอดสำเร็จ " + GT.goodsTransfer_No + ",";
                                    }

                                    resultMsg += CycleCountDetail.Product_Id + " : นี้ปรับยอดสำเร็จ,";
                                    foreach (var bb in binbalance)
                                    {
                                        var pickProduct = new PickbinbalanceViewModel();
                                        if ((bb.BinBalance_QtyBal - bb.BinBalance_QtyReserve) > qty_diff && qty_diff > 0)
                                        {
                                            pickProduct.binbalance_Index = bb.BinBalance_Index.ToString();
                                            pickProduct.goodsTransfer_Index = GT.goodsTransfer_Index.ToString();
                                            pickProduct.pick = qty_diff;

                                            var unin = new ProductConversionViewModelDoc
                                            {
                                                productconversion_Ratio = bb.BinBalance_Ratio
                                            };
                                            pickProduct.unit = unin;
                                            pickProduct.documentType_Index = DocumentType.documentType_Index.ToString();
                                            pickProduct.create_By = data.create_By;
                                            pickProduct.goodsTransfer_No = GT.goodsTransfer_No;
                                            qty_diff = 0;
                                            var GTI = utils.SendDataApi<actionResultPickbinbalanceViewModel>(new AppSettingConfig().GetUrl("CreateGTItem"), pickProduct.sJson());
                                            if (GTI.resultIsUse)
                                            {
                                                var transaction = db.Database.BeginTransaction();
                                                try
                                                {
                                                    genDetailGT = true;
                                                    CycleCountDetail.Document_Status = 3;
                                                    db.SaveChanges();
                                                    transaction.Commit();
                                                }

                                                catch (Exception exy)
                                                {
                                                    msglog = State + " ex Rollback " + exy.Message.ToString();
                                                    olog.logging("AdjustStock", msglog);
                                                    transaction.Rollback();
                                                    throw exy;
                                                }
                                            }
                                            else
                                            {
                                                return "API Transfer Error";
                                            }
                                        }
                                        else if ((bb.BinBalance_QtyBal - bb.BinBalance_QtyReserve) <= qty_diff && qty_diff > 0)
                                        {
                                            pickProduct.binbalance_Index = bb.BinBalance_Index.ToString();
                                            pickProduct.goodsTransfer_Index = GT.goodsTransfer_Index.ToString();
                                            pickProduct.pick = (bb.BinBalance_QtyBal - bb.BinBalance_QtyReserve);

                                            var unin = new ProductConversionViewModelDoc
                                            {
                                                productconversion_Ratio = bb.BinBalance_Ratio
                                            };
                                            pickProduct.unit = unin;
                                            pickProduct.documentType_Index = DocumentType.documentType_Index.ToString();
                                            pickProduct.create_By = data.create_By;
                                            pickProduct.goodsTransfer_No = GT.goodsTransfer_No;
                                            qty_diff = qty_diff - (bb.BinBalance_QtyBal - bb.BinBalance_QtyReserve);
                                            var GTI = utils.SendDataApi<actionResultPickbinbalanceViewModel>(new AppSettingConfig().GetUrl("CreateGTItem"), pickProduct.sJson());
                                            if (GTI.resultIsUse)
                                            {
                                                var transaction = db.Database.BeginTransaction();
                                                try
                                                {
                                                    genDetailGT = true;
                                                    CycleCountDetail.Document_Status = qty_diff == 0 ? 3 : 2;
                                                    db.SaveChanges();
                                                    transaction.Commit();
                                                }

                                                catch (Exception exy)
                                                {
                                                    msglog = State + " ex Rollback " + exy.Message.ToString();
                                                    olog.logging("AdjustStock", msglog);
                                                    transaction.Rollback();
                                                    throw exy;
                                                }
                                            }
                                            else
                                            {
                                                return "API Transfer Error";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (genHeaderGT && genDetailGT)
                        {
                            GT.document_Status = 0;
                            GT.create_By = data.create_By;
                            var Checkitem = utils.GetDataApi<List<GoodsTransferItemViewModel>>(new AppSettingConfig().GetUrl("getGoodsTransferItem"), GT.goodsTransfer_Index.ToString(), "false");
                            if (Checkitem.Count > 0)
                            {
                                var location = utils.SendDataApi<List<LocationViewModel>>(new AppSettingConfig().GetUrl("GetLocation"), new { location_Index = new AppSettingConfig().GetUrl("confixLocationAdjust") }.sJson()).FirstOrDefault();
                                foreach (var CI in Checkitem)
                                {
                                    CI.location_Index_To = location.location_Index;
                                    CI.location_Id_To = location.location_Id;
                                    CI.location_Name_To = location.location_Name;
                                    var addlocation = utils.SendDataApi<string>(new AppSettingConfig().GetUrl("AddNewLocationTransfer"), CI.sJson());
                                }
                                GT.listGoodsTransferItemViewModel = Checkitem;

                                var updateGT = utils.SendDataApi<Boolean>(new AppSettingConfig().GetUrl("UpdateDocument"), GT.sJson());
                            }
                        }
                        var transaction2 = db.Database.BeginTransaction();
                        try
                        {
                            CycleCount.Document_Status = 4;
                            db.SaveChanges();
                            transaction2.Commit();
                        }

                        catch (Exception exy)
                        {
                            msglog = State + " ex Rollback " + exy.Message.ToString();
                            olog.logging("AdjustStock", msglog);
                            transaction2.Rollback();
                            throw exy;
                        }
                    }
                }
                else
                {
                    return "เอกสารนี้ยังไม่เสร็จสิน ยังไม่สามารถ Adjust ได้";
                }

                var msg = resultMsgH + resultMsg;
                return msg;
            }
            catch (Exception ex)
            {
                msglog = State + " ex Rollback " + ex.Message.ToString();
                olog.logging("AdjustStock", msglog);
                throw ex;
            }
        }
    }
}
