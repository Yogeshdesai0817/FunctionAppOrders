namespace Common.Models
{
    /// <summary>
    /// Sales order 
    /// </summary>
    public class SalesOrder
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Order Amount
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Product Item id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Product Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product Details
        /// </summary>
        public Product ProductDetails { get; set; }
    }
}
