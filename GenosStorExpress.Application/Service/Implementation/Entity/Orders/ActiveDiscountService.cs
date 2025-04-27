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
        /// Активация скидки для товара
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="discountData">Данные о скидке</param>
        /// <exception cref="NullReferenceException">Если указанного товара не существует</exception>
        public void Activate(int itemId, ActiveDiscountWrapper discountData) {
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var created = new ActiveDiscount {
                CreatedAt = DateTime.Now,
                EndsAt = discountData.EndsAt,
                Value = discountData.Value
            };
            _repositories.Orders.ActiveDiscounts.Create(created);
            item.ActiveDiscount = created;
            _repositories.Save();
        }

        /// <summary>
        /// Редактирование данных о скидке
        /// </summary>
        /// <param name="discountId">Номер скидки</param>
        /// <param name="discountData">Обновлённые данные о скидке</param>
        /// <exception cref="NullReferenceException">Если указанной скидки не существует</exception>
        public void Edit(int discountId, ActiveDiscountWrapper discountData) {
            
            ActiveDiscount? obj = _repositories.Orders.ActiveDiscounts.Get(discountId);
            if (obj == null) {
                throw new NullReferenceException($"Скидки с номером {discountId} не существует");
            }

            obj.Value = discountData.Value;
            obj.EndsAt = discountData.EndsAt;
            _repositories.Orders.ActiveDiscounts.Update(obj);
            
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