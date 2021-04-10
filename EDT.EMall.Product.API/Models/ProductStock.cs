using System;

namespace EDT.EMall.Product.API.Models
{
    /// <summary>
    /// Product Stock Event Command
    /// 假设这是一个订单扣减库存的事件的数据对象
    /// </summary>
    public class ProductStock
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Stock { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
