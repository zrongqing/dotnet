/*************************************************************************************
 *
 * 创建人员:  zrq 
 * 创建时间:  2022/1/7 15:34:06
 * 文件描述:  
 * 
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombox
{
    internal class ItemListViewModel : INotifyPropertyChanged
    {
        private int selIndex = 1;

        private string selItem = string.Empty;

        private ObservableCollection<string> sizeBindingList;

        public ItemListViewModel()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("200*300*100");
            list.Add("200*200*100");
            SizeBindingList = list;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public int SelIndex
        {
            get
            {
                return selIndex;
            }

            set
            {
                selIndex = value;
                //if (selIndex == 0)
                //{
                //    selIndex = 1;
                //}
                //OnPropertyChanged(nameof(SelIndex));
            }
        }

        public string SelItem
        {
            get
            {
                return selItem;
            }
            set
            {
                selItem = value;
                if (sizeBindingList.IndexOf(value) == 0)
                {
                    selItem = sizeBindingList[1];
                }
                OnPropertyChanged(nameof(SelItem));
            }
        }

        public ObservableCollection<string> SizeBindingList { get => sizeBindingList; set => sizeBindingList = value; }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));//对Name进行监听  
            }
        }
    }
}