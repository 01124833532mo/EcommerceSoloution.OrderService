using System;
using System.Collections.Generic;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;

public record OrderAddRequest(Guid UserID, DateTime OrderDate, List<OrderItemAddRequest> OrderItems)
{
  public OrderAddRequest(): this(Guid.Empty, default, new List<OrderItemAddRequest>())
  {
  }
}