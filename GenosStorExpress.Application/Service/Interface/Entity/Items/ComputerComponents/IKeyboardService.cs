using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    public interface IKeyboardService:
        IStandardService<Keyboard>,
        ISupportsFilter<Keyboard> {
		
    }
}