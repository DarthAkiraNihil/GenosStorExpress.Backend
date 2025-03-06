using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
    public class DPIMode {
	    public int Id { get; set; }
		[Required]
		public int DPI { get; set; }
        
        public List<Mouse> Mouses { get; set; }

        public DPIMode() {
            Mouses = new List<Mouse>();
        }
        
    }
}