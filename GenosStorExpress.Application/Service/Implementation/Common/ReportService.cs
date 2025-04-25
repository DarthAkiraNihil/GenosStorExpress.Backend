using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Implementation.Util.QuestPDFExtensions;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GenosStorExpress.Application.Service.Implementation.Common {
    public class ReportService: IReportService {
        
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IItemTypeService _itemTypeService;
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IAllItemsRepository _allItemsRepository;

        private readonly List<string> _receiptHeaderTitles = new List<string> {
            "Наименование предмета", "Цена за единицу (руб.)", "Количество (шт.)", "Итого (руб.)"
        };

        private readonly List<string> _invoiceHeaderTitles = new List<string> {
            "Наименование товара",
            "Код вида товара",
            "Единица измерения",
            "Количество",
            "Цена за единицу",
            "Стоимость товаров всего"
        };

        private readonly List<string> _clientStatisticHeaderTitles = new List<string> {
            "Email покупателя",
            "Тип",
            "Сделано заказов за период",
            "Максимальная сумма чека",
            "Средняя сумма чека",
            "Общая выручка от клиента за период"
        };
        
        private const string _traderName = "ООО \"Genos Store Incorporated\"";
        private const string _traderAddress = "ООО \"Genos Store Incorporated\"";
        private const int _traderINN = 1000000;
        private const int _traderKPP = 1000000;


        private const int _soldItemsChartHeight = 350;
        private const int _soldItemsChartTitlePadding = 30;

        private class ClientStatisticData {
            public Customer? Customer { get; set; }
            public string Type { get; set; }
            public int OrdersCount { get; set; }
            public double MaxRevenue { get; set; }
            public double AverageRevenue { get; set; }
            public double Revenue { get; set; }

            public ClientStatisticData() {
                Type = string.Empty;
            }
        }
        

        public ReportService(IPaymentService paymentService, IOrderService orderService, IItemTypeService itemTypeService, IGenosStorExpressRepositories repositories) {
            _paymentService = paymentService;
            _orderService = orderService;
            _itemTypeService = itemTypeService;
            _repositories = repositories;
            _allItemsRepository = _repositories.Items.All;
        }
        
        private Customer? _getCustomer(string id) {
            Customer? customer = _repositories.Users.IndividualEntities.Get(id);
            if (customer == null) {
                return _repositories.Users.LegalEntities.Get(id);
            }
            return customer;
        }

        public MemoryStream CreateOrderReceipt(string customerId, int orderId) {
            
            var customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException($"Покупатель с номером {customerId} не найден");
            }
            
            if (!(customer is IndividualEntity)) {
                throw new ArgumentException("Покупатель должен быть физическим лицом для генерации чека");
            }
            
            var order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказ с номером {orderId} не найден");
            }

            string orderer = _paymentService.GetOrdererInfo(customerId);
            string createdAt = order.CreatedAt.ToString("dd/MM/yyyy HH:mm");

            Document doc = Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8).FontFamily("Arial"));

                    page.Header()
                        .Text($"Чек для заказа №{order.Id}")
                        .SemiBold()
                        .FontSize(16)
                        .AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column => {
                            column.Item().Text($"Создан: {createdAt}").FontSize(12);
                            column.Item().Text($"Заказчик: {orderer}").FontSize(12);
                            column.Item().Text($"Статус: {order.OrderStatus!.Name}").FontSize(12);
                            column.Item().Table(table => {
                                table.ColumnsDefinition(columns => {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                foreach (var title in _receiptHeaderTitles) {
                                    table.Cell().LabelCell(title);
                                }

                                foreach (var item in order.Items) {
                                    _fillReceiptItemRow(table, item);
                                }
                            });
                            column.Item()
                                  .Text($"Итого: {_orderService.CalculateTotal(orderId)} руб.")
                                  .AlignRight()
                                  .FontSize(14)
                                  .Bold();
                        });


                    page.Footer()
                        .AlignCenter()
                        .Text(x => {
                            x.Span("Страница ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return new MemoryStream(doc.GeneratePdf());
        }

        private void _fillReceiptItemRow(TableDescriptor table, OrderItems item) {
            var boughtFor = item.BoughtFor;
            var quantity = item.Quantity;
            var total = boughtFor * quantity;

            var realItem = _repositories.Items.All.Get(item.ItemId)!;
            
            table.Cell()
                 .ValueCell()
                 .Text(realItem.Name);
            table.Cell()
                 .ValueCell()
                 .Text(boughtFor.ToString("0.00"));
            table.Cell()
                 .ValueCell()
                 .Text(quantity.ToString());
            table.Cell()
                 .ValueCell()
                 .Text(total.ToString("0.00"));
        }

        public MemoryStream CreateOrderInvoice(string customerId, int orderId) {
            
            var customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException($"Покупатель с номером {customerId} не найден");
            }
            
            if (!(customer is LegalEntity)) {
                throw new ArgumentException("Customer must be an legal entity in order to generate invoice");
            }
            var entity = customer as LegalEntity;
            
            var order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказ с номером {orderId} не найден");
            }
            string createdAt = order.CreatedAt.ToString("dd/MM/yyyy");

            Document doc = Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8).FontFamily("Arial"));

                    page.Header()
                        .Text($"Счёт-фактура для заказа №{order.Id} от {createdAt}")
                        .SemiBold()
                        .FontSize(16)
                        .AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column => {
                            _fillExtendedOrderInformation(column, entity!, order);
                            column.Item().Table(table => {
                                table.ColumnsDefinition(columns => {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                foreach (var title in _invoiceHeaderTitles) {
                                    table.Cell().LabelCell(title);
                                }

                                foreach (var item in order.Items) {
                                    _fillInvoiceItemRow(table, item);
                                }
                            });
                            column.Item()
                                  .Text($"Всего к оплате: {_orderService.CalculateTotal(orderId)} руб.")
                                  .AlignRight()
                                  .FontSize(14)
                                  .Bold();
                        });


                    page.Footer()
                        .AlignCenter()
                        .Text(x => {
                            x.Span("Страница ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return new MemoryStream(doc.GeneratePdf());
        }

        private void _fillExtendedOrderInformation(ColumnDescriptor column, LegalEntity legalEntity, Order order) {
            
            string orderer = _paymentService.GetOrdererInfo(legalEntity.Id);

            foreach (var text in new List<string> {
                 $"Продавец: {_traderName}",
                 $"Адрес: {_traderAddress}",
                 $"ИНН продавца: {_traderINN}",
                 $"КПП продавца: {_traderKPP}",
                 $"Покупатель: {orderer}",
                 $"Адрес: {legalEntity.LegalAddress}",
                 $"ИНН покупателя: {legalEntity.INN}",
                 $"КПП покупателя: {legalEntity.KPP}",
                 "Валюта: руб.",
            }) {
                column.Item().Text(text).FontSize(14);
            }
            
            
            
        }
        
        private void _fillInvoiceItemRow(TableDescriptor table, OrderItems item) {
            var boughtFor = item.BoughtFor;
            var quantity = item.Quantity;
            var total = boughtFor * quantity;
            
            table.Cell()
                 .ValueCell()
                 .Text(item.Item!.Name);
            table.Cell()
                 .ValueCell()
                 .Text(item.Item.ItemType.Id.ToString());
            table.Cell()
                 .ValueCell()
                 .Text("шт.");
            table.Cell()
                 .ValueCell()
                 .Text(quantity.ToString());
            table.Cell()
                 .ValueCell()
                 .Text(boughtFor.ToString("0.00"));
            table.Cell()
                 .ValueCell()
                 .Text(total.ToString("0.00"));
        }

        public MemoryStream CreateOrderHistoryReport(string customerId) {

            var orders = _repositories.Orders.Orders.List().Where(o => o.CustomerId == customerId).ToList();
            orders.Sort((x, y) => x.Id < y.Id ? -1 : 1);
            string orderer = _paymentService.GetOrdererInfo(customerId);

            Document doc = Document.Create(container => {
                foreach (var order in orders) {
                    string createdAt = order.CreatedAt.ToString("dd/MM/yyyy HH:mm");

                    container.Page(page => {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(8).FontFamily("Arial"));

                        page.Header()
                            .Text($"Данные по заказу №{order.Id}")
                            .SemiBold()
                            .FontSize(16)
                            .AlignCenter();

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(column => {
                                column.Item().Text($"Создан: {createdAt}").FontSize(12);
                                column.Item().Text($"Заказчик: {orderer}").FontSize(12);
                                column.Item().Text($"Статус: {order.OrderStatus!.Name}").FontSize(12);
                                column.Item().Table(table => {
                                    table.ColumnsDefinition(columns => {
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    foreach (var title in _receiptHeaderTitles) {
                                        table.Cell().LabelCell(title);
                                    }

                                    foreach (var item in order.Items) {
                                        _fillReceiptItemRow(table, item);
                                    }
                                });
                                column.Item()
                                      .Text($"Итого: {_orderService.CalculateTotal((int)order.Id)} руб.")
                                      .AlignRight()
                                      .FontSize(14)
                                      .Bold();
                            });


                        page.Footer()
                            .AlignCenter()
                            .Text(x => {
                                x.Span("Страница ");
                                x.CurrentPageNumber();
                            });
                    });
                }
            });

            return new MemoryStream(doc.GeneratePdf());
        }

        private readonly Dictionary<string, string> _chartItemTypeColors = new Dictionary<string, string> {
            { "Motherboard", "#00FA9A" },
            { "CPU", "#4682B4" },
            { "RAM", "#800080" },
            { "GraphicsCard", "#FFC0CB" },
            { "HDD", "#8B4513" },
            { "NVMeSSD", "#708090" },
            { "SataSSD", "#F0F8FF" },
            { "CPUCooler", "#C0C0C0" },
            { "PowerSupply", "#FFFF00" },
            { "ComputerCase", "#FFA500" },
            { "Display", "#FF0000" },
            { "Keyboard", "#2E8B57" },
            { "Mouse", "#0000CD" },
            { "PreparedAssembly", "#000000" },
        };
        
        private readonly Dictionary<string, string> _chartItemTypeTranslations = new Dictionary<string, string> {
            { "Motherboard", "Материнская плата" },
            { "CPU", "ЦПУ" },
            { "RAM", "ОЗУ" },
            { "GraphicsCard", "Видеокарта" },
            { "HDD", "Жёсткий диск" },
            { "NVMeSSD", "Накопитель NVMe SSD" },
            { "SataSSD", "Накопитель Sata SSD" },
            { "CPUCooler", "Кулер для процессора" },
            { "PowerSupply", "Блок питания" },
            { "ComputerCase", "Корпус" },
            { "Display", "Монитор" },
            { "Keyboard", "Клавиатура" },
            { "Mouse", "Мышь" },
            { "PreparedAssembly", "Готовая сборка" },
        };

        public void GenerateSalesAnalysisReport(DateTime startDate, DateTime endDate, string path) {
            var orders = _repositories.Orders.Orders
                         .List()
                         .Where(
                                    o => o.CreatedAt >= startDate 
                               &&   o.CreatedAt <= endDate
                               &&   o.OrderStatus!.Id != (int) OrderStatusDescriptor.Cancelled
                        ).ToList();
            
            var customers = new List<Customer>();
            foreach (var order in orders) {
                if (customers.Count(c => c.Id == order.Customer!.Id) == 0) {
                    customers.Add(order.Customer!);
                }
            }

            var statisticData = new List<ClientStatisticData>();
            foreach (var customer in customers) {
                var customerOrders = orders.Where(o => o.Customer!.Id == customer.Id).ToList();

                string type;
                if (customer is LegalEntity) {
                    type = "Юридическое лицо";
                } else {
                    type = "Физическое лицо";
                }
                
                double maxCustomerRevenue = -1.0;
                double avgCustomerRevenue = 0.0;
                
                int orderCount = customerOrders.Count;
                double totalRevenue = customerOrders.Sum(o => _orderService.CalculateTotal((int) o.Id));

                foreach (var customerOrder in customerOrders) {
                    double total = _orderService.CalculateTotal((int) customerOrder.Id);
                    if (total > maxCustomerRevenue) {
                        maxCustomerRevenue = total;
                    }
                    avgCustomerRevenue += total;
                }
                
                avgCustomerRevenue /= orderCount;
                
                statisticData.Add(new ClientStatisticData {
                    Customer = customer,
                    Type = type,
                    AverageRevenue = avgCustomerRevenue,
                    MaxRevenue = maxCustomerRevenue,
                    OrdersCount = orderCount,
                    Revenue = totalRevenue,
                });
            }
            
            var itemsTypesFrequency = new Dictionary<string, int>();
            var itemTypesOutcomes = new Dictionary<string, double>();
            foreach (var type in _itemTypeService.List()) {
                itemsTypesFrequency.Add(type, 0);
                itemTypesOutcomes.Add(type, 0.0);
            }
            
            var itemsFrequency = new Dictionary<int, int>();
            var revenue = 0.0;
            var avgRevenue = 0.0;
            

            foreach (var order in orders) {
                if (order.OrderStatus!.Id >= (int)OrderStatusDescriptor.Paid) {
                    double total = _orderService.CalculateTotal((int) order.Id);
                    revenue += total;
                    avgRevenue += total;
                }
                foreach (var item in order.Items) {
                    itemsTypesFrequency[item.Item!.ItemType.Name] += item.Quantity;
                    itemTypesOutcomes[item.Item.ItemType.Name] += item.Quantity * item.BoughtFor;
                    
                    if (!itemsFrequency.ContainsKey(item.Item.Id)) {
                        itemsFrequency.Add(item.Item.Id, 0);
                    }
                    itemsFrequency[item.Item.Id] += item.Quantity;
                }
            }
            
            avgRevenue /= orders.Count;

            var itemTypesFrequenciesSlices = new List<PieSlice>();
            var itemTypesOutcomesSlices = new List<PieSlice>();

            foreach (var k in itemsTypesFrequency) {
                if (k.Value > 0) {
                    itemTypesFrequenciesSlices.Add(
                        new PieSlice(_chartItemTypeTranslations[k.Key], k.Value) {
                            IsExploded = true, Fill = OxyColor.Parse(_chartItemTypeColors[k.Key])
                        }
                    );
                }
            }
            
            foreach (var k in itemTypesOutcomes) {
                if (k.Value > 0) {
                    itemTypesOutcomesSlices.Add(
                        new PieSlice(_chartItemTypeTranslations[k.Key], k.Value) {
                            IsExploded = true, Fill = OxyColor.Parse(_chartItemTypeColors[k.Key])
                        }
                    );
                }
            }

            int mostBoughtItemId = 1;
            int leastBoughtItemId = 1;

            int minItemFreq = 1000000;
            int maxItemFreq = -1;

            foreach (var item in itemsFrequency) {
                if (item.Value > maxItemFreq) {
                    mostBoughtItemId = item.Key;
                    maxItemFreq = item.Value;
                }

                if (item.Value < minItemFreq) {
                    leastBoughtItemId = item.Key;
                    minItemFreq = item.Value;
                }
            }
            
            
            //var chart = new PieChart() {Entries = entries};
            
            Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8).FontFamily("Arial"));

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column => {
                            var titleStyle = TextStyle
                                             .Default
                                             .FontSize(14)
                                             .SemiBold();

                            column
                                .Item()
                                .PaddingBottom(10)
                                .AlignCenter()
                                .Text($"Статистика по продажам с {startDate.ToString("dd/MM/yyyy")} по {endDate.ToString("dd/MM/yyyy")}")
                                .Style(titleStyle);

                            column
                                .Item()
                                .ExtendHorizontal()
                                .Height(_soldItemsChartHeight)
                                .SkiaSharpCanvas((canvas, size) => {

                                    PlotModel soldItemsModel = new PlotModel {
                                        Title = "Статистика покупки по типам товаров",
                                        TitleFont = "Arial",
                                        TitleFontSize = 14,
                                        TitlePadding = _soldItemsChartTitlePadding
                                    };

                                    var soldItemsSeries = new PieSeries {
                                        InsideLabelPosition = 0.5,
                                        AngleSpan = 360, StartAngle = 0,
                                        OutsideLabelFormat = "{0} ({2:0}%)",
                                        Font = "Arial",
                                        AreInsideLabelsAngled = true
                                    };

                                    foreach (var entry in itemTypesFrequenciesSlices) {
                                        soldItemsSeries.Slices.Add(entry);
                                    }

                                    soldItemsModel.Series.Add(soldItemsSeries);

                                    var stream = new MemoryStream();
                                    var exporter = new SvgExporter {
                                        Width = size.Width,
                                        Height = _soldItemsChartHeight,
                                        UseVerticalTextAlignmentWorkaround = true
                                    };
                                    exporter.Export(soldItemsModel, stream);
                                    stream.Position = 0;

                                    var svg = new Svg.Skia.SKSvg();
                                    svg.Load(stream);

                                    canvas.DrawPicture(svg.Picture);
                                });

                            column
                                .Item()
                                .ExtendHorizontal()
                                .Height(_soldItemsChartHeight)
                                .SkiaSharpCanvas((canvas, size) => {

                                    PlotModel itemTypesRevenueModel = new PlotModel {
                                        Title = "Статистика выручки по типам товаров",
                                        TitleFont = "Arial",
                                        TitleFontSize = 14,
                                        TitlePadding = _soldItemsChartTitlePadding
                                    };

                                    var itemTypesRevenue = new PieSeries {
                                        InsideLabelPosition = 0.5,
                                        AngleSpan = 360, StartAngle = 0,
                                        OutsideLabelFormat = "{0} ({2:0}%)",
                                        Font = "Arial",
                                        AreInsideLabelsAngled = true
                                    };

                                    foreach (var entry in itemTypesOutcomesSlices) {
                                        itemTypesRevenue.Slices.Add(entry);
                                    }

                                    itemTypesRevenueModel.Series.Add(itemTypesRevenue);

                                    var stream = new MemoryStream();
                                    var exporter = new SvgExporter {
                                        Width = size.Width,
                                        Height = _soldItemsChartHeight,
                                        UseVerticalTextAlignmentWorkaround = true
                                    };
                                    exporter.Export(itemTypesRevenueModel, stream);
                                    stream.Position = 0;

                                    var svg = new Svg.Skia.SKSvg();
                                    svg.Load(stream);

                                    canvas.DrawPicture(svg.Picture);
                                });

                            column
                                .Item()
                                .AlignCenter()
                                .Text("Данные по продажам")
                                .Style(titleStyle);

                            column
                                .Item()
                                .Text($"Общая выручка: {revenue}")
                                .FontFamily("Arial")
                                .FontSize(12);

                            column
                                .Item()
                                .Text($"Средний размер чека: {avgRevenue}")
                                .FontFamily("Arial")
                                .FontSize(12);

                            var mostBoughtItem = _allItemsRepository.Get(mostBoughtItemId);

                            column
                                .Item()
                                .Text($"Самый продаваемый предмет: {mostBoughtItem!.Name}")
                                .FontFamily("Arial")
                                .FontSize(12);

                            var leastBoughtItem = _allItemsRepository.Get(leastBoughtItemId);

                            column
                                .Item()
                                .Text($"Наименее продаваемый предмет предмет: {leastBoughtItem!.Name}. Возможно, стоит установить на него скидку.")
                                .FontFamily("Arial")
                                .FontSize(12);

                            column
                                .Item()
                                .AlignCenter()
                                .Text("Статистика по заказам клиентам")
                                .Style(titleStyle);

                            column.Item().Table(table => {
                                table.ColumnsDefinition(columns => {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                foreach (var title in _clientStatisticHeaderTitles) {
                                    table.Cell().LabelCell(title);
                                }

                                foreach (var data in statisticData) {
                                    _fillStatisticRow(table, data);
                                }
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Страница ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf(path);
            
        }

        private void _fillStatisticRow(TableDescriptor table, ClientStatisticData data) {
            table.Cell()
                 .ValueCell()
                 .Text(data.Customer!.Email);
            table.Cell()
                 .ValueCell()
                 .Text(data.Type);
            table.Cell()
                 .ValueCell()
                 .Text(data.OrdersCount.ToString());
            table.Cell()
                 .ValueCell()
                 .Text(data.MaxRevenue.ToString("0.00"));
            table.Cell()
                 .ValueCell()
                 .Text(data.AverageRevenue.ToString("0.00"));
            table.Cell()
                 .ValueCell()
                 .Text(data.Revenue.ToString("0.00"));
        }
    }
}