using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ILocalizedPropertyService
    {
        IList<TLocalizedProperty> LocalizedProperties();
        void LocalizedPropertiesReset();

        string Localized<TEntity, TProp>(TEntity entity, Guid languageId, Expression<Func<TEntity, TProp>> keySelector) where TEntity : BaseEntity;
    }
}