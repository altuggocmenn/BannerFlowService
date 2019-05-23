using BannerFlowService.Helpers;
using BannerFlowService.Models;
using BannerFlowService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerFlowService.Services
{
    public class BannerService : IBannerService
    {
        private const string success = "Success";
        private const string fail = "Fail";
        private static BannerContext context;
        public BannerService(BannerContext _context = null)
        {
            if (_context != null)
            {
                context = _context;
            }
            else if (context == null)
            {
                var options = new DbContextOptionsBuilder<BannerContext>()
                    .UseInMemoryDatabase(databaseName: "BannerItems")
                    .Options;
                context = new BannerContext(options);

                context.Banners.Add(new Banner() { Id = 1, Html = "<body>Dummy1<body/>", Created = DateTime.Now, Modified = null });
                context.Banners.Add(new Banner() { Id = 2, Html = "<body>Dummy2<body/>", Created = DateTime.Now, Modified = null });
                context.Banners.Add(new Banner() { Id = 3, Html = "<body>Dummy3<body/>", Created = DateTime.Now, Modified = null });
                context.Banners.Add(new Banner() { Id = 4, Html = "<body>Dummy4<body/>", Created = DateTime.Now, Modified = null });

                context.SaveChanges();
            }
        }
        public string GetHtml(int id)
        {
            var entity = context.Banners.Find(id);
            return (entity != null && Validation.IsHtml(entity.Html)) ? entity.Html : string.Empty;
        }
        public List<Banner> GetAllBanners()
        {
            return context.Banners.ToList();
        }
        public Banner GetBanner(int id)
        {
            return context.Banners.Find(id);
        }
        public string CreateBanner(Banner banner)
        {
            // Validation
            if (Validation.IsValidBanner(banner))
                return fail;

            if (context.Banners.Find(banner.Id) == null)
            {
                context.Banners.Add(banner);
                context.SaveChanges();
                return success;
            }

            return fail;
        }
        public string UpdateBanner(Banner banner)
        {
            // Validation
            if (Validation.IsValidBanner(banner))
                return fail;

            if (context.Banners.Find(banner.Id) != null)
            {
                var trackedEntity = context.Banners.Find(banner.Id);
                context.Entry(trackedEntity).CurrentValues.SetValues(banner);
                context.Entry(trackedEntity).State = EntityState.Modified;
                context.SaveChanges();
                return success;
            }
            
            return fail;
        }
        public string DeleteBanner(int id)
        {
            var entity = context.Banners.Find(id);
            if (entity != null)
            {
                context.Banners.Remove(entity);
                context.SaveChanges();
                return success;
            }

            return fail;
        }
    }
}
