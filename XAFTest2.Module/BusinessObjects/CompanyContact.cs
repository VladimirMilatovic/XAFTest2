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
    public class CompanyContact : XPObject
    { 
        public CompanyContact(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        Company company;
        string contactValue;
        string contactType;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ContactType
        {
            get => contactType;
            set => SetPropertyValue(nameof(ContactType), ref contactType, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ContactValue
        {
            get => contactValue;
            set => SetPropertyValue(nameof(ContactValue), ref contactValue, value);
        }


        [Association("Company-Contacts")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

    }
}