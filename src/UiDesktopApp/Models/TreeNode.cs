using System.Collections.ObjectModel;
using System.Diagnostics;

namespace UiDesktopApp.Models;

public partial class TreeNode:ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private ObservableCollection<TreeNode> _children = [];

    [ObservableProperty]
    private string _id = string.Empty;
    
    [ObservableProperty]
    private string _parentId = string.Empty;
}