using AdminDashboardMVC.Helpers;
using AdminDashboardMVC.Models;
using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;


namespace AdminDashboardMVC.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork , IMapper _mapper) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IReadOnlyList<ProductViewModel>>(products);
            return View(mappedProducts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                }
                else
                    productViewModel.PictureUrl = "images/products/glazed-donuts.png";
                var mappedProduct = _mapper.Map<ProductViewModel,Product>(productViewModel);
                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
			}
    
            return View(productViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product,ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    if (productViewModel.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(productViewModel.PictureUrl, "products");
                    }

                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                }

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(productViewModel);
                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product,ProductViewModel>(product);
            return View(mappedProduct); 
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id , ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return NotFound();
            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
                if(product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.GetRepository<Product, int>().Remove(product);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View(productViewModel);
            }
        }
    }
}
