using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Lagarsystem.Models
{
    public static class LinqToEntityHelper
    {
        public static string Name<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector)
        {
            MemberExpression body = (MemberExpression)propertySelector.Body;
            return body.Member.Name;
        }

        public static Expression<Func<TItem, bool>> PropertyStartsWith<TItem>(PropertyInfo propertyInfo, string value)
        {
            var param = Expression.Parameter(typeof(TItem));

            var m = Expression.MakeMemberAccess(param, propertyInfo);
            var c = Expression.Constant(value, typeof(string));
            var mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            var body = Expression.Call(m, mi, c);

            return Expression.Lambda<Func<TItem, bool>>(body, param);
        }

    }
}