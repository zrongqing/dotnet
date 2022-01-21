using RQ.HWComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePage
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeItemNode : NotifyPropertyBase
    {

        /// <summary>
        /// 父节点
        /// </summary>
        public TreeItemNode Parent { get; set; }

        private string nodeName { get; set; }
        /// <summary>
        /// 节点的名字
        /// </summary>
        public string NodeName
        {
            get
            {
                return nodeName;
            }
            set
            {
                nodeName = value;
                OnPropertyChanged("NodeName");
            }
        }

        private bool isSelect;
        /// <summary>
        /// 选中状态
        /// </summary>
        public bool IsSelect { get => isSelect;  set => isSelect = value; }


        private bool isParentNode { get; set; }

        /// <summary>
        /// 是否是类型节点
        /// </summary>
        /// <remarks>
        /// 类似树的父节点
        /// </remarks>
        public bool IsParentNode
        {
            get
            {
                return isParentNode;
            }
            set
            {
                isParentNode = value;
                OnPropertyChanged("IsTypeNumber");
            }
        }

        private ObservableCollection<TreeItemNode> _childs { get; set; }
        /// <summary>
        /// 递归结构，用于建树
        /// </summary>
        public ObservableCollection<TreeItemNode> Childs
        {
            get
            {
                return _childs;
            }
            set
            {
                _childs = value;
                OnPropertyChanged("Childs");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLeafItem
        {
            get
            {
                return 0 == _childs.Count;
            }
        }


        public TreeItemNode()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        public TreeItemNode(TreeItemNode parent, string name)
        {
            this.Parent = parent;
            this.Childs = new ObservableCollection<TreeItemNode>();
            this.NodeName = name;
            this.IsSelect = false;
            this.IsParentNode = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="assemblyInstance"></param>
        public TreeItemNode(TreeItemNode parent, string name, string index, object assemblyInstance)
        {
            this.Parent = parent;
            this.Childs = new ObservableCollection<TreeItemNode>();
            this.NodeName = string.Format("{0}({1})", name, index);
            this.IsSelect = false;
            this.IsParentNode = false;
        }
    }
}
