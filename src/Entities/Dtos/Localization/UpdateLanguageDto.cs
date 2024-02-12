using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class UpdateLanguageDto : IDto
    {
        public Guid LocalizationId { get; set; }
        public string Value { get; set;}
    }
}