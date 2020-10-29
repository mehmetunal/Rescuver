using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Rescuer.Core.Extensions
{
    public static class ObjectHelper
    {
        public static TResult Map<TResult, TEntity>(TEntity copyData) where TResult : new()
        {
            var entity = new TResult();

            if (copyData == null)
            {
                return entity;
            }

            foreach (var prop in copyData.GetType().GetProperties())
            {
                var value = prop.GetValue(copyData, null);
                if (value == null) continue;

                var isThere = entity.GetType().GetProperties().FirstOrDefault(x => x.Name == prop.Name);
                if (isThere == null) continue;
                if (prop.PropertyType.GenericTypeArguments.Length > 0)
                {
                    isThere.SetValue(entity, Convert.ChangeType(value, prop.PropertyType.GenericTypeArguments[0], CultureInfo.InvariantCulture));
                }
                else
                {
                    isThere.SetValue(entity, Convert.ChangeType(value, prop.PropertyType, CultureInfo.InvariantCulture));
                }

            }
            return entity;
        }
        public static IEnumerable<TResult> Map<TResult, TEntity>(IEnumerable<TEntity> copyData) where TResult : new()
        {
            if (copyData == null || copyData.Any())
            {
                return null;
            }
            return copyData.Select(Map<TResult, TEntity>).ToList();
        }
    }
}
