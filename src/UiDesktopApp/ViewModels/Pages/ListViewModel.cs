using System.Collections.ObjectModel;
using App.UI;
using UiDesktopApp.Models;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.ViewModels.Pages;

public partial class ListViewModel : ObservableObject, INavigationAware,IAppViewModel
{
    [ObservableProperty]
    private ObservableCollection<TreeNode> _treeNodes;

    public ListViewModel()
    {
        // Initialize with some sample data
        TreeNodes = new ObservableCollection<TreeNode>
        {
            new TreeNode { Name = "Node 1" },
            new TreeNode { Name = "Node 2", Children = new ObservableCollection<TreeNode>
            {
                new TreeNode { Name = "Child Node 2.1" }
            }}
        };
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