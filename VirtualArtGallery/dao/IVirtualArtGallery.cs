using System.Collections.Generic;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.dao
{
    public interface IVirtualArtGallery
    {
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkId);
        Artwork GetArtworkById(int artworkId);
        List<Artwork> SearchArtworks(string keyword);
        bool AddArtworkToFavorite(int userId, int artworkId);
        bool RemoveArtworkFromFavorite(int userId, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(int userId);
    }
}

