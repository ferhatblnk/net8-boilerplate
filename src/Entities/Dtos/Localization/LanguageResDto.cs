using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class LanguageResDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public string FlagUrl { get; set; }
    }
}