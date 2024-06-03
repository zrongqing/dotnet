using System;
using System.Collections.Generic;
using System.Text;

namespace ZRQ.Utils.ClassTemplate
{
    /// <summary>
    /// 单例模板
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    public class Singleton<T> : IDisposable where T : class, new()
    {
        private static T? _instance = null;

        public static T Ins
        {
            get { return _instance ??= new T(); }
        }

        public static T Instance()
        {
            return _instance ??= new T();
        }

        public static void Reset()
        {
            if (null != _instance)
            {
                _instance = new T();
            }
        }

        public void Dispose()
        {
            DisposeImp();
        }

        protected virtual void DisposeImp()
        {
        }
    }
}
