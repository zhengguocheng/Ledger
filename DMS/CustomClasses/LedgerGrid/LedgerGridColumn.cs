using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMS.CustomClasses
{
    public enum EnumColumnFormat { Date, Amount, Text, Dropdown, CheckBox, Button }
    public enum EnumLedgetType { ID, DocumentItemID, Date, Description, Reference, Gross, VATCode, VAT, Net, NominalCode, 
        NominalCodeID, Comment, Credit, Debit, 
        //SDB Columns
        Gross1,Gross2,Gross3,Gross4,Gross5,Gross6,Total,
        Amount, DebitNominalCodeID, CreditNominalCodeID,Notes,
        //Bank Reconcile
        Payments, Receipts, Tick, ViewSource,
        //Closing trial balance
        ItemType, TransferNominalCodeID,
        //modify by @zgc 2018年11月9日22:41:15
        //add UcJournal
        Details, CodeDr, CodeCr
    }
    public enum EnumRepeatType { None, UpperCell, UpperCellIncrement, Custom }
    public class LedgerColumn
    {
        public string Name;
        public EnumColumnFormat Format;
        public EnumLedgetType LedgerType;
        public int Index;
        public EnumRepeatType RepeatType;
        public bool IsRequired = false;
        public ushort Width = 100;
        public LedgerColumn(string name, EnumColumnFormat format, EnumLedgetType leg,bool required = false)
        {
            Name = name;
            Format = format;
            LedgerType = leg;
            if (LedgerType == EnumLedgetType.Date || LedgerType == EnumLedgetType.Description || LedgerType == EnumLedgetType.VATCode
                || LedgerType == EnumLedgetType.NominalCode)
            {
                RepeatType = EnumRepeatType.UpperCell;
            }
            else if (LedgerType == EnumLedgetType.Reference)
            {
                RepeatType = EnumRepeatType.UpperCellIncrement;
            }
            else
            {
                RepeatType = EnumRepeatType.None;
            }
            IsRequired = required;

        }
    }

    public class DataItemInfo
    {
        public int ID { get; set; }
    }
}
