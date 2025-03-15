using System.Text.Json;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class ComputerCaseWrapper: ComputerComponentWrapper {
    public string Typesize { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public IList<string> SupportedMotherboardFormFactors { get; set; }
    public bool HasARGBLighting { get; set; }
    public byte DrivesSlotsCount { get; set; }

    public ComputerCaseWrapper() {
        SupportedMotherboardFormFactors = new List<string>();
    }
}