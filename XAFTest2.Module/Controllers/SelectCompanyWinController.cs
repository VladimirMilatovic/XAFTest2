using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XAFTest2.Module.BusinessObjects;

namespace XAFTest2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class SelectCompanyWinController : WindowController
    {
        SingleChoiceAction singleChoiceAction;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public SelectCompanyWinController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            TargetWindowType = WindowType.Main;
            singleChoiceAction= new SingleChoiceAction(this, "SelectCompany", PredefinedCategory.Filters);
            singleChoiceAction.Caption = "Select active company";

        }
        protected override void OnActivated()
        {
            Session session = (Application.ObjectSpaceProvider.CreateObjectSpace() as XPObjectSpace).Session;
            XPQuery<Company> companies = new XPQuery<Company>(session);
            
            var list = from company in companies where (company.BookKeeping == true) select new {company.Name, company.Oid};

            base.OnActivated();
            if (singleChoiceAction?.Items.Count <= 0)
            {

                singleChoiceAction.Items.Clear();
                foreach (var item in list)
                {
                    singleChoiceAction.Items.Add(new ChoiceActionItem(item.Name, item.Oid));
                }
                if (singleChoiceAction.Items.Count > 0)
                {
                    singleChoiceAction.SelectedIndex = 0;
                }
            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
