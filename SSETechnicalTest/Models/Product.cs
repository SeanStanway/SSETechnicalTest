namespace SSETechnicalTest.Models
{
    public class Product
    {
        public int SKU { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
