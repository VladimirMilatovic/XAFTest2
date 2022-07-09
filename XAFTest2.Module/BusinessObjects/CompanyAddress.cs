using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAFTest2.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class CompanyAddress : XPObject
    {
        public CompanyAddress(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        Company company;
        string country;
        string zIP;
        string city;
        string street;
        string address;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string AddressType
        {
            get => address;
            set => SetPropertyValue(nameof(AddressType), ref address, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Street
        {
            get => street;
            set => SetPropertyValue(nameof(Street), ref street, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string City
        {
            get => city;
            set => SetPropertyValue(nameof(City), ref city, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ZIP
        {
            get => zIP;
            set => SetPropertyValue(nameof(ZIP), ref zIP, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Country
        {
            get => country;
            set => SetPropertyValue(nameof(Country), ref country, value);
        }


        [Association("Company-Addresses")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }
    }
}
