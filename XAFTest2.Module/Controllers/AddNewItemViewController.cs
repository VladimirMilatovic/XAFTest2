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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XAFTest2.Module.BusinessObjects;

namespace XAFTest2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AddNewItemViewController : ViewController
    {
        private NewObjectViewController controller;
        SingleChoiceAction singleChoiceAction;


        public AddNewItemViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            controller = Frame.GetController<NewObjectViewController>();

            if (controller != null)
            {
                controller.ObjectCreated += controller_ObjectCreated;
            }
        }

        protected void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            PropertyInfo propertyCompany = e.CreatedObject.GetType().GetProperty("Company", BindingFlags.Public | BindingFlags.Instance);
            if (propertyCompany?.CanWrite == true)
            {
                singleChoiceAction = Application.MainWindow.Controllers[typeof(SelectCompanyWinController)].Actions["SelectCompany"] as SingleChoiceAction;
                Company company = e.ObjectSpace.GetObjectByKey<Company>(singleChoiceAction.SelectedItem.Data);

                propertyCompany.SetValue(e.CreatedObject, company);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            if (controller != null) controller.ObjectCreated -= controller_ObjectCreated;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
