using System.Text.Json;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

public class AnonymousItemWrapper: ItemWrapper {
    public IDictionary<string, dynamic> Characteristics { get; set; }

    public AnonymousItemWrapper() {
        Characteristics = new Dictionary<string, dynamic>();
    }
}