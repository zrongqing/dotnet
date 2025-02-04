using System.Collections.ObjectModel;
using App.UI;
using UiDesktopApp.Models;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.ViewModels.Pages;

public partial class TreeViewModel : ObservableObject, INavigationAware,IAppViewModel
{
    [ObservableProperty]
    private ObservableCollection<TreeNode> _treeNodes;
    
    [ObservableProperty]
    private TreeNode _selectedTreeNode;

    [RelayCommand]
    private void SelectNode()
    {
        
    }

    public TreeViewModel()
    {
        // Initialize with some sample data
        TreeNodes = new ObservableCollection<TreeNode>
        {                                  
            new TreeNode { Name = "Node 1" },
            new TreeNode { Name = "Node 2", Children = new ObservableCollection<TreeNode>
            {
                new TreeNode { Name = "Child Node 2.1" },
                new TreeNode{Name = "Child Node 2.2"},
                new TreeNode{Name = "Child Node 2.3"},
            }},
            new TreeNode { Name = "Node 3" ,Children = new ObservableCollection<TreeNode>
            {
                new TreeNode { Name = "Child Node 3.1" },
                new TreeNode { Name = "Child Node 3.2"},
                new TreeNode { Name = "Child Node 3.3"},
            }},
        };
    }


    private void InitData()
    {
        var nodes = new List<TreeNode>();
        nodes.Add(new TreeNode { Name = "Node 1", Id = "0" ,ParentId = "-1"});
        nodes.Add(new TreeNode { Name = "Node 2", Id = "1",ParentId = "-1"});
        nodes.Add(new TreeNode { Name = "Node 2.1", Id = "2",ParentId = "0"});
        nodes.Add(new TreeNode { Name = "Node 2.2", Id = "3",ParentId = "0"});
        nodes.Add(new TreeNode { Name = "Node 2.3", Id = "4",ParentId = "0"});
        nodes.Add(new TreeNode { Name = "Node 3", Id = "5" ,ParentId = "-1"});
        nodes.Add(new TreeNode { Name = "Node 3.1", Id = "0" ,ParentId = "5"});
        nodes.Add(new TreeNode { Name = "Node 3.2", Id = "0" ,ParentId = "5"});
    }

    private List<TreeNode> GetTreeNodes(string parentId, List<TreeNode> nodes)
    {
        var mainNodes = nodes.Where(x => x.ParentId == parentId).ToList();
        var otherNodes = nodes.Where(x => !x.ParentId.Equals(parentId)).ToList();
        foreach (var node in mainNodes)
        {
            node.Children = new ObservableCollection<TreeNode>(GetTreeNodes(node.Id, otherNodes));
        }

        return mainNodes;
    }
    
    public Task OnNavigatedToAsync()
    {
        return Task.CompletedTask;
    }

    public Task OnNavigatedFromAsync()
    {
        return Task.CompletedTask;
    }
} 