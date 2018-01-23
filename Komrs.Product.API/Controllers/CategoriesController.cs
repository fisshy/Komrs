using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Komrs.Product.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/categories")]
    public class CategoriesController : Controller
    {
    }
}