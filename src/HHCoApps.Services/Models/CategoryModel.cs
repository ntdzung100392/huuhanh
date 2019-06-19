using System;

namespace HHCoApps.Services.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
