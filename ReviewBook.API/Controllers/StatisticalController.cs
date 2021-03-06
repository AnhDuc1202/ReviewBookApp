using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;
        private readonly IUserService _userService;

        public StatisticalController(IStatisticalService statisticalService, IUserService userService)
        {
            _statisticalService = statisticalService;
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Rate/{n}")]
        public ActionResult<IEnumerable<RateStatisticalDTOs>> GetRate(int n)
        {
            return _statisticalService.RateStatistical(n);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Review/{n}")]
        public ActionResult<IEnumerable<ReviewStatisticalDTOs>> GetReview(int n)
        {
            return _statisticalService.ReviewStatistical(n);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Readed/{n}")]
        public ActionResult<IEnumerable<ReadedStatisticalDTOs>> GetReaded(int n)
        {
            return _statisticalService.ReadedStatistical(n);
        }
    }
}