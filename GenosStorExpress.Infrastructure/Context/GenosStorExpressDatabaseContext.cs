using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


//using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Context{
	public class GenosStorExpressDatabaseContext : IdentityDbContext<User> {
		public GenosStorExpressDatabaseContext(DbContextOptions<GenosStorExpressDatabaseContext> options) : base(options) { }

		// enum
		public DbSet<ItemType> ItemTypes { get; set; }
		public DbSet<Certificate80Plus> Certificates80Plus { get; set; }
		public DbSet<ComputerCaseTypesize> ComputerCaseTypesizes { get; set; }
		public DbSet<CoolerMaterial> CoolerMaterials { get; set; }
		public DbSet<CPUSocket> CPUSockets { get; set; }
		public DbSet<KeyboardTypesize> KeyboardTypesizes { get; set; }
		public DbSet<KeyboardType> KeyboardTypes { get; set; }
		public DbSet<MatrixType> MatrixTypes { get; set; }
		public DbSet<MotherboardFormFactor> MotherboardFormFactors { get; set; }
		public DbSet<PCIEVersion> PCIEVersions { get; set; }
		public DbSet<RAMType> RAMTypes { get; set; }
		public DbSet<Underlight> Underlights { get; set; }
		public DbSet<VesaSize> VesaSizes { get; set; }
		public DbSet<VideoPort> VideoPorts { get; set; }
		
		public DbSet<SimpleComputerComponentType> SimpleComputerComponentTypes { get; set; }
		
		public DbSet<BankSystem> BankSystems { get; set; }
		
		// char class

		public DbSet<Definition> Definitions { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<DPIMode> DPIModes { get; set; }

		// computer components

		public DbSet<CPU> CPUs { get; set; }
		public DbSet<RAM> RAMs { get; set; }
		public DbSet<Motherboard> Motherboards { get; set; }
		public DbSet<GraphicsCard> GraphicsCards { get; set; }
		public DbSet<PowerSupply> PowerSupplies { get; set; }
		public DbSet<SataSSD> SataSSDs { get; set; }
		public DbSet<NVMeSSD> NVMeSSDs { get; set; }
		public DbSet<HDD> HDDs { get; set; }
		public DbSet<DiskDrive> DiskDrives { get; set; }
		public DbSet<Display> Displays { get; set; }
		public DbSet<ComputerCase> ComputerCases { get; set; }
		public DbSet<Keyboard> Keyboards { get; set; }
		public DbSet<Mouse> Mouses { get; set; }
		public DbSet<CPUCooler> CPUCoolers { get; set; }

		public DbSet<PreparedAssembly> PreparedAssemblies { get; set; }
		public DbSet<Item> Items { get; set; }

		// simple computer components

		public DbSet<MotherboardChipset> MotherboardChipsets { get; set; }
		public DbSet<AudioChipset> AudioChipsets { get; set; }
		public DbSet<NetworkAdapter> NetworkAdapters { get; set; }
		public DbSet<SSDController> SSDControllers { get; set; }
		public DbSet<CPUCore> CPUCores { get; set; }
		public DbSet<GPU> GPUs { get; set; }

		// user

		public new DbSet<User> Users { get; set; }
		public DbSet<Administrator> Administrators { get; set; }
		public DbSet<IndividualEntity> IndividualEntities { get; set; }
		public DbSet<LegalEntity> LegalEntities { get; set; }

		// order

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItems> OrderedItems { get; set; }
		public DbSet<ActiveDiscount> ActiveDiscounts { get; set; }
		public DbSet<BankCard> BankCards { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<OrderStatus> OrderStatuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("public");
			
			//modelBuilder.Entity<Named>().HasRequired(n => n.Name);
			//modelBuilder.Entity<WithModel>().HasRequired(w => w.Model);

			var itemEntity = modelBuilder.Entity<Item>();

			itemEntity.Navigation(i => i.ActiveDiscount).AutoInclude().IsRequired(false);
			itemEntity.Navigation(i => i.ItemType).AutoInclude().IsRequired();
			itemEntity.HasMany(i => i.Reviews).WithOne(i => i.Item);
			
			var preparedAssemblyEntity = modelBuilder.Entity<PreparedAssembly>();

			preparedAssemblyEntity.Navigation(a => a.CPU).AutoInclude().IsRequired();
			preparedAssemblyEntity.Navigation(a => a.Motherboard).AutoInclude().IsRequired();
			preparedAssemblyEntity.Navigation(a => a.GraphicsCard).AutoInclude().IsRequired();
			preparedAssemblyEntity.Navigation(a => a.PowerSupply).AutoInclude().IsRequired();
			preparedAssemblyEntity.Navigation(a => a.ComputerCase).AutoInclude().IsRequired();
			preparedAssemblyEntity.Navigation(a => a.CPUCooler).AutoInclude().IsRequired();
			
			preparedAssemblyEntity.Navigation(a => a.Display).AutoInclude().IsRequired(false);
			preparedAssemblyEntity.Navigation(a => a.Keyboard).AutoInclude().IsRequired(false);
			preparedAssemblyEntity.Navigation(a => a.Mouse).AutoInclude().IsRequired(false);

			preparedAssemblyEntity
				.HasMany(a => a.RAM)
				.WithMany(r => r.PreparedAssemblies);

			preparedAssemblyEntity
				.HasMany(a => a.Disks)
				.WithMany(d => d.PreparedAssemblies);



			modelBuilder.Entity<ComputerComponent>().Navigation(c => c.Vendor).AutoInclude().IsRequired();
			
			
			
			var computerCaseEntity = modelBuilder.Entity<ComputerCase>();
			
			computerCaseEntity.Navigation(cc => cc.Typesize).AutoInclude().IsRequired();
			computerCaseEntity.HasMany(cc => cc.SupportedMotherboardFormFactors)
			                  .WithMany(mbff => mbff.ComputerCases);
			
			
			
			var cpuEntity = modelBuilder.Entity<CPU>();
			
			cpuEntity.Navigation(c => c.Core).AutoInclude().IsRequired();
			cpuEntity.Navigation(c => c.Socket).AutoInclude().IsRequired();
			cpuEntity.HasMany(c => c.SupportedRamType)
			         .WithMany(r => r.CPUs);
			
			
			
			var cpuCoolerEntity = modelBuilder.Entity<CPUCooler>();

			cpuCoolerEntity.Navigation(c => c.FoundationMaterial).AutoInclude().IsRequired();
			cpuCoolerEntity.Navigation(c => c.RadiatorMaterial).AutoInclude().IsRequired();
			
			
			
			var displayEntity = modelBuilder.Entity<Display>();

			displayEntity.Navigation(d => d.Definition).AutoInclude().IsRequired();
			displayEntity.Navigation(d => d.MatrixType).AutoInclude().IsRequired();
			displayEntity.Navigation(d => d.Underlight).AutoInclude().IsRequired();
			displayEntity.Navigation(d => d.VesaSize).AutoInclude().IsRequired();
			
			
			
			var graphicsCardEntity = modelBuilder.Entity<GraphicsCard>();

			graphicsCardEntity.Navigation(g => g.GPU).AutoInclude().IsRequired();
			graphicsCardEntity.HasMany(g => g.VideoPorts)
			                  .WithMany(v => v.GraphicsCards);
			
			
			
			var keyboardEntity = modelBuilder.Entity<Keyboard>();
			
			keyboardEntity.Navigation(k => k.KeyboardType).AutoInclude().IsRequired();
			keyboardEntity.Navigation(k => k.Typesize).AutoInclude().IsRequired();
			
			
			
			var motherboardEntity = modelBuilder.Entity<Motherboard>();

			motherboardEntity.Navigation(m => m.FormFactor).AutoInclude().IsRequired();
			motherboardEntity.Navigation(m => m.CPUSocket).AutoInclude().IsRequired();
			motherboardEntity.Navigation(m => m.MotherboardChipset).AutoInclude().IsRequired();
			
			motherboardEntity.HasMany(m=>m.SupportedCPUCores)
			                 .WithMany(c => c.Motherboards);
			motherboardEntity.HasMany(m=>m.SupportedRAMTypes)
			                 .WithMany(r => r.Motherboards);
			motherboardEntity.HasMany(m=>m.VideoPorts)
			                 .WithMany(v => v.Motherboards);

			motherboardEntity.Navigation(m => m.PCIEVersion).AutoInclude().IsRequired();
			motherboardEntity.Navigation(m => m.AudioChipset).AutoInclude().IsRequired();
			motherboardEntity.Navigation(m => m.NetworkAdapter).AutoInclude().IsRequired();



			var mouseEntity = modelBuilder.Entity<Mouse>();
			
			mouseEntity.HasMany(m=>m.DPIModes)
			           .WithMany(m => m.Mouses);
			
			
			var powerSupplyEntity = modelBuilder.Entity<PowerSupply>();

			powerSupplyEntity.Navigation(p => p.Certificate80Plus).AutoInclude().IsRequired();
			
			
			
			var ramEntity = modelBuilder.Entity<RAM>();
			
			ramEntity.Navigation(r => r.Type).AutoInclude().IsRequired();
			
			
			
			var ssdEntity = modelBuilder.Entity<SSD>();

			ssdEntity.Navigation(s => s.Controller).AutoInclude().IsRequired();
			
			
			
			var simpleComputerComponentEntity = modelBuilder.Entity<SimpleComputerComponent>();
			
			simpleComputerComponentEntity.Navigation(s => s.Type).AutoInclude().IsRequired();
			
			
			var cpuCoreEntity = modelBuilder.Entity<CPUCore>();

			cpuCoreEntity.Navigation(c => c.Vendor).AutoInclude().IsRequired();
			
			
			
			var gpuEntity = modelBuilder.Entity<GPU>();

			gpuEntity.Navigation(g => g.Vendor).AutoInclude().IsRequired();



			var bankCardEntity = modelBuilder.Entity<BankCard>();

			bankCardEntity.Navigation(c => c.BankSystem).AutoInclude().IsRequired();

			var cartEntity = modelBuilder.Entity<Cart>();
			
			cartEntity.HasKey(c => c.CustomerId);
			cartEntity.HasMany(c => c.Items);
			          //.WithMany(i => i.Carts);

			var cartItemsEntity = modelBuilder.Entity<CartItem>();
			
			cartItemsEntity.Navigation(o => o.Cart).AutoInclude().IsRequired();
			cartItemsEntity.Navigation(o=> o.Item).AutoInclude().IsRequired();
			
			var customerEntity = modelBuilder.Entity<Customer>();

			customerEntity
				.HasOne(c => c.Cart)
				.WithOne(c => c.Customer)
				.HasForeignKey<Customer>(c => c.CartId)
				.IsRequired();

			customerEntity.HasMany(c => c.Orders);

			modelBuilder.Entity<Order>()
			            .HasMany(o => o.Items);
			
			modelBuilder.Entity<Order>()
			            .Navigation(o => o.OrderStatus).AutoInclude().IsRequired();

			var orderItemsEntity = modelBuilder.Entity<OrderItems>();
			
			orderItemsEntity.Navigation(o => o.Order).AutoInclude().IsRequired();
			orderItemsEntity.Navigation(o => o.Item).AutoInclude().IsRequired();
			
			base.OnModelCreating(modelBuilder);
		}
	}
}