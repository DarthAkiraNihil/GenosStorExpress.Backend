using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class GraphicsCardService: IGraphicsCardService {
        private IGenosStorExpressRepositories _repositories;

        public GraphicsCardService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(GraphicsCard item) {
            _repositories.Items.ComputerComponents.GraphicsCards.Create(item);
        }

        public GraphicsCard Get(int id) {
            return _repositories.Items.ComputerComponents.GraphicsCards.Get(id);
        }

        public List<GraphicsCard> List() {
            return _repositories.Items.ComputerComponents.GraphicsCards.List();
        }

        public void Update(GraphicsCard item) {
            _repositories.Items.ComputerComponents.GraphicsCards.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.GraphicsCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<GraphicsCard> Filter(List<Func<GraphicsCard, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}