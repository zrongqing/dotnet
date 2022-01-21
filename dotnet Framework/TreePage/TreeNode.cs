using RQ.HWComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePage
{
    public class TreeNode : NotifyPropertyBase
    {
        private ObservableCollection<string> typeNumberList;
        private ObservableCollection<TreeItemNode> showItemList;

        /// <summary>
        /// 全部的数据集合
        /// </summary>
        private Dictionary<string, TreeItemNode> MEPPrefabMemData { get; set; }

        /// <summary>
        /// 展示的数据源
        /// </summary>
        /// <remarks>
        /// Filtered item list for UI.
        /// </remarks>
        public ObservableCollection<TreeItemNode> ShowItemList
        {
            get => showItemList;
            set
            {
                showItemList = value;
                OnPropertyChanged(nameof(ShowItemList));
            }
        }

        /// <summary>
        /// 分类集合
        /// </summary>
        /// <remarks>
        /// All type numbers list with "全部" in combobox
        /// </remarks>
        public ObservableCollection<string> ItemTypeList
        {
            get => typeNumberList;
            set
            {
                typeNumberList = value;
                OnPropertyChanged(nameof(ItemTypeList));
            }
        }

        /// <summary>
        /// 选择的ItemType
        /// </summary>
        public string ItemTypeSelect { get; set; }

        /// <summary>
        /// 数据来源的标记
        /// </summary>
        public string DataSign { get; private set; }
    }
}
