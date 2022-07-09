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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Company : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        bool bookKeeping;
        string pIB;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PIB
        {
            get => pIB;
            set => SetPropertyValue(nameof(PIB), ref pIB, value);
        }
        
        public bool BookKeeping
        {
            get => bookKeeping;
            set => SetPropertyValue(nameof(BookKeeping), ref bookKeeping, value);
        }




        [Association("Company-Addresses"), DevExpress.Xpo.Aggregated]
        public XPCollection<CompanyAddress> Addresses
        {
            get
            {
                return GetCollection<CompanyAddress>(nameof(Addresses));
            }
        }

        [Association("Company-Contacts"), DevExpress.Xpo.Aggregated]
        public XPCollection<CompanyContact> Contacts
        {
            get
            {
                return GetCollection<CompanyContact>(nameof(Contacts));
            }
        }

        [Association("Company-Departments"), DevExpress.Xpo.Aggregated]
        public XPCollection<Department> Departments
        {
            get
            {
                return GetCollection<Department>(nameof(Departments));
            }
        }
        
        [Association("Company-Products"), DevExpress.Xpo.Aggregated]
        public XPCollection<Product> Products
        {
            get
            {
                return GetCollection<Product>(nameof(Products));
            }
        }

        //[Association("Company-Invoices"), DevExpress.Xpo.Aggregated]
        //public XPCollection<Invoice> Invoices
        //{
        //    get
        //    {
        //        return GetCollection<Invoice>(nameof(Invoices));
        //    }
        //}
    }
}