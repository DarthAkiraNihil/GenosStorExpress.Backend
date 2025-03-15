using GenosStorExpress.Domain.Entity.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    public interface IDPIModeService {
        List<int> List();
        DPIMode? GetByValue(int dpi);
    }
}