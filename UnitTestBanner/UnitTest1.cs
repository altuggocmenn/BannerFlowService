using BannerFlowService.Controllers;
using BannerFlowService.Models;
using BannerFlowService.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTestBanner
{
    public class UnitTest1
    {
        [Fact]
        public void GetHtmlTest()
        {
            BannerContext context = new BannerContext(new DbContextOptionsBuilder<BannerContext>().UseInMemoryDatabase(databaseName: "BannerItemsTest1").Options);
            var testBanners = TestBanners();
            context.Banners.AddRange(testBanners);
            context.SaveChanges();
            var bannerService = new BannerService(context);

            var actual = bannerService.GetHtml(2);
            var expected = testBanners.Find(x => x.Id == 2).Html;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBannerTest()
        {
            BannerContext context = new BannerContext(new DbContextOptionsBuilder<BannerContext>().UseInMemoryDatabase(databaseName: "BannerItemsTest2").Options);
            var testBanners = TestBanners();
            context.Banners.AddRange(testBanners);
            context.SaveChanges();
            var bannerService = new BannerService(context);

            var actual = bannerService.GetBanner(2);
            var expected = testBanners.Find(x => x.Id == 2);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllBannersTest()
        {
            BannerContext context = new BannerContext(new DbContextOptionsBuilder<BannerContext>().UseInMemoryDatabase(databaseName: "BannerItemsTest3").Options);
            var testBanners = TestBanners();
            context.Banners.AddRange(testBanners);
            context.SaveChanges();
            var bannerService = new BannerService(context);

            var actual = bannerService.GetAllBanners();
            var expected = testBanners;

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteBannerTest()
        {
            BannerContext context = new BannerContext(new DbContextOptionsBuilder<BannerContext>().UseInMemoryDatabase(databaseName: "BannerItemsTest4").Options);
            var testBanners = TestBanners();
            context.Banners.AddRange(testBanners);
            context.SaveChanges();
            var bannerService = new BannerService(context);

            bannerService.DeleteBanner(2);
            var actual = bannerService.GetAllBanners();
            testBanners.Remove(testBanners.Find(x => x.Id == 2));
            var expected = testBanners;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateBannerTest()
        {
            var updateBanner = new Banner() { Id = 2, Html = "<body>UpdateBanner<body/>", Created = DateTime.Now, Modified = DateTime.Now };
            BannerContext context = new BannerContext(new DbContextOptionsBuilder<BannerContext>().UseInMemoryDatabase(databaseName: "BannerItemsTest5").Options);
            var testBanners = TestBanners();
            context.Banners.AddRange(testBanners);
            context.SaveChanges();
            var bannerService = new BannerService(context);

            bannerService.UpdateBanner(updateBanner);
            var actual = bannerService.GetAllBanners();

            var index = testBanners.FindIndex(x => x.Id == updateBanner.Id);
            testBanners.RemoveAt(index);
            testBanners.Insert(index, updateBanner);
            var expected = testBanners;

            CollectionAssert.AreEqual(expected, actual);
        }




        private List<Banner> TestBanners()
        {
            var context = new BannerContext();

            var banners = new List<Banner>();
            banners.Add(new Banner() { Id = 1, Html = "<body>Dummy1<body/>", Created = DateTime.Now, Modified = null });
            banners.Add(new Banner() { Id = 2, Html = "<bodyDummy2<body/>", Created = DateTime.Now, Modified = null });
            banners.Add(new Banner() { Id = 3, Html = "<bod>Dummy3<body/>", Created = DateTime.Now, Modified = DateTime.Now.AddDays(-3) });
            banners.Add(new Banner() { Id = 4, Html = "<body>Dummy4<body/>", Created = DateTime.Now.AddDays(-2), Modified = DateTime.Now });
            return banners;
        }
    }
}
