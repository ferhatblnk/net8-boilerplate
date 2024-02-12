using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class LanguageKeyValueDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LanguageId { get; set; }
        public string MapKey { get; set; }
        public string Value { get; set; }
    }
}