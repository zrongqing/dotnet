using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RQ.UI.HWProcessBar
{
    public enum HWProgressBarMode
    {
        /// <summary>
        /// 默认模式:根据用户提供的值进行改变
        /// </summary>
        [Description("默认")]
        Normal,

        /// <summary>
        /// 循环,进度条一直循环
        /// </summary>
        [Description("循环")]
        Loop,

        /// <summary>
        /// 留备份
        /// </summary>
        [Description("其他")]
        Other,
    }
}
