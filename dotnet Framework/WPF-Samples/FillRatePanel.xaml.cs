using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Samples
{
    public class FillRate
    {
        /// <summary>
        /// 当前填充率
        /// </summary>
        private string curFillRate;

        /// <summary>
        /// 限制填充率
        /// </summary>
        private string limitFillRate;

        /// <summary>
        /// 类型
        /// </summary>
        private string type;
        public string CurFillRate { get => curFillRate; set => curFillRate = value; }
        public string LimitFillRate { get => limitFillRate; set => limitFillRate = value; }
        public string Type { get => type; set => type = value; }
    }

    public class FillRateModel : HWCommonUI.HWComponentModel.NotifyPropertyBase
    {
        public BindingList<FillRate> FillRateList { get; set; }
        public FillRateModel()
        {
            FillRateList = new BindingList<FillRate>();
        }

        public static FillRateModel TestData()
        {
            FillRateModel fillRateModel = new FillRateModel();

            FillRate fillRate = new FillRate();
            fillRate.Type = "type1";
            fillRate.CurFillRate = "50";
            fillRate.LimitFillRate = "40";
            fillRateModel.FillRateList.Add(fillRate);

            fillRate = new FillRate();
            fillRate.Type = "type2";
            fillRate.CurFillRate = "60";
            fillRate.LimitFillRate = "50";
            fillRateModel.FillRateList.Add(fillRate);

            fillRate = new FillRate();
            fillRate.Type = "type3";
            fillRate.CurFillRate = "70";
            fillRate.LimitFillRate = "50";
            fillRateModel.FillRateList.Add(fillRate);

            return fillRateModel;
        }
    }


    /// <summary>
    /// FillRate.xaml 的交互逻辑
    /// </summary>
    public partial class FillRateWindow : Window
    {
        public FillRateModel fillRateModel = new FillRateModel();
        public FillRateWindow()
        {
            InitializeComponent();
            fillRateModel = FillRateModel.TestData();
            this.DataContext = fillRateModel;
        }
    }
}
