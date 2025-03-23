using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Microsoft.AspNetCore.Identity;

namespace GenosStorExpress.Infrastructure.Context
{
    public static class GenosStorExpressDatabaseInitializer {

	    private static readonly IList<string> BankSystems = new List<string> {
		    "Visa",
		    "MasterCard",
		    "UnionPay",
		    "JBC",
		    "Mir"
	    };

	    private static readonly IList<string> Certificates80Plus = new List<string> {
		    "Standard",
		    "Bronze",
		    "Silver",
		    "Gold",
		    "Platinum",
		    "Titanium"
	    };

	    private static readonly IList<string> ComputerCaseTypesizes = new List<string> {
		    "MiniTower",
		    "MidTower",
		    "BigTower"
	    };

	    private static readonly IList<string> CoolerMaterials = new List<string> {
		    "Cooper",
		    "Aluminium"
	    };

	    private static readonly IList<string> CPUSockets = new List<string> {
		    "LGA1700",
		    "LGA1200",
		    "Socket4",
	    };

	    private static readonly IList<string> ItemTypes = new List<string> {
		    "cpu",
		    "ram",
		    "motherboard",
		    "graphics_card",
		    "power_supply",
		    "hdd",
		    "sata_ssd",
		    "nvme_ssd",
		    "display",
		    "cpu_cooler",
		    "computer_case",
		    "keyboard",
		    "mouse",
		    "prepared_assembly"
	    };

	    private static readonly IList<string> KeyboardType = new List<string> {
		    "Optical",
		    "Mechanic",
		    "Membrane"
	    };

	    private static readonly IList<string> KeyboardTypesizes = new List<string> {
		    "TKL",
		    "Percent60",
		    "Full",
		    "FullPlusNumpad"
	    };

	    private static readonly IList<string> MatrixTypes = new List<string> {
		    "TN",
		    "IPS",
		    "VA",
		    "OLED",
	    };

	    private static readonly IList<string> OrderStatuses = new List<string> {
		    "Created",
		    "Confirmed",
		    "AwaitsPayment",
		    "Paid",
		    "Processing",
		    "Delivering",
		    "Received",
		    "Canceled"
	    };

	    private static readonly IList<string> PCIEVersions = new List<string> {
		    "4.0",
		    "3.0"
	    };

	    private static readonly IList<string> RAMTypes = new List<string> {
		    "DDR4",
		    "DDR5",
		    "DDR3"
	    };

	    private static readonly IList<string> SimpleComputerComponentTypes = new List<string> {
		    "motherboard_chipset",
		    "audio_chipset",
		    "network_adapter",
		    "ssd_controller"
	    };

	    private static readonly IList<string> Underlights = new List<string> {
		    "LED",
		    "CCFL",
		    "RGB"
	    };

	    private static readonly IList<string> VesaSizes = new List<string> {
		    "Vesa100x100",
		    "Vesa120x120"
	    };
		    
	    private static readonly IList<string> VideoPorts = new List<string> {
			    "HDMI",
			    "DisplayPort",
			    "VGA",
			    "DVI"
		    };
		    
	    private static readonly IList<string> MotherboardFormFactors = new List<string> {
		    "mini-ATX",
		    "ATX",
		    "micro-ATX",
		    "mini-ITX",
		};
	    
	    public static async Task Initialize(
		    GenosStorExpressDatabaseContext context,
		    UserManager<User> userManager,
		    RoleManager<IdentityRole> roleManager
		) {

			context.Database.EnsureCreated();
			
			await _initializeUsers(userManager, roleManager);
		    await _initializeBankSystems(context);
	    	await _initializeCertificates80Plus(context);
	    	await _initializeComputerCaseTypesizes(context);
	    	await _initializeCoolerMaterials(context);
	    	await _initializeCPUSockets(context);
	    	await _initializeItemTypes(context);
	    	await _initializeKeyboardType(context);
	    	await _initializeKeyboardTypesizes(context);
	    	await _initializeMatrixTypes(context);
	    	await _initializeOrderStatuses(context);
	    	await _initializePCIEVersions(context);
	    	await _initializeRAMTypes(context);
	    	await _initializeSimpleComputerComponentTypes(context);
	    	await _initializeUnderlights(context);
	    	await _initializeVesaSizes(context);
	    	await _initializeVideoPorts(context);
	    	await _initializeMotherboardFormFactors(context);

            await context.SaveChangesAsync();
	    }
	    
	    private static async Task _initializeUsers(
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager) {
		    if (!roleManager.Roles.Any()) {
			    await roleManager.CreateAsync(new IdentityRole("administrator"));
			    await roleManager.CreateAsync(new IdentityRole("individual_entity"));
			    await roleManager.CreateAsync(new IdentityRole("legal_entity"));
		    }

		    if (!userManager.Users.Any()) {

			    var admin = new Administrator {
				    UserName = "akira@dancorp.org",
				    Email = "akira@dancorp.org",
			    };

			    var adminCreationResult = await userManager.CreateAsync(admin, "String6!");
			    if (adminCreationResult.Succeeded) {
				    await userManager.AddToRoleAsync(admin, "administrator");
			    }

			    var individualEntity = new IndividualEntity {
				    UserName = "amogus@amogus.net",
				    Email = "amogus@amogus.net",
				    Name = "Amogus",
				    Surname = "Amogusov",
				    PhoneNumber = "+1234567890"
			    };

			    var individualEntityCreationResult = await userManager.CreateAsync(individualEntity, "String6!");
			    if (individualEntityCreationResult.Succeeded) {
				    await userManager.AddToRoleAsync(individualEntity, "individual_entity");
			    }

			    var legalEntity = new LegalEntity {
				    UserName= "jensen_huang@nvidia.com",
				    Email = "jensen_huang@nvidia.com",
				    INN = 102420484096,
				    KPP = 128256512,
				    PhysicalAddress = "USA",
				    LegalAddress = "USA",
				    IsVerified = true
			    };

			    var legalEntityCreationResult = await userManager.CreateAsync(legalEntity, "String6!");
			    if (legalEntityCreationResult.Succeeded) {
				    await userManager.AddToRoleAsync(legalEntity, "legal_entity");
			    }
		    }
	    }

	    private static async Task _initializeBankSystems(GenosStorExpressDatabaseContext context) {
		    foreach (var bankSystem in BankSystems) {
			    if (context.BankSystems.FirstOrDefault(i => i.Name == bankSystem) == null) {
				    await context.BankSystems.AddAsync(new BankSystem { Name = bankSystem });
			    }
		    }
	    }
	    
	    private static async Task _initializeCertificates80Plus(GenosStorExpressDatabaseContext context) {
		    foreach (var certificate in Certificates80Plus) {
			    if (context.Certificates80Plus.FirstOrDefault(i => i.Name == certificate) == null) {
				    await context.Certificates80Plus.AddAsync(new Certificate80Plus { Name = certificate });
			    }
		    }
	    }
	    
	    private static async Task _initializeComputerCaseTypesizes(GenosStorExpressDatabaseContext context) {
		    foreach (var typesize in ComputerCaseTypesizes) {
			    if (context.ComputerCaseTypesizes.FirstOrDefault(i => i.Name == typesize) == null) {
				    await context.ComputerCaseTypesizes.AddAsync(new ComputerCaseTypesize { Name = typesize });
			    }
		    }
	    }
	    
	    private static async Task _initializeCoolerMaterials(GenosStorExpressDatabaseContext context) {
		    foreach (var material in CoolerMaterials) {
			    if (context.CoolerMaterials.FirstOrDefault(i => i.Name == material) == null) {
				    await context.CoolerMaterials.AddAsync(new CoolerMaterial { Name = material });
			    }
		    }
	    }
	    
	    private static async Task _initializeCPUSockets(GenosStorExpressDatabaseContext context) {
		    foreach (var socket in CPUSockets) {
			    if (context.CPUSockets.FirstOrDefault(i => i.Name == socket) == null) {
				    await context.CPUSockets.AddAsync(new CPUSocket { Name = socket });
			    }
		    }
	    }
	    
	    private static async Task _initializeItemTypes(GenosStorExpressDatabaseContext context) {
		    foreach (var type in ItemTypes) {
			    if (context.ItemTypes.FirstOrDefault(i => i.Name == type) == null) {
				    await context.ItemTypes.AddAsync(new ItemType { Name = type });
			    }
		    }
	    }
	    
	    private static async Task _initializeKeyboardType(GenosStorExpressDatabaseContext context) {
		    foreach (var type in KeyboardType) {
			    if (context.KeyboardTypes.FirstOrDefault(i => i.Name == type) == null) {
				    await context.KeyboardTypes.AddAsync(new KeyboardType { Name = type });
			    }
		    }
	    }
	    
	    private static async Task _initializeKeyboardTypesizes(GenosStorExpressDatabaseContext context) {
		    foreach (var typesize in KeyboardTypesizes) {
			    if (context.KeyboardTypesizes.FirstOrDefault(i => i.Name == typesize) == null) {
				    await context.KeyboardTypesizes.AddAsync(new KeyboardTypesize { Name = typesize });
			    }
		    }
	    }
	    
	    private static async Task _initializeMatrixTypes(GenosStorExpressDatabaseContext context) {
		    foreach (var type in MatrixTypes) {
			    if (context.MatrixTypes.FirstOrDefault(i => i.Name == type) == null) {
				    await context.MatrixTypes.AddAsync(new MatrixType { Name = type });
			    }
		    }
	    }
	    
	    private static async Task _initializeOrderStatuses(GenosStorExpressDatabaseContext context) {
		    foreach (var status in OrderStatuses) {
			    if (context.OrderStatuses.FirstOrDefault(i => i.Name == status) == null) {
				    await context.OrderStatuses.AddAsync(new OrderStatus { Name = status });
			    }
		    }
	    }
	    private static async Task _initializePCIEVersions(GenosStorExpressDatabaseContext context) {
		    foreach (var version in PCIEVersions) {
			    if (context.PCIEVersions.FirstOrDefault(i => i.Name == version) == null) {
				    await context.PCIEVersions.AddAsync(new PCIEVersion { Name = version });
			    }
		    }
	    }
	    
	    private static async Task _initializeRAMTypes(GenosStorExpressDatabaseContext context) {
		    foreach (var type in RAMTypes) {
			    if (context.RAMTypes.FirstOrDefault(i => i.Name == type) == null) {
				    await context.RAMTypes.AddAsync(new RAMType { Name = type });
			    }
		    }
	    }
	    
	    private static async Task _initializeSimpleComputerComponentTypes(GenosStorExpressDatabaseContext context) {
		    foreach (var type in SimpleComputerComponentTypes) {
			    if (context.SimpleComputerComponentTypes.FirstOrDefault(i => i.Name == type) == null) {
				    await context.SimpleComputerComponentTypes.AddAsync(new SimpleComputerComponentType { Name = type });
			    }
		    }
	    }
	    
	    private static async Task _initializeUnderlights(GenosStorExpressDatabaseContext context) {
		    foreach (var underlights in Underlights) {
			    if (context.Underlights.FirstOrDefault(i => i.Name == underlights) == null) {
				    await context.Underlights.AddAsync(new Underlight { Name = underlights });
			    }
		    }
	    }
	    
	    private static async Task _initializeVesaSizes(GenosStorExpressDatabaseContext context) {
		    foreach (var size in VesaSizes) {
			    if (context.VesaSizes.FirstOrDefault(i => i.Name == size) == null) {
				    await context.VesaSizes.AddAsync(new VesaSize { Name = size });
			    }
		    }
	    }
	    
	    private static async Task _initializeVideoPorts(GenosStorExpressDatabaseContext context) {
		    foreach (var port in VideoPorts) {
			    if (context.VideoPorts.FirstOrDefault(i => i.Name == port) == null) {
				    await context.VideoPorts.AddAsync(new VideoPort { Name = port });
			    }
		    }
	    }
	    
	    private static async Task _initializeMotherboardFormFactors(GenosStorExpressDatabaseContext context) {
		    foreach (var formFactor in MotherboardFormFactors) {
			    if (context.MotherboardFormFactors.FirstOrDefault(i => i.Name == formFactor) == null) {
				    await context.MotherboardFormFactors.AddAsync(new MotherboardFormFactor { Name = formFactor });
			    }
		    }
	    }
	    
    }
    
}
