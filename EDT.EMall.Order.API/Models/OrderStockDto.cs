using System;

namespace EDT.EMall.Order.API.Models
{
    /// <summary>
    /// Order Stock DTO
    /// 假设这是一个订单库存DTO
    /// </summary>
    public class OrderStockDto
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
