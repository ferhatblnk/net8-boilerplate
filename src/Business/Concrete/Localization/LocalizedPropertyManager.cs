using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Business.Concrete
{
    public class LocalizedPropertyManager : BaseManager, ILocalizedPropertyService
    {
        [YearlyCache]
        public IList<TLocalizedProperty> LocalizedProperties()
        {
            return _localizedPropertyDal.GetList().ToList();
        }
        [CacheRemove("ILocalizedPropertyService.LocalizedProperties")]
        public void LocalizedPropertiesReset() { }

        public string Localized<TEntity, TProp>(TEntity entity, Guid languageId, Expression<Func<TEntity, TProp>> keySelector) where TEntity : BaseEntity
        {
            if (entity == null)
                return string.Empty;

            if (keySelector.Body is not MemberExpression member)
                return string.Empty;

            if (member.Member is not PropertyInfo propInfo)
                return string.Empty;

            var id = entity.Id;
            var table = entity.GetType().Name;
            var column = propInfo.Name;

            var localizedProperty = LP.LocalizedProperties() //calling by service for interceptor
                                        .Where(x => x.LanguageId.Equals(languageId) && x.TableName.Equals(table) &&
                                                    x.TableField.Equals(column) && x.TableId.Equals(id) && !x.Deleted)
                                        .FirstOrDefault();

            if (localizedProperty == null || string.IsNullOrEmpty(localizedProperty.Value))
            {
                var localizer = keySelector.Compile();
                return localizer(entity)?.ToString() ?? "";
            }

            return localizedProperty.Value;
        }
    }
}