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
    public class InvoiceDetail : XPObject
    {
        public InvoiceDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        bool rR;
        decimal priceNoVAT;
        decimal priceWithVAT;
        decimal discountP;
        decimal discountValue;
        decimal netPriceNoVAT;
        decimal netPriceWithVAT;
        double qty = 1.00;
        decimal vATRate;
        decimal totalVAT;
        decimal totalNoVAT;
        decimal totalWithVAT;
        string productCode;
        string productName;
        UnitOfMeasure unitOfMeasure;
        Product product;
        Invoice invoice;

        [Association("Invoice-InvoiceDetails")]
        public Invoice Invoice
        {
            get => invoice;
            set => SetPropertyValue(nameof(Invoice), ref invoice, value);
        }

        [DevExpress.Xpo.Aggregated]
        public Product Product
        {
            get => product;
            set
            {
                SetPropertyValue(nameof(Product), ref product, value);
                ProductName = Product.Name;
                ProductCode = Product.Code;
                UnitOfMeasure = Product.UnitOfMeasure;
                RR = true;
                VATRate = Product.VatRate;
                PriceNoVAT = Product.UnitPrice;
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ProductName
        {
            get => productName;
            set => SetPropertyValue(nameof(ProductName), ref productName, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ProductCode
        {
            get => productCode;
            set => SetPropertyValue(nameof(ProductCode), ref productCode, value);
        }

        public UnitOfMeasure UnitOfMeasure
        {
            get => unitOfMeasure;
            set => SetPropertyValue(nameof(UnitOfMeasure), ref unitOfMeasure, value);
        }

        //Q
        public double Qty
        {
            get => qty;
            set { SetPropertyValue(nameof(Qty), ref qty, value); CalculatePrices(); }
        }

        //UPB
        public decimal PriceNoVAT
        {
            get => priceNoVAT;
            set
            {
                SetPropertyValue(nameof(PriceNoVAT), ref priceNoVAT, value);
                rR = true;
                CalculatePrices();
            }
        }

        public decimal PriceWithVAT
        {
            get => priceWithVAT;
            set
            {
                SetPropertyValue(nameof(PriceWithVAT), ref priceWithVAT, value);
                priceNoVAT = Math.Round(PriceWithVAT / (100 + VATRate) * 100, 4);
            }
        }

        //R
        public decimal DiscountP
        {
            get => discountP;
            set { SetPropertyValue(nameof(DiscountP), ref discountP, value); CalculatePrices(); }
        }

        public bool RR
        {
            get => rR;
            set => SetPropertyValue(nameof(RR), ref rR, value);
        }

        public decimal DiscountValue
        {
            get => discountValue;
            set => SetPropertyValue(nameof(DiscountValue), ref discountValue, value);
        }

        public decimal NetPriceNoVAT
        {
            get => netPriceNoVAT;
            set
            {
                SetPropertyValue(nameof(NetPriceNoVAT), ref netPriceNoVAT, value);
                NetPriceWithVAT = Math.Round(NetPriceNoVAT * (1 + VATRate / 100), 4);
            }
        }

        //UPA - Price with VAT and Rebate
        public decimal NetPriceWithVAT
        {
            get => netPriceWithVAT;
            set
            {
                SetPropertyValue(nameof(NetPriceWithVAT), ref netPriceWithVAT, value);
                if (NetPriceWithVAT < PriceWithVAT) DiscountP = Math.Round(-(NetPriceWithVAT / PriceWithVAT - 1) * 100, 2);
                RR = false;
                CalculatePrices();
            }
        }

        public decimal TotalNoVAT
        {
            get => totalNoVAT;
            set => SetPropertyValue(nameof(TotalNoVAT), ref totalNoVAT, value);
        }

        public decimal VATRate
        {
            get => vATRate;
            set => SetPropertyValue(nameof(VATRate), ref vATRate, value);
        }

        public decimal TotalVAT
        {
            get => totalVAT;
            set => SetPropertyValue(nameof(TotalVAT), ref totalVAT, value);
        }

        public decimal TotalWithVAT
        {
            get => totalWithVAT;
            set => SetPropertyValue(nameof(TotalWithVAT), ref totalWithVAT, value);
        }

        private void CalculatePrices()
        {
            if (RR)
            {
                netPriceWithVAT = Math.Round(PriceNoVAT * (1 + VATRate / 100) * (1 - DiscountP / 100), 4);
                totalNoVAT = Math.Round(PriceNoVAT * (1 - DiscountP / 100) * (decimal)Qty, 4);
                totalVAT = Math.Round(TotalNoVAT * (VATRate / 100), 4);
                totalWithVAT = TotalNoVAT + TotalVAT;
            }
            else
            {
                priceNoVAT = Math.Round(NetPriceWithVAT / ((1 + VATRate / 100) * (1 - DiscountP / 100)), 4);
                totalWithVAT = Math.Round(NetPriceWithVAT * (decimal)Qty, 4);
                totalVAT = Math.Round(TotalWithVAT * (1 - 1 / (1 + VATRate / 100)), 4);
                totalNoVAT = TotalWithVAT - TotalVAT;
            }
            priceWithVAT = Math.Round(PriceNoVAT * (1 + VATRate / 100), 4);
            discountValue = Math.Round(PriceNoVAT * (DiscountP / 100), 4);
            netPriceNoVAT = PriceNoVAT - DiscountValue;
        }
    }
}