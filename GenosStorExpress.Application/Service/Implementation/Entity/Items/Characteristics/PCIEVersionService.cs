using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics;

public class PCIEVersionService: IPCIEVersionService {
    private readonly IGenosStorExpressRepositories _repositories;
    private readonly IPCIEVersionRepository _pcieVersions;

    public PCIEVersionService(IGenosStorExpressRepositories repositories) {
        _repositories = repositories;
        _pcieVersions = _repositories.Items.Characteristics.PCIEVersions;
    }

    public void Create(string item) {
        var created = new PCIEVersion { Name = item };
        _pcieVersions.Create(created);
    }

    public string Get(int id) {
        return _pcieVersions.Get(id).Name;
    }

    public List<string> List() {
        return _pcieVersions.List().Select(c => c.Name).ToList();
    }

    public void Update(int id, string item) {
        PCIEVersion obj = _pcieVersions.Get(id);
        obj.Name = item;
        _pcieVersions.Update(obj);
    }

    public void Delete(int id) {
        _pcieVersions.Delete(id);
    }

    public int Save() {
        return _repositories.Save();
    }

    public bool BelongsToEnum(string value) {
        return _pcieVersions.List().Exists(c => c.Name == value);
    }

    public PCIEVersion GetEntityFromString(string value) {
        return _pcieVersions.List().FirstOrDefault(c => c.Name == value, null);
    }
}