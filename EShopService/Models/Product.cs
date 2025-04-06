namespace EShopService.Models
{
    public class Product:BaseModel
    {
      
        public string name { get; set; }

        public string ean { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; } = 0;
        public string sku { get; set; }
        public Category category { get; set; }
       

    }
}
