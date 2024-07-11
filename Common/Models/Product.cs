namespace Common.Models
{
    /// <summary>
    /// Product 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product id
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public double Price { get; set; }
    }
}
