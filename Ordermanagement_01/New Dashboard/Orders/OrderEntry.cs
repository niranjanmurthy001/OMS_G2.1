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
        private Dictionary<LayoutControlItem, int> dictionaryLayoutItems;
        private Dictionary<LayoutControlGroup, int> dictionaryLayoutGroups;
        public OrderEntry()
        {
            InitializeComponent();
            dictionaryLayoutItems = new Dictionary<LayoutControlItem, int>()
            {
                { layoutControlItemPriorDate,1 },
                { layoutControlItemDeedChain,1 },
                { layoutControlItemLoanNo, 2 },
                { layoutControlItemReqType,2 }
            };
            dictionaryLayoutGroups = new Dictionary<LayoutControlGroup, int>()
            {
                { layoutControlGroupOrder,1 },
                { layoutControlGroupOthers,1 },
                { layoutControlGroupAdditional,1 },
                { layoutControlGroupLereta,2 },
                { layoutControlGroupTitle,3 },
                { layoutControlGroupTaxCode,4 }
            };
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

        private void lookUpEditProjectType_EditValueChanged(object sender, EventArgs e)
        {
            int projectType = Convert.ToInt32(lookUpEditProjectType.EditValue);
            if (projectType > 0)
            {
                if (projectType == 4)
                {
                    dictionaryLayoutGroups.Where(kv => kv.Value == 2).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                else
                {
                    dictionaryLayoutGroups.Where(kv => kv.Value == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 2).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                if (projectType == 1)
                {
                    dictionaryLayoutItems.Where(kv => kv.Value == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutItems.Where(kv => kv.Value != 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 3).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 4).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                if (projectType == 2 || projectType == 3)
                {
                    dictionaryLayoutItems.Where(kv => kv.Value == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutItems.Where(kv => kv.Value != 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 3).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutGroups.Where(kv => kv.Value == 4).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                }
            }
            else
            {
                dictionaryLayoutGroups.Keys.ToList().ForEach(group => group.Visibility = LayoutVisibility.Never);              
            }
        }
    }
}