using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class LocalizationSearchDto : IDto
    {
        public Guid LanguageId { get; set; }
        public string Key { get; set;}
        public string Value {get; set;}
    }
}