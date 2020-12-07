namespace BookStore.Data.Mappers
{
    public static class MapperInventory
    {
        /// <summary>
        /// Turn a entity inventory into a model stock
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static Domain.Models.Stock Map(Entities.InventoryEntity inventory)
        {
            return new Domain.Models.Stock
            {
                Book = MapperBook.Map(inventory.BookIsbnNavigation),
                Quantity = (int)inventory.Quantity
            };
        }

        /// <summary>
        /// Turn a model stock into an entity inventory
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public static Entities.InventoryEntity Map(Domain.Models.Stock stock)
        {
            return new Entities.InventoryEntity
            {
                BookIsbnNavigation = MapperBook.Map(stock.Book),
                Quantity = stock.Quantity,
                BookIsbn = MapperBook.Map(stock.Book).Isbn
            };
        }
    }
}
