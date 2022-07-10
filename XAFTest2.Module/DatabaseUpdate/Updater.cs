using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using XAFTest2.Module.BusinessObjects;
using XAFTest2.Module.Helper;

namespace XAFTest2.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater
{
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion)
    {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();
        //string name = "MyName";
        //DomainObject1 theObject = ObjectSpace.FirstOrDefault<DomainObject1>(u => u.Name == name);
        //if(theObject == null) {
        //    theObject = ObjectSpace.CreateObject<DomainObject1>();
        //    theObject.Name = name;
        //}
        //Random rnd = new();
        //for (int i = 1; i < 101; i++)
        //{
        //    Company company = ObjectSpace.CreateObject<Company>();
        //    company.PIB = i.ToString();
        //    company.Name = $"Company {i}";
        //    ObjectSpace.CommitChanges();

        //    for (int a = 0; a < 3; a++)
        //    {
        //        CompanyContact contact = ObjectSpace.CreateObject<CompanyContact>();
        //        contact.ContactType = $"Phone";
        //        contact.ContactValue = $"Street {a}{a}{a}-{a}{a}{a}";
        //        contact.Company = company;
        //    }
        //    ObjectSpace.CommitChanges();

        //    for (int a = 0; a < 3; a++)
        //    {
        //        Department contact = ObjectSpace.CreateObject<Department>();
        //        contact.Name = $"Department {i}/{a}";
        //        contact.Company = company;
        //    }
        //    ObjectSpace.CommitChanges();

        //    for (int a = 0; a < 3; a++)
        //    {
        //        CompanyAddress address = ObjectSpace.CreateObject<CompanyAddress>();
        //        address.AddressType = $"Addres type {a}";
        //        address.Street = $"Street {a}";
        //        address.City = $"City {a}";
        //        address.Country = $"Country {a}";
        //        address.ZIP = $"{a}{a}{a}{a}{a}";
        //        address.Company = company;
        //    }
        //    ObjectSpace.CommitChanges();

        //    for (int p = 1; p < 10001; p++)
        //    {
        //        Product product = ObjectSpace.CreateObject<Product>();
        //        product.Company = company;
        //        product.Name = $"Product {p} company:{i}";
        //        product.UnitPrice = (decimal)rnd.Next(100, 99999) / 100.00M;
        //        product.UnitOfMeasure = (UnitOfMeasure)rnd.Next(3);
        //        product.VatRate = rnd.Next(100) > 10 ? 21 : 7;
        //    }
        //    ObjectSpace.CommitChanges();

        //}
        //ObjectSpace.CommitChanges();
        //foreach (var company in ObjectSpace.GetObjects<Company>())
        //{
        //    var products = ObjectSpace.GetObjects<Product>().Where(x => x.Company.Oid == company.Oid).ToArray();
        //    foreach (var department in company.Departments)
        //    {
        //        for (int i = 1; i < 101; i++)
        //        {
        //            Invoice invoice = ObjectSpace.CreateObject<Invoice>();
        //            invoice.Company = company;
        //            invoice.Department = department;
        //            invoice.Customer = ObjectSpace.GetObjects<Company>().Where(x => x.Oid == rnd.Next(1, 100)).FirstOrDefault();
        //            invoice.InvoiceDate = RandomDateFromTo.GetRandomDate(DateTime.Parse("2020-01-01"), DateTime.Parse("2022-12-31"));
        //            invoice.InvoiceNumber = $"{invoice.InvoiceDate.Year}/{i}";


        //            for (int a = 0; a < rnd.Next(25); a++)
        //            {
        //                InvoiceDetail invoiceDetail = ObjectSpace.CreateObject<InvoiceDetail>();
        //                invoiceDetail.Invoice = invoice;
        //                invoiceDetail.Product = products[rnd.Next(1, 10000)];
        //                invoiceDetail.Qty = rnd.Next(20);
        //                invoiceDetail.DiscountP = rnd.Next(100) > 15 ? rnd.Next(8) * 5 : 0;
        //            }
        //            ObjectSpace.CommitChanges();
        //        }
        //    }
        //}

        ApplicationUser sampleUser = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "User");
        if (sampleUser == null)
        {
            sampleUser = ObjectSpace.CreateObject<ApplicationUser>();
            sampleUser.UserName = "User";
            // Set a password if the standard authentication type is used
            sampleUser.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persists created object(s).
            ((ISecurityUserWithLoginInfo)sampleUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser));
        }
        PermissionPolicyRole defaultRole = CreateDefaultRole();
        sampleUser.Roles.Add(defaultRole);

        ApplicationUser userAdmin = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "Admin");
        if (userAdmin == null)
        {
            userAdmin = ObjectSpace.CreateObject<ApplicationUser>();
            userAdmin.UserName = "Admin";
            // Set a password if the standard authentication type is used
            userAdmin.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persists created object(s).
            ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
        }
        // If a role with the Administrators name doesn't exist in the database, create this role
        PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
        if (adminRole == null)
        {
            adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            adminRole.Name = "Administrators";
        }
        adminRole.IsAdministrative = true;
        userAdmin.Roles.Add(adminRole);
        ObjectSpace.CommitChanges(); //This line persists created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema()
    {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
    private PermissionPolicyRole CreateDefaultRole()
    {
        PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
        if (defaultRole == null)
        {
            defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            defaultRole.Name = "Default";

            defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
        }
        return defaultRole;
    }
}
