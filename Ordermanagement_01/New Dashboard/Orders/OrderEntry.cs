using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data;
using System;
using System.Collections.Generic;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraLayout;

namespace Ordermanagement_01.New_Dashboard.Orders
{
    public partial class OrderEntry : XtraForm
    {
        public OrderEntry()
        {
            InitializeComponent();
        }

        private void OrderEntry_Resize(object sender, System.EventArgs e)
        {
            // layoutControlGroupAdditional.Expanded = WindowState == FormWindowState.Maximized ? true : false;
        }

        private void OrderEntry_Load(object sender, System.EventArgs e)
        {
            DevExpress.UserSkins.BonusSkins.Register();
            WindowState = FormWindowState.Maximized;
            BindProjectType();
            gridControl2.DataSource = GetList();
        }

        private void BindProjectType()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {0,"SELECT" },
                {1,"TITLE" },
                {2,"TAX" },
                {3,"Code" }
            };
            lookUpEditProjectType.Properties.DataSource = dictionary;
            lookUpEditProjectType.Properties.DisplayMember = "Value";
            lookUpEditProjectType.Properties.ValueMember = "Key";
        }

        private List<OrderInfo> GetList()
        {
            return new List<OrderInfo>()
            {
                new OrderInfo{orderNumber="548648",client="10000",subClient="10001",Date="01/25/2020",Task="Search",user="Shashi" },
                new OrderInfo{orderNumber="548648",client="10000",subClient="10001",Date="01/26/2020",Task="Search QC", user="Kartik" },
            };
        }

        private class OrderInfo
        {
            public string orderNumber { get; set; }
            public string client { get; set; }
            public string subClient { get; set; }
            public string Date { get; set; }
            public string Task { get; set; }
            public string user { get; set; }
        }

        private void SetVisibility(List<LayoutControlItem> layoutControlItems, LayoutVisibility mode)
        {
            layoutControlItems.ForEach(layoutControlItem => layoutControlItem.Visibility = mode);
        }

        private void lookUpEditProjectType_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditProjectType.EditValue) > 0)
            {
                if (Convert.ToInt32(lookUpEditProjectType.EditValue) == 1)
                {
                    SetVisibility(new List<LayoutControlItem>() {
                        layoutControlItem27,
                        layoutControlItem28,                       
                    }, LayoutVisibility.Never);

                    SetVisibility(new List<LayoutControlItem>() {
                        layoutControlItem18,
                        layoutControlItem29
                    }, LayoutVisibility.Always);
                    layoutControlGroupTitle.Visibility = LayoutVisibility.Always;
                    layoutControlGroupTaxCode.Visibility = LayoutVisibility.Never;

                }
                if (Convert.ToInt32(lookUpEditProjectType.EditValue) == 2 || Convert.ToInt32(lookUpEditProjectType.EditValue) == 3)
                {
                    SetVisibility(new List<LayoutControlItem>() {
                        layoutControlItem27,
                        layoutControlItem28
                    }, LayoutVisibility.Always);

                    SetVisibility(new List<LayoutControlItem>() {
                        layoutControlItem18,
                        layoutControlItem29
                    }, LayoutVisibility.Never);
                    layoutControlGroupTaxCode.Visibility = LayoutVisibility.Always;
                    layoutControlGroupTitle.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                SetVisibility(new List<LayoutControlItem>() {
                        layoutControlItem27,
                        layoutControlItem28,
                        layoutControlItem18,
                        layoutControlItem29
                    }, LayoutVisibility.Always);
                layoutControlGroupTaxCode.Visibility = LayoutVisibility.Never;
                layoutControlGroupTitle.Visibility = LayoutVisibility.Never;
            }
        }
    }
}