﻿namespace EShop.Domain.Models
{
    public class BaseModel
    {
        public int id { get; set; }
        public Boolean deleted { get; set; }
        public DateTime created_at { get; set; }
        public Guid created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public Guid updated_by { get; set; }
    }
}
