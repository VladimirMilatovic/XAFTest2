using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAFTest2.Module.BusinessObjects;

namespace XAFTest2.Module.Helper
{
    internal static class InvoiceNextSequenceNumber
    {
        public static (int SequenceNumber, string InvoiceNumber) GetNext(string format, Session sesion, Invoice invoice)
        {
            StringBuilder InvoiceNumber = new StringBuilder();
            int nextSequenceNumber;
            XPQuery<Invoice> getNextInvoiceNumber = new XPQuery<Invoice>(sesion);
            var nextNumber = from tmpInvoice in getNextInvoiceNumber
                             where (tmpInvoice.Company.Oid == invoice.Company.Oid
                                                                            && tmpInvoice.Year == invoice.Year
                                                                            && tmpInvoice.Department.Oid == invoice.Department.Oid)
                             select new { tmpInvoice.InvoiceSequenceNumber };
            if (nextNumber.FirstOrDefault() != null)
            {
                nextSequenceNumber = (int)nextNumber.Max(x=>x.InvoiceSequenceNumber) + 1;
            }
            else
            {
                nextSequenceNumber = 1;
            }

            InvoiceNumber.Append(format);
            InvoiceNumber.Replace("[#]", nextSequenceNumber.ToString());

            return (nextSequenceNumber, InvoiceNumber.ToString());
        }
    }
}
