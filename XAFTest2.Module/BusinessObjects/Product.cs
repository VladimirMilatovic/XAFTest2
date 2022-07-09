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
    public class Product : XPObject
    { 
        public Product(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        Company company;
        UnitOfMeasure unitOfMeasure;
        decimal unitPrice;
        decimal vatRate;
        string code;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        public decimal VatRate
        {
            get => vatRate;
            set => SetPropertyValue(nameof(VatRate), ref vatRate, value);
        }


        public decimal UnitPrice
        {
            get => unitPrice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value);
        }


        public UnitOfMeasure UnitOfMeasure
        {
            get => unitOfMeasure;
            set => SetPropertyValue(nameof(UnitOfMeasure), ref unitOfMeasure, value);
        }

        
        [Association("Company-Products")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

    }

    public enum UnitOfMeasure { Pcs, L, kg, m}
}