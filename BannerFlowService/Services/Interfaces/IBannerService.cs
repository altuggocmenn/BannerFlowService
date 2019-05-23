using BannerFlowService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerFlowService.Services.Interfaces
{
    public interface IBannerService
    {
        string GetHtml(int id);
        List<Banner> GetAllBanners();
        Banner GetBanner(int id);
        string CreateBanner(Banner banner);
        string UpdateBanner(Banner banner);
        string DeleteBanner(int id);
    }
}
