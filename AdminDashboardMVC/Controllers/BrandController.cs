using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardMVC.Controllers
{
	public class BrandController(IUnitOfWork _unitOfWork) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			return View(brands);
		}
		public async Task<IActionResult> Create(ProductBrand brand)
		{
			try
			{
				await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);
				await _unitOfWork.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				ModelState.AddModelError("Name", "Please enter new name");
				return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
			}
		}
		public async Task<IActionResult> Delete(int id)
		{
			var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetByIdAsync(id);
			_unitOfWork.GetRepository<ProductBrand, int>().Remove(brand);
			await _unitOfWork.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
