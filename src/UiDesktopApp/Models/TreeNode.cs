using System.Collections.ObjectModel;

namespace UiDesktopApp.Models;

public partial class TreeNode:ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private ObservableCollection<TreeNode> _children = [];
}