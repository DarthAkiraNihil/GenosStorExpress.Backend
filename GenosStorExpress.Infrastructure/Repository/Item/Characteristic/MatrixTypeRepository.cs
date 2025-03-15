using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class MatrixTypeRepository: IMatrixTypeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public MatrixTypeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<MatrixType> List() {
            return _context.MatrixTypes.ToList();
        }

        public MatrixType? Get(int id) {
            return _context.MatrixTypes.Find(id);
        }

        public void Create(MatrixType matrixType) {
            _context.MatrixTypes.Add(matrixType);
        }

        public void Update(MatrixType matrixType) {
            _context.Entry(matrixType).State = EntityState.Modified;
        }

        public void Delete(int id) {
            MatrixType? matrixType = _context.MatrixTypes.Find(id);
            if (matrixType != null) {
                _context.MatrixTypes.Remove(matrixType);
            }
        }
        
    }
}