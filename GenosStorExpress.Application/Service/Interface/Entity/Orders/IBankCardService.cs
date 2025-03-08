using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.Orders;
using GenosStorExpress.Domain.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IBankCardService: IStandardService<BankCardWrapper> {
        
    }
}