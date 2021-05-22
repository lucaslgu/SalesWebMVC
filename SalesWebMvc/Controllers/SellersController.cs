using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: SellersController
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAll();
            return View(list);
        }

        // GET: SellersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var seller = await _sellerService.FindSeller(id);
            return View(seller);
        }

        // GET: SellersController/Create
        public async Task<IActionResult> Create(int id)
        {
            var departments = await _departmentService.FindAll();
            var viewModel = new SellerFormViewModel(departments);

            return View(viewModel);
        }

        // POST: SellersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            try
            {
                if(!ModelState.IsValid) 
                {
                    var departments = await _departmentService.FindAll();
                    var viewModel = new SellerFormViewModel(seller, departments);
                    return View(viewModel);
                }
                
                await _sellerService.CreateSeller(seller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Ocorreu um erro!" 
                                            });
            }
        }

        // GET: SellersController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Id not found!" 
                                            });

            var seller = await _sellerService.FindSeller(id.Value);

            if (seller == null)
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Seller not found!" 
                                            });

            List<Department> departments = await _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel(seller, departments);
            return View(viewModel);
        }

        // POST: SellersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seller seller)
        {
            try
            {
                if(!ModelState.IsValid) 
                {
                    var departments = await _departmentService.FindAll();
                    var viewModel = new SellerFormViewModel(seller, departments);
                    return View(viewModel);
                }

                await _sellerService.UpdateSeller(seller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Ocorreu um erro!" 
                                            });
            }
        }

        // GET: SellersController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Ocorreu um erro!" 
                                            });
            }
            var seller = await _sellerService.FindSeller(id.Value);
            return View(seller);
        }

        // POST: SellersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Seller seller)
        {
            try
            {
                await _sellerService.DeleteSeller(seller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error), new { 
                                            message = "Ops... Ocorreu um erro!" 
                                            });
            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel
            { 
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
