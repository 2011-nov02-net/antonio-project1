﻿namespace BookStore.Data.Mappers
{
    public static class MapperOrderLine
    {
        /// <summary>
        /// Turn an entity orderline into a model orderline
        /// </summary>
        /// <param name="orderline"></param>
        /// <returns></returns>
        public static Domain.Models.OrderLine Map(Entities.OrderlineEntity orderline)
        {
            return new Domain.Models.OrderLine
            {
                BookISBN = orderline.BookIsbn,
                Quantity = orderline.Quantity,
                LineCost = orderline.Total
            };
        }

        /// <summary>
        /// turn a model orderline into an entity orderline
        /// </summary>
        /// <param name="orderline"></param>
        /// <returns></returns>
        public static Entities.OrderlineEntity Map(Domain.Models.OrderLine orderline)
        {
            return new Entities.OrderlineEntity
            {
                BookIsbn = orderline.BookISBN,
                Quantity = orderline.Quantity,
                Total = orderline.LineCost
            };
        }
    }
}
