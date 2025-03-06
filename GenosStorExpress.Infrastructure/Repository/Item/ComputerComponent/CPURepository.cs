﻿using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class CPURepository: ICPURepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CPURepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CPU> List() {
            return _context.CPUs.ToList();
        }

        public CPU Get(int id) {
            return _context.CPUs.Find(id);
        }

        public void Create(CPU cpu) {
            _context.CPUs.Add(cpu);
        }

        public void Update(CPU cpu) {
            _context.Entry(cpu).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CPU cpu = _context.CPUs.Find(id);
            if (cpu != null)
                _context.CPUs.Remove(cpu);
        }
        
    }
}