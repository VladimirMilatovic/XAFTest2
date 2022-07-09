using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XAFTest2.Module.BusinessObjects;

namespace XAFTest2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CompanyFilterListViewController : ViewController<ListView>
    {
        SingleChoiceAction singleChoiceAction;

        public CompanyFilterListViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            singleChoiceAction = Application.MainWindow.Controllers[typeof(SelectCompanyWinController)].Actions["SelectCompany"] as SingleChoiceAction;
            singleChoiceAction.Execute += SingleChoiceAction_Execute;
            if (singleChoiceAction != null && singleChoiceAction?.SelectedItem != null)
            {
                if (View.CollectionSource.ObjectTypeInfo.OwnMembers.Where(p => p.Name == "Company").Count() > 0)
                {
                    View.CollectionSource.Criteria["CompanyFilter"] = new BinaryOperator("Company.Oid", singleChoiceAction.SelectedItem.Data);
                }
            }
        }

        private void SingleChoiceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            View.Close();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            singleChoiceAction.Execute -= SingleChoiceAction_Execute;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
