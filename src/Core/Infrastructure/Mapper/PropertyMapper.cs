using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Core.Entities;

namespace Core.Infrastructure.Mapper
{
    public static class PropertyMapper
    {
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
        public sealed class IgnoreMappingAttribute : Attribute { public IgnoreMappingAttribute() { } }

        private static readonly Dictionary<Type, IList<PropertyInfo>> typeDictionary = [];

        private static IList<PropertyInfo> GetPropertiesFor(Type type)
        {
            if (!typeDictionary.ContainsKey(type))
            {
                try
                {
                    typeDictionary.Add(type, type.GetProperties().ToList());
                }
                catch { }
            }

            return typeDictionary[type];
        }
        private static IList<PropertyInfo> GetPropertiesFor<T>()
        {
            return GetPropertiesFor(typeof(T));
        }
        private static IList<PropertyInfo> GetPropertiesForEntity(IEntity entity)
        {
            return GetPropertiesFor(entity.GetType());
        }
        private static IList<PropertyInfo> GetPropertiesForDto(IDto model)
        {
            return GetPropertiesFor(model.GetType());
        }

        public static TModel ToDto<TModel>(this IEntity entity, IDto model = null, bool excludeNull = false) where TModel : IDto
        {
            return CreateDtoFromEntity<TModel>(entity, model, excludeNull);
        }
        public static TEntity ToEntity<TEntity>(this IDto model, IEntity entity = null, bool excludeNull = false) where TEntity : IEntity
        {
            return CreateEntityFromDto<TEntity>(model, entity, excludeNull);
        }
        public static TEntity Clone<TEntity>(this IEntity entity, IEntity model = null, bool excludeNull = false) where TEntity : IEntity
        {
            return CreateEntityFromEntity<TEntity>(entity, model, excludeNull, excludeId: true);
        }
        public static TModel Clone<TModel>(this IDto model, IDto entity = null, bool excludeNull = false) where TModel : IDto
        {
            return CreateDtoFromDto<TModel>(model, entity, excludeNull, excludeId: true);
        }

        private static TModel CreateDtoFromEntity<TModel>(IEntity entity, IDto model = null, bool excludeNull = false) where TModel : IDto
        {
            TModel item = (TModel)Activator.CreateInstance(typeof(TModel));

            IList<PropertyInfo> sourceProperties = GetPropertiesForEntity(entity);
            IList<PropertyInfo> targetProperties = GetPropertiesFor<TModel>();

            foreach (var property in targetProperties)
            {
                var sourceProperty = sourceProperties
                                        .Where(x => x.Name == property.Name)
                                        .FirstOrDefault();

                if (sourceProperty == null)
                    if (model == null)
                        continue;
                    else
                    {
                        sourceProperty = targetProperties
                                            .Where(x => x.Name == property.Name)
                                            .FirstOrDefault();

                        var baseValue = sourceProperty.GetValue(model);

                        if (baseValue != null || !excludeNull)
                            SetPropertyValue(item, property, baseValue);

                        continue;
                    }

                var sourceValue = sourceProperty.GetValue(entity);

                if (sourceValue != null || !excludeNull)
                    SetPropertyValue(item, property, sourceValue);
            }

            return item;
        }
        private static TEntity CreateEntityFromDto<TEntity>(IDto model, IEntity entity = null, bool excludeNull = false) where TEntity : IEntity
        {
            TEntity item = (TEntity)Activator.CreateInstance(typeof(TEntity));

            IList<PropertyInfo> sourceProperties = GetPropertiesForDto(model);
            IList<PropertyInfo> targetProperties = GetPropertiesFor<TEntity>();

            foreach (var property in targetProperties)
            {
                var sourceProperty = sourceProperties
                                        .Where(x => x.Name == property.Name)
                                        .FirstOrDefault();

                if (sourceProperty == null)
                    if (entity == null)
                        continue;
                    else
                    {
                        sourceProperty = targetProperties
                                            .Where(x => x.Name == property.Name)
                                            .FirstOrDefault();

                        var baseValue = sourceProperty.GetValue(entity);

                        if (baseValue != null || !excludeNull)
                            SetPropertyValue(item, property, baseValue);

                        continue;
                    }

                var sourceValue = sourceProperty.GetValue(model);

                if (sourceValue != null || !excludeNull)
                    SetPropertyValue(item, property, sourceValue);
            }

            return item;
        }

        private static TModel CreateDtoFromDto<TModel>(IDto source, IDto model = null, bool excludeNull = false, bool excludeId = false) where TModel : IDto
        {
            TModel item = (TModel)Activator.CreateInstance(typeof(TModel));

            IList<PropertyInfo> sourceProperties = GetPropertiesForDto(source);
            IList<PropertyInfo> targetProperties = GetPropertiesFor<TModel>();

            foreach (var property in targetProperties)
            {
                if (excludeId && property.Name.Equals("Id"))
                    continue;

                var sourceProperty = sourceProperties
                                        .Where(x => x.Name == property.Name)
                                        .FirstOrDefault();

                if (sourceProperty == null)
                    if (model == null)
                        continue;
                    else
                    {
                        sourceProperty = targetProperties
                                            .Where(x => x.Name == property.Name)
                                            .FirstOrDefault();

                        var baseValue = sourceProperty.GetValue(model);

                        if (baseValue != null || !excludeNull)
                            SetPropertyValue(item, property, baseValue);

                        continue;
                    }

                var sourceValue = sourceProperty.GetValue(source);

                if (sourceValue != null || !excludeNull)
                    SetPropertyValue(item, property, sourceValue);
            }

            return item;
        }
        private static TEntity CreateEntityFromEntity<TEntity>(IEntity source, IEntity model = null, bool excludeNull = false, bool excludeId = false) where TEntity : IEntity
        {
            TEntity item = (TEntity)Activator.CreateInstance(typeof(TEntity));

            IList<PropertyInfo> sourceProperties = GetPropertiesForEntity(source);
            IList<PropertyInfo> targetProperties = GetPropertiesFor<TEntity>();

            foreach (var property in targetProperties)
            {
                if (excludeId && property.Name.Equals("Id"))
                    continue;

                var sourceProperty = sourceProperties
                                        .Where(x => x.Name == property.Name)
                                        .FirstOrDefault();

                if (sourceProperty == null)
                    if (model == null)
                        continue;
                    else
                    {
                        sourceProperty = targetProperties
                                            .Where(x => x.Name == property.Name)
                                            .FirstOrDefault();

                        var baseValue = sourceProperty.GetValue(model);

                        if (baseValue != null || !excludeNull)
                            SetPropertyValue(item, property, baseValue);

                        continue;
                    }

                var sourceValue = sourceProperty.GetValue(source);

                if (sourceValue != null || !excludeNull)
                    SetPropertyValue(item, property, sourceValue);
            }

            return item;
        }

        private static void SetPropertyValue(object item, PropertyInfo property, object sourceValue)
        {
            var atr = property.GetCustomAttribute(typeof(IgnoreMappingAttribute));

            if (atr == null && sourceValue != null)
            {
                try
                {
                    if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        DateTime.TryParse(sourceValue.ToString(), out DateTime date);

                        if (sourceValue.ToString() != "")
                            property.SetValue(item, date, null);
                        else
                            property.SetValue(item, "", null);
                    }
                    else if (sourceValue.GetType() == typeof(DBNull))
                        property.SetValue(item, "", null);
                    else if (property.PropertyType == typeof(string))
                        property.SetValue(item, sourceValue.ToString(), null);
                    else
                        property.SetValue(item, sourceValue, null);
                }
                catch { }
            }
        }
    }
}
