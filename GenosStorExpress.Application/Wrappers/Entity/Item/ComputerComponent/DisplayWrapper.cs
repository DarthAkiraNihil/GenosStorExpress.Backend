using GenosStorExpress.Application.Wrappers.Entity.Item.Characteristic;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

public class DisplayWrapper: ComputerComponentWrapper {
    public int MaxUpdateFrequency { get; set; }
    public double ScreenDiagonal { get; set; }

    public DefinitionWrapper Definition { get; set; }
    public string MatrixType { get; set; }
    public string Underlight { get; set; }
    public string VesaSize { get; set; }
}