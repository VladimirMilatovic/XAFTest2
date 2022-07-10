using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XAFTest2.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class InvoiceServiceDetail : XPObject
    {
        public InvoiceServiceDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        Invoice invoice;
        int vATRate;
        double qty = 1;
        decimal price;
        string description;

        [Size(1000)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }

        public double Qty
        {
            get => qty;
            set => SetPropertyValue(nameof(Qty), ref qty, value);
        }

        public int VATRate
        {
            get => vATRate;
            set => SetPropertyValue(nameof(VATRate), ref vATRate, value);
        }

        
        [Association("Invoice-InvoiceServiceDetails")]
        public Invoice Invoice
        {
            get => invoice;
            set => SetPropertyValue(nameof(Invoice), ref invoice, value);
        }
    }
}