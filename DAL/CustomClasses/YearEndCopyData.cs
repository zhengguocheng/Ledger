using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.CustomClasses
{
    public class YearEndCopyData
    {
        public static void CreateNewData(long yrEndFolID)
        {
            CopyVATRate(yrEndFolID);
            CopyAnalysisCode(yrEndFolID);
            CopyAccountGroup(yrEndFolID);
            CopyChartAccounts(yrEndFolID);
        }

        public static void CopyVATRate(long yrEndFolID)
        {
            var cnt = new tblVATRateController();

            var lstVat = cnt.FetchByYearEndID(0);
            foreach (var item in lstVat)
            {
                if (item.YearEndFolderID == 0)
                {
                    var newObj = new tblVATRate();
                    newObj.Code = item.Code;
                    newObj.Percentage = item.Percentage;
                    newObj.Type = item.Type;

                    newObj.YearEndFolderID = yrEndFolID;
                    cnt.Save(newObj);
                }
            }
        }

        public static void CopyAnalysisCode(long yrEndFolID)
        {
            var cnt = new tblAnalysisCodeController();

            var lstAnyCode = cnt.FetchByYearEndID(0);
            foreach (var item in lstAnyCode)
            {
                if (item.YearEndFolderID == 0)
                {
                    var newObj = new tblAnalysisCode();
                    newObj.Code = item.Code;
                    newObj.Notes = item.Notes;

                    newObj.YearEndFolderID = yrEndFolID;
                    cnt.Save(newObj);
                }
            }
        }

        public static void CopyAccountGroup(long yrEndFolID)
        {
            var cnt = new tblAccountGroupController();

            var lstVat = cnt.FetchByYearEndID(0);
            foreach (var item in lstVat)
            {
                if (item.YearEndFolderID == 0)
                {
                    var newObj = new tblAccountGroup();
                    newObj.Name = item.Name;
                    newObj.Description = item.Description;
                    newObj.YearEndFolderID = yrEndFolID;
                    cnt.Save(newObj);
                }
            }
        }
        public static void CopyChartAccounts(long yrEndFolID)
        {
            var cnt = new tblChartAccountController();
            var cntAcctGrp = new tblAccountGroupController();

            var lstParentNomCode = cnt.FetchByYearEndID(0);

            var lstActGrpNew = cntAcctGrp.FetchByYearEndID(yrEndFolID);
            var lstActGrpOld = cntAcctGrp.FetchByYearEndID(0);

            foreach (var item in lstParentNomCode)
            {
                if (item.YearEndFolderID == 0)
                {
                    var newObj = new tblChartAccount();
                    newObj.Code = item.Code;
                    newObj.Description = item.Description;
                    newObj.Type = item.Type;

                    if (item.AccountGroupID.HasValue)
                    {
                        var oldActGrp = lstActGrpOld.FirstOrDefault(x => x.ID == item.AccountGroupID.Value);
                        if (oldActGrp != null)
                        {
                            var newActGrp = lstActGrpNew.FirstOrDefault(x => x.Name == oldActGrp.Name);
                            if (newActGrp != null)
                            {
                                newObj.AccountGroupID = newActGrp.ID;
                            }
                        }
                    }
                    newObj.YearEndFolderID = yrEndFolID;
                    cnt.Save(newObj);
                }
            }


            #region Copy YearEndCodeID data
            
            foreach (var objNom in lstParentNomCode)
            {
                if(objNom.YearEndCodeID.HasValue)
                {
                    try
                    {
                        var nomCodeToChange = cnt.FetchByCode(objNom.Code, yrEndFolID);//get ob to change
                        if (nomCodeToChange != null)
                        {
                            var objWithSameCode = cnt.FetchSimilarRecord(objNom.YearEndCodeID.Value, yrEndFolID);//Get newly added record with same code
                            if (objWithSameCode != null)
                            {
                                nomCodeToChange.YearEndCodeID = objWithSameCode.ID;
                                cnt.Save(nomCodeToChange);
                            }
                        }
                    }
                    catch(Exception ecp)
                    {
                        
                    }
                }
            }
            
            #endregion
        }

        
    }
}
