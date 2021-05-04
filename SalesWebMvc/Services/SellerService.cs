using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.OrderBy(x => x.Name).ToList();
        }

        public Seller FindSeller(int id)
        {
            return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
        }

        public void CreateSeller(Seller seller)
        {
            _context.Add(seller);
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
