using GenosStorExpress.Application.Service.Implementation;
using GenosStorExpress.Application.Service.Implementation.Common;
using GenosStorExpress.Application.Service.Implementation.Entity;
using GenosStorExpress.Application.Service.Implementation.Entity.Items;
using GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Service.Implementation.Entity.Orders;
using GenosStorExpress.Application.Service.Implementation.Entity.Users;
using GenosStorExpress.Application.Service.Interface;
using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Infrastructure.Context;
using GenosStorExpress.Infrastructure.Repository;
using GenosStorExpress.Infrastructure.Repository.Item;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GenosStorExpressDatabaseContext>(
	options => options.UseInMemoryDatabase(
	builder.Configuration.GetConnectionString("DefaultConnection")
	)
);

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<GenosStorExpressDatabaseContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<IGenosStorExpressRepositories, GenosStorExpressRepositories>();
builder.Services.AddScoped<IAllItemsRepository, AllItemsRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICommonServices, CommonServices>();



builder.Services.AddScoped<ICertificate80PlusService, Certificate80PlusService>();
builder.Services.AddScoped<IComputerCaseTypesizeService, ComputerCaseTypesizesService>();
builder.Services.AddScoped<ICoolerMaterialService, CoolerMaterialService>();
builder.Services.AddScoped<ICPUSocketService, CPUSocketsService>();
builder.Services.AddScoped<IDefinitionService, DefinitionService>();
builder.Services.AddScoped<IDPIModeService, DPIModeService>();
builder.Services.AddScoped<IKeyboardTypeService, KeyboardTypeService>();
builder.Services.AddScoped<IKeyboardTypesizeService, KeyboardTypesizeService>();
builder.Services.AddScoped<IMatrixTypeService, MatrixTypeService>();
builder.Services.AddScoped<IMotherboardFormFactorService, MotherboardFormFactorsService>();
builder.Services.AddScoped<IRAMTypeService, RAMTypesService>();
builder.Services.AddScoped<IUnderlightService, UnderlightService>();
builder.Services.AddScoped<IVendorService, VendorsService>();
builder.Services.AddScoped<IVesaSizeService, VesaSizeService>();
builder.Services.AddScoped<IVideoPortService, VideoPortService>();
builder.Services.AddScoped<IPCIEVersionService, PCIEVersionService>();
builder.Services.AddScoped<ICharacteristicsService, CharacteristicsService>();

builder.Services.AddScoped<IComputerCaseService, ComputerCaseService>();
builder.Services.AddScoped<ICPUCoolerService, CPUCoolerService>();
builder.Services.AddScoped<ICPUService, CPUService>();
builder.Services.AddScoped<IDisplayService, DisplayService>();
builder.Services.AddScoped<IGraphicsCardService, GraphicsCardService>();
builder.Services.AddScoped<IHDDService, HDDService>();
builder.Services.AddScoped<IKeyboardService, KeyboardService>();
builder.Services.AddScoped<IMotherboardService, MotherboardService>();
builder.Services.AddScoped<IMouseService, MouseService>();
builder.Services.AddScoped<INVMeSSDService, NVMeSSDService>();
builder.Services.AddScoped<IPowerSupplyService, PowerSupplyService>();
builder.Services.AddScoped<IRAMService, RAMService>();
builder.Services.AddScoped<ISataSSDService, SataSSDService>();
builder.Services.AddScoped<IComputerComponentServices, ComputerComponentServices>();

builder.Services.AddScoped<IAudioChipsetService, AudioChipsetService>();
builder.Services.AddScoped<ICPUCoreService, CPUCoresService>();
builder.Services.AddScoped<IGPUService, GPUService>();
builder.Services.AddScoped<IMotherboardChipsetService, MotherboardChipsetService>();
builder.Services.AddScoped<INetworkAdapterService, NetworkAdapterService>();
builder.Services.AddScoped<ISimpleComputerComponentTypeService, SimpleComputerComponentTypeService>();
builder.Services.AddScoped<ISSDControllerService, SSDControllerService>();
builder.Services.AddScoped<ISimpleComputerComponentService, SimpleComputerComponentsService>();

builder.Services.AddScoped<IAllItemsService, AllItemsService>();
builder.Services.AddScoped<IItemTypeService, ItemTypesService>();
builder.Services.AddScoped<IPreparedAssemblyService, PreparedAssemblyService>();
builder.Services.AddScoped<IItemServices, ItemServices>();

builder.Services.AddScoped<IActiveDiscountService, ActiveDiscountService>();
builder.Services.AddScoped<IBankCardService, BankCardsService>();
builder.Services.AddScoped<IBankSystemService, BankSystemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddScoped<IOrderEntitiesService, OrderEntitiesService>();

builder.Services.AddScoped<ILegalEntityService, LegalEntityService>();
builder.Services.AddScoped<IUserEntitiesService, UserEntitiesService>();
builder.Services.AddScoped<IEntityServices, EntityServices>();

builder.Services.AddScoped<IItemBuilderService, ItemBuilderService>();
builder.Services.AddScoped<IItemServiceRouter, ItemServiceRouter>();

builder.Services.AddScoped<IServices, Services>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
	    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GenosStorExpress API V1");
	    c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) {
	var context = scope.ServiceProvider.GetRequiredService<GenosStorExpressDatabaseContext>();

	var services = scope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<User>>();
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

	await GenosStorExpressDatabaseInitializer.Initialize(context, userManager, roleManager);
	
	context.Vendors.Add(new Vendor { Name = "Ardor" });
	context.ItemTypes.Add(new ItemType { Name = "computer_case" });
	context.MotherboardFormFactors.Add(new MotherboardFormFactor { Name = "ATX" });
	context.ComputerCaseTypesizes.Add(new ComputerCaseTypesize { Name = "MidTower" });
	context.SaveChanges();

}

app.Run();
