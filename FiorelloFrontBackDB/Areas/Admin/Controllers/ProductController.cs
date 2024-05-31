﻿using FiorelloFrontBackDB.Helpers;
using FiorelloFrontBackDB.Services;
using FiorelloFrontBackDB.Services.Interfaces;
using FiorelloFrontBackDB.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloFrontBackDB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var paginatedDatas = await _productService.GetAllPaginatedAsync(page);

            var mappedDatas = _productService.GetMappedDatas(paginatedDatas);


            int pageCount = await GetPageCountAsync(4);

            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return View(mappedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }
    }
}
