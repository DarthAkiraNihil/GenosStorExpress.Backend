﻿using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class GraphicsCardRepository: IGraphicsCardRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public GraphicsCardRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<GraphicsCard> List() {
            return _context.GraphicsCards
                           .Include(i => i.Reviews)
                           .Include(i => i.VideoPorts)
                           .ToList();
        }

        public GraphicsCard? Get(int id) {
            return _context.GraphicsCards
                           .Include(i => i.Reviews)
                           .Include(i => i.VideoPorts)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(GraphicsCard graphicsCard) {
            _context.GraphicsCards.Add(graphicsCard);
            _context.SaveChanges();
        }

        public void Update(GraphicsCard graphicsCard) {
            _context.Entry(graphicsCard).State = EntityState.Modified;
        }

        public void Delete(int id) {
            GraphicsCard? graphicsCard = _context.GraphicsCards.Find(id);
            if (graphicsCard != null) {
                _context.GraphicsCards.Remove(graphicsCard);
            }
        }
        
    }
}