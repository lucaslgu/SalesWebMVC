using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        public Department FindDepartment(int id)
        {
            return _context.Department.Find(id);
        }

        public void CreateDepartment(Department department)
        {
            _context.Add(department);
            _context.SaveChanges();
        }

        public void UpdateSeller(Department department)
        {
            _context.Update(department);
            _context.SaveChanges();
        }

        public void DeleteSeller(Department department)
        {
            _context.Seller.Remove(department);
            _context.SaveChanges();
        }
    }
}
