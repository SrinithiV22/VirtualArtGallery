using System;
using System.Collections.Generic;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
using VirtualArtGallery.myexceptions;

namespace VirtualArtGallery.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            IVirtualArtGallery galleryService = new VirtualArtGalleryImpl();

            try
            {
                // 1. Add Artwork
                Artwork newArtwork = new Artwork(0,"Sample Artwork", "Sample Description", DateTime.Now, "Oil on Canvas", "https://example.com/sample.jpg", 1);
                bool addResult = galleryService.AddArtwork(newArtwork);
                Console.WriteLine("Artwork Added: " + addResult);

                // 2. Update Artwork
                Artwork updateArtwork = new Artwork(4, "Starry Night Updated", "Updated Description", DateTime.Now, "Oil on Canvas", "https://example.com/starrynight.jpg", 1);
                bool updateResult = galleryService.UpdateArtwork(updateArtwork);
                Console.WriteLine("Artwork Updated: " + updateResult);

                // 3. Get Artwork by ID
                Artwork foundArtwork = galleryService.GetArtworkById(4);
                Console.WriteLine("Artwork Found: " + foundArtwork.Title);

                // 4. Search Artworks
                List<Artwork> searchResults = galleryService.SearchArtworks("Starry");
                Console.WriteLine("Search Results:");
                foreach (var artwork in searchResults)
                {
                    Console.WriteLine($"Artwork ID: {artwork.ArtworkId}, Title: {artwork.Title}");
                }

                // 5. Remove Artwork
                bool removeResult = galleryService.RemoveArtwork(6); 
                Console.WriteLine("Artwork Removed: " + removeResult);

                // 6. Add to Favorite
                bool addFavorite = galleryService.AddArtworkToFavorite(1, 4);
                Console.WriteLine("Artwork added to favorites: " + addFavorite);

                // 7. Remove from Favorite
                bool removeFavorite = galleryService.RemoveArtworkFromFavorite(1, 4);
                Console.WriteLine("Artwork removed from favorites: " + removeFavorite);

                // 8. Get User's Favorite Artworks
                List<Artwork> userFavorites = galleryService.GetUserFavoriteArtworks(1);
                Console.WriteLine("User's Favorite Artworks:");
                foreach (var artwork in userFavorites)
                {
                    Console.WriteLine($"Artwork ID: {artwork.ArtworkId}, Title: {artwork.Title}");
                }
            }
            catch (ArtWorkNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Program Finished.");
            }

            Console.ReadLine();
        }
    }
}
