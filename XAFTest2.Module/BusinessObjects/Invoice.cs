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
using XAFTest2.Module.Helper;

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

        protected override void OnSaving()
        {

            var result = InvoiceNextSequenceNumber.GetNext("R[#]", Session, this);
            InvoiceNumber = result.InvoiceNumber;
            InvoiceSequenceNumber = result.SequenceNumber;

            base.OnSaving();
        }

        InvoiceType invoiceType= InvoiceType.Product;
        int invoiceSequenceNumber;
        int year;
        Company company;
        string invoiceNumber;
        Department department;
        Company bayer;
        DateTime invoiceDate;

        [Indexed("Year;Department;InvoiceSequenceNumber", Unique = true, Name = "Invoice_CompanyYearDepartmentInvoiceSequenceNumber_UniqueIndex")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

        [Browsable(false)]
        public int Year
        {
            get => year;
            set => SetPropertyValue(nameof(Year), ref year, value);
        }


        [DataSourceProperty("Company.Departments")]
        public Department Department
        {
            get => department;
            set => SetPropertyValue(nameof(Department), ref department, value);
        }

        [Browsable(false)]
        public int InvoiceSequenceNumber
        {
            get => invoiceSequenceNumber;
            set => SetPropertyValue(nameof(InvoiceSequenceNumber), ref invoiceSequenceNumber, value);
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
            set { SetPropertyValue(nameof(InvoiceDate), ref invoiceDate, value); Year = value.Year; }
        }

        [ImmediatePostData]
        public InvoiceType InvoiceType
        {
            get => invoiceType;
            set => SetPropertyValue(nameof(InvoiceType), ref invoiceType, value);
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
        
        [Association("Invoice-InvoiceServiceDetails"), DevExpress.Xpo.Aggregated]
        public XPCollection<InvoiceServiceDetail> InvoiceServiceDetails
        {
            get
            {
                return GetCollection<InvoiceServiceDetail>(nameof(InvoiceServiceDetails));
            }
        }



    }

    public enum InvoiceType { Product, Service};
}