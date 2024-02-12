using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class LanguagesResDto : IDto
    {
        public int Id { get; set; }

        public Guid RowGuid { get; set; }

        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string FlagUrl { get; set; }
    }
}