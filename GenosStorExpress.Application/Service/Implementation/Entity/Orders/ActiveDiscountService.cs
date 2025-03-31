using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    /// <summary>
    /// Реализация сервиса скидок
    /// </summary>
    public class ActiveDiscountService: IActiveDiscountService {
        private readonly IGenosStorExpressRepositories _repositories;
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="repositories"></param>
        public ActiveDiscountService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }
        
        /// <summary>
        /// Создание скидки
        /// </summary>
        /// <param name="item">Данные создаваемой скидки</param>
        public void Create(DetailedActiveDiscountWrapper item) {
            var created = new ActiveDiscount {
                CreatedAt = DateTime.Now,
                EndsAt = item.EndsAt,
                Value = item.Value
            };
            _repositories.Orders.ActiveDiscounts.Create(created);
            _repositories.Items.All.List().ForEach(i => {
                long? match = item.ForItems.FirstOrDefault(it => it == i.Id);
                if (match != null) {
                    i.ActiveDiscount = created;
                    _repositories.Items.All.Update(i);
                }
            });
            item.Id = created.Id;
        }

        /// <summary>
        /// Получение информации о конкретной скидке
        /// </summary>
        /// <param name="id">Номер скидки</param>
        /// <returns>Данные о скидке</returns>
        public DetailedActiveDiscountWrapper? Get(int id) {
            ActiveDiscount? obj = _repositories.Orders.ActiveDiscounts.Get(id);
            if (obj == null) {
                return null;
            }
            return new DetailedActiveDiscountWrapper {
                Id = obj.Id,
                Value = obj.Value,
                EndsAt = obj.EndsAt,
                ForItems = _repositories.Items.All.List().Where(it => it.ActiveDiscount?.Id == id).Select(it => it.Id).ToList()
            };
        }

        /// <summary>
        /// Обновление информации о скидке
        /// </summary>
        /// <param name="id">Номер обновляемой скидки</param>
        /// <param name="wrapped">Обновлённые данные о скидке</param>
        /// <exception cref="NullReferenceException">Если такой скидки не существует</exception>
        public void Update(int id, DetailedActiveDiscountWrapper wrapped) {
            ActiveDiscount? obj = _repositories.Orders.ActiveDiscounts.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Скидки с номером {id} не существует");
            }

            obj.Value = wrapped.Value;
            obj.EndsAt = wrapped.EndsAt;
            _repositories.Orders.ActiveDiscounts.Update(obj);
            
            _repositories.Items.All.List().ForEach(i => {
                long? match = wrapped.ForItems.FirstOrDefault(it => it == i.Id);
                if (match != null) {
                    i.ActiveDiscount = obj;
                    _repositories.Items.All.Update(i);
                }
            });
            
        }

        /// <summary>
        /// Получение списка активных скидок
        /// </summary>
        /// <returns>Список активных скидок</returns>
        public List<ActiveDiscountWrapper> List() {
            return _repositories.Orders.ActiveDiscounts.List()
                .Where(i => i.EndsAt >= DateTime.Now)
                .Select(
                    i => new ActiveDiscountWrapper {
                        Id = i.Id,
                        Value = i.Value,
                        EndsAt = i.EndsAt
                    }).ToList();
        }

        /// <summary>
        /// Деактивация скидки. Все товары с указанной скидкой теряют её
        /// </summary>
        /// <param name="id">Номер скидки</param>
        public void Deactivate(int id) {
            _ = _repositories
                        .Items
                        .All
                        .List()
                        .Where(i => i.ActiveDiscount != null ? i.ActiveDiscount.Id == id: false)
                        .Select(i => { i.ActiveDiscount = null; return i; })
                        .ToList()
                        .Select(i => { _repositories.Items.All.Update(i); return i; })
                        .ToList();
            _repositories.Save();
        }
        
        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
    }
}