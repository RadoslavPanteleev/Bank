using BankServer.Controllers.Base;
using BankServer.Models;
using BankServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : BankControllerBase<Category, Category, CategoriesService>
    {
        private readonly CategoriesService categoriesService;

        public CategoriesController(CategoriesService categoriesService) : base(categoriesService)
        {
            this.categoriesService = categoriesService;
        }
    }
}
