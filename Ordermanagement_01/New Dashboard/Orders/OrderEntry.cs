using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data;
using System;
using System.Linq;
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

        private void OrderEntry_Resize(object sender, EventArgs e)
        {
            // layoutControlGroupAdditional.Expanded = WindowState == FormWindowState.Maximized ? true : false;
        }

        private void OrderEntry_Load(object sender, EventArgs e)
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
                {3,"CODE" },
                {4,"LERETA" }
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
            int projectType = Convert.ToInt32(lookUpEditProjectType.EditValue);
            if (projectType > 0)
            {
                if (projectType == 4)
                {
                    layoutControlGroupOrder.Visibility = LayoutVisibility.Never;
                    layoutControlGroupOthers.Visibility = LayoutVisibility.Never;
                    layoutControlGroupAdditional.Visibility = LayoutVisibility.Never;
                    layoutControlGroupLereta.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    layoutControlGroupOrder.Visibility = LayoutVisibility.Always;
                    layoutControlGroupOthers.Visibility = LayoutVisibility.Always;
                    layoutControlGroupAdditional.Visibility = LayoutVisibility.Always;
                    layoutControlGroupLereta.Visibility = LayoutVisibility.Never;

                }
                if (projectType == 1)
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
                if (projectType == 2 || projectType == 3)
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
                layoutControlGroupLereta.Visibility = LayoutVisibility.Never;
                layoutControlGroupOrder.Visibility = LayoutVisibility.Never;
                layoutControlGroupOthers.Visibility = LayoutVisibility.Never;
                layoutControlGroupAdditional.Visibility = LayoutVisibility.Never;
            }
        }
    }
}