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
    public class Invoice : XPObject
    { 
        public Invoice(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        Company company;
        string invoiceNumber;
        Department department;
        Company bayer;
        DateTime invoiceDate;

        //[Association("Company-Invoices")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

        [DataSourceProperty("Company.Departments")]
        public Department Department
        {
            get => department;
            set => SetPropertyValue(nameof(Department), ref department, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string InvoiceNumber
        {
            get => invoiceNumber;
            set => SetPropertyValue(nameof(InvoiceNumber), ref invoiceNumber, value);
        }

        public DateTime InvoiceDate
        {
            get => invoiceDate;
            set => SetPropertyValue(nameof(InvoiceDate), ref invoiceDate, value);
        }

        public Company Customer
        {
            get => bayer;
            set => SetPropertyValue(nameof(Customer), ref bayer, value);
        }

        [Association("Invoice-InvoiceDetails"), DevExpress.Xpo.Aggregated]
        public XPCollection<InvoiceDetail> InvoiceDetails
        {
            get
            {
                return GetCollection<InvoiceDetail>(nameof(InvoiceDetails));
            }
        }

        
        
    }
}