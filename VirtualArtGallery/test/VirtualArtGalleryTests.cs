using NUnit.Framework;
using System;
using System.Collections.Generic;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.test
{
    [TestFixture]
    public class VirtualArtGalleryTests
    {
        IVirtualArtGallery galleryService;

        [SetUp]
        public void Setup()
        {
            galleryService = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_AddArtwork()
        {
            Artwork artwork = new Artwork(0, "Test Artwork", "Test Description", DateTime.Now, "Oil on Canvas", "https://example.com/test.jpg", 1);
            bool result = galleryService.AddArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_UpdateArtwork()
        {
            Artwork artwork = new Artwork(4, "Updated Artwork", "Updated Description", DateTime.Now, "Oil on Canvas", "https://example.com/updated.jpg", 1);
            bool result = galleryService.UpdateArtwork(artwork);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_RemoveArtwork()
        {
            bool result = galleryService.RemoveArtwork(9); 
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_GetArtworkById()
        {
            Artwork artwork = galleryService.GetArtworkById(4); 
            Assert.IsNotNull(artwork);
            Assert.AreEqual(4, artwork.ArtworkId);
        }

        [Test]
        public void Test_SearchArtworks()
        {
            List<Artwork> artworks = galleryService.SearchArtworks("Mona Lisa"); 
            Assert.IsNotNull(artworks);
            Assert.IsTrue(artworks.Count > 0);
        }


        [Test]
        public void Test_AddArtworkToFavorite()
        {
            bool result = galleryService.AddArtworkToFavorite(1, 4); 
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_RemoveArtworkFromFavorite()
        {
            bool result = galleryService.RemoveArtworkFromFavorite(1, 4);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_GetUserFavoriteArtworks()
        {
            List<Artwork> favorites = galleryService.GetUserFavoriteArtworks(1);
            Assert.IsNotNull(favorites);
            Assert.IsTrue(favorites.Count >= 0);
        }
    }
}

