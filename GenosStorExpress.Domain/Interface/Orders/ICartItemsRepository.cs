﻿using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Base;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Interface.Orders {
    public interface ICartItemsRepository: IRepository<CartItem>, ISupportsDeleteRaw<CartItem> {
        
    }
}