using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class MapKeyValue
    {
        public string MapKey { get; set; }
        public string Value { get; set; }
    }
    public class KeyCodeValue
    {
        public string Code { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Key
    {
        public string MapKey { get; set; }
    }

    public class ImportMapKeyExcelByLanguageDto : IDto
    {
        public Guid LanguageId { get; set; }
        public List<MapKeyValue> LocalizedMapList { get; set; }
    }

    public class ImportKeyExcelByLanguageDto : IDto
    {
        public Guid LanguageId { get; set; }
        public List<KeyCodeValue> LocalizedMapList { get; set; }
    }
}