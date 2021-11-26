using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePage
{
    public class TreeDataManger
    {
        public static TreeNode CreatePrafabItemNode()
        {
            TreeNode treeNode = new TreeNode();
            treeNode.ItemTypeSelect = "全部";
            treeNode.ItemTypeList = new System.Collections.ObjectModel.ObservableCollection<string>() { "全部" };

            TreeItemNode treeItemNode1 = new TreeItemNode();
            {
                treeItemNode1.IsParentNode = true;
                treeItemNode1.NodeName = "父节点1";

                TreeItemNode chirdNode = new TreeItemNode();
                chirdNode.IsParentNode = false;
                chirdNode.NodeName = "子节点1";

                treeItemNode1.Childs = new System.Collections.ObjectModel.ObservableCollection<TreeItemNode>() { chirdNode };
            }

            treeNode.ShowItemList = new System.Collections.ObjectModel.ObservableCollection<TreeItemNode>() { treeItemNode1 };

            return treeNode;
        }
    }
}
