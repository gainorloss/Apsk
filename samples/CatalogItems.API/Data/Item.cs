namespace Projects.API.Data
{
    /// <summary>
    /// Item.
    /// </summary>

    public class Item
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets sale volumes.
        /// </summary>
        public int SaleVolumes { get; set; }

        /// <summary>
        /// Gets or sets browse times.
        /// </summary>
        public int BrowseTimes { get; set; }
    }
}
