using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        public Department FindSeller(int id)
        {
            return _context.Department.Find(id);
        }

        public void CreateSeller(Department department)
        {
            _context.Add(department);
            _context.SaveChanges();
        }

        public void UpdateSeller(Seller seller)
        {
            _context.Update(seller);
            _context.SaveChanges();
        }

        public void DeleteSeller(Seller seller)
        {
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}
