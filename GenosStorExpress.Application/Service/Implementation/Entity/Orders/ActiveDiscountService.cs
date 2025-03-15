using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class ActiveDiscountService: IActiveDiscountService {
        private IGenosStorExpressRepositories _repositories;

        public void Create(ActiveDiscountWrapper item) {
            _repositories.Orders.ActiveDiscounts.Create(new ActiveDiscount {CreatedAt = DateTime.Now, EndsAt = item.EndsAt, Value = item.Value});
        }

        public ActiveDiscountWrapper? Get(int id) {
            ActiveDiscount? obj = _repositories.Orders.ActiveDiscounts.Get(id);
            if (obj == null) {
                return null;
            }
            return new ActiveDiscountWrapper { Value = obj.Value, EndsAt = obj.EndsAt };
        }

        public List<ActiveDiscountWrapper> List() {
            return _repositories.Orders.ActiveDiscounts.List().Select(i => new ActiveDiscountWrapper { Value = i.Value, EndsAt = i.EndsAt }).ToList();
        }

        public void Update(int id, ActiveDiscountWrapper wrapped) {
            ActiveDiscount? obj = _repositories.Orders.ActiveDiscounts.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Скидки с номером {id} не существует");
            }
            obj.EndsAt = wrapped.EndsAt;
            obj.Value = wrapped.Value;
            _repositories.Orders.ActiveDiscounts.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Orders.ActiveDiscounts.Delete(id);
        }

        public bool IsActive(ActiveDiscountWrapper activeDiscount) {
            return activeDiscount.EndsAt > DateTime.Now;
        }

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

        public ActiveDiscountService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}