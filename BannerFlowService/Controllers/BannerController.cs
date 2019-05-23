using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BannerFlowService.Helpers;
using BannerFlowService.Models;
using BannerFlowService.Services;
using BannerFlowService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BannerFlowService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private IBannerService bannerService;
        public BannerController()
        {
            bannerService = new BannerService();
        }

        [HttpGet("GetHtml/{id}")]
        public ActionResult<string> GetHtml(int id)
        {
            return bannerService.GetHtml(id);
        }

        [HttpGet("GetAllBanners")]
        public ActionResult<IEnumerable<Banner>> GetAllBanners()
        {
            return bannerService.GetAllBanners();
        }

        [HttpGet("GetBanner/{id}")]
        public ActionResult<Banner> GetBanner(int id)
        {
            return bannerService.GetBanner(id);
        }

        [HttpPost]
        public string CreateBanner(Banner banner)
        {
            return bannerService.CreateBanner(banner);
        }


        [HttpPost]
        public string UpdateBanner(Banner banner)
        {
            return bannerService.UpdateBanner(banner);
        }

        [HttpDelete("DeleteBanner/{id}")]
        public string DeleteBanner(int id)
        {
            return bannerService.DeleteBanner(id);
        }
    }
}
