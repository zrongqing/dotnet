using System;
using System.Linq.Expressions;

namespace RQ.HWComponentModel
{
    public static class NotifyPropertyBaseEx
    {
        public static void SetProperty<T, U>(this T tvm, Expression<Func<T, U>> expre) where T : NotifyPropertyBase, new()
        {
            string _pro = NotifyPropertyBaseEx.GetPropertyName(expre);
            tvm.OnPropertyChanged(_pro);
        }

        /// <summary>
        /// 返回属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyName<T, U>(Expression<Func<T, U>> expr)
        {
            string _propertyName = "";
            if (expr.Body is MemberExpression)
            {
                _propertyName = (expr.Body as MemberExpression).Member.Name;
            }
            else if (expr.Body is UnaryExpression)
            {
                _propertyName = ((expr.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            }
            return _propertyName;
        }
    }
}
