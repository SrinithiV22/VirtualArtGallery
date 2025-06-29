using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VirtualArtGallery.entity;
using VirtualArtGallery.myexceptions;
using VirtualArtGallery.util;

namespace VirtualArtGallery.dao
{
    public class VirtualArtGalleryImpl : IVirtualArtGallery
    {
        public bool AddArtwork(Artwork artwork)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO artwork (title, description, creationdate, medium, imageurl, artistid) VALUES (@title, @description, @creationdate, @medium, @imageurl, @artistid)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@title", artwork.Title);
                cmd.Parameters.AddWithValue("@description", artwork.Description);
                cmd.Parameters.AddWithValue("@creationdate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@imageurl", artwork.ImageUrl);
                cmd.Parameters.AddWithValue("@artistid", artwork.ArtistId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE artwork SET title=@title, description=@description, creationdate=@creationdate, medium=@medium, imageurl=@imageurl, artistid=@artistid WHERE artworkid=@artworkid";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@title", artwork.Title);
                cmd.Parameters.AddWithValue("@description", artwork.Description);
                cmd.Parameters.AddWithValue("@creationdate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@imageurl", artwork.ImageUrl);
                cmd.Parameters.AddWithValue("@artistid", artwork.ArtistId);
                cmd.Parameters.AddWithValue("@artworkid", artwork.ArtworkId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM user_favorite_artwork WHERE userid = @userid AND artworkid = @artworkid";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@userid", userId);
                checkCmd.Parameters.AddWithValue("@artworkid", artworkId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                    return false; // Already exists, don't insert again

                string query = "INSERT INTO user_favorite_artwork (userid, artworkid) VALUES (@userid, @artworkid)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@artworkid", artworkId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }


        public Artwork GetArtworkById(int artworkId)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM artwork WHERE artworkid=@artworkid";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@artworkid", artworkId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Artwork artwork = new Artwork
                    {
                        ArtworkId = (int)reader["artworkid"],
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        CreationDate = Convert.ToDateTime(reader["creationdate"]),
                        Medium = reader["medium"].ToString(),
                        ImageUrl = reader["imageurl"].ToString(),
                        ArtistId = (int)reader["artistid"]
                    };
                    reader.Close();
                    return artwork;
                }
                else
                {
                    reader.Close();
                    throw new ArtWorkNotFoundException("Artwork ID not found");
                }
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM artwork WHERE title LIKE @keyword OR description LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                List<Artwork> artworks = new List<Artwork>();

                while (reader.Read())
                {
                    Artwork artwork = new Artwork
                    {
                        ArtworkId = (int)reader["artworkid"],
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        CreationDate = Convert.ToDateTime(reader["creationdate"]),
                        Medium = reader["medium"].ToString(),
                        ImageUrl = reader["imageurl"].ToString(),
                        ArtistId = (int)reader["artistid"]
                    };
                    artworks.Add(artwork);
                }
                reader.Close();
                return artworks;
            }
        }

        public bool RemoveArtwork(int artworkId)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                // Step 1: Remove from favorites
                string deleteFavoriteQuery = "DELETE FROM user_favorite_artwork WHERE artworkid = @artworkid";
                SqlCommand deleteFavoriteCmd = new SqlCommand(deleteFavoriteQuery, connection);
                deleteFavoriteCmd.Parameters.AddWithValue("@artworkid", artworkId);
                deleteFavoriteCmd.ExecuteNonQuery();

                // Step 2: Remove from artwork_gallery
                string deleteGalleryQuery = "DELETE FROM artwork_gallery WHERE artworkid = @artworkid";
                SqlCommand deleteGalleryCmd = new SqlCommand(deleteGalleryQuery, connection);
                deleteGalleryCmd.Parameters.AddWithValue("@artworkid", artworkId);
                deleteGalleryCmd.ExecuteNonQuery();

                // Step 3: Remove the artwork
                string deleteArtworkQuery = "DELETE FROM artwork WHERE artworkid = @artworkid";
                SqlCommand deleteArtworkCmd = new SqlCommand(deleteArtworkQuery, connection);
                deleteArtworkCmd.Parameters.AddWithValue("@artworkid", artworkId);

                int result = deleteArtworkCmd.ExecuteNonQuery();
                if (result == 0)
                    throw new ArtWorkNotFoundException("Artwork ID not found");

                return true;
            }
        }



        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM user_favorite_artwork WHERE userid=@userid AND artworkid=@artworkid";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@artworkid", artworkId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT a.* FROM artwork a JOIN user_favorite_artwork ufa ON a.artworkid = ufa.artworkid WHERE ufa.userid = @userid";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userid", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                List<Artwork> artworks = new List<Artwork>();

                while (reader.Read())
                {
                    Artwork artwork = new Artwork
                    {
                        ArtworkId = (int)reader["artworkid"],
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        CreationDate = Convert.ToDateTime(reader["creationdate"]),
                        Medium = reader["medium"].ToString(),
                        ImageUrl = reader["imageurl"].ToString(),
                        ArtistId = (int)reader["artistid"]
                    };
                    artworks.Add(artwork);
                }
                reader.Close();

                if (artworks.Count == 0)
                    throw new UserNotFoundException("No favorites found or User ID not found");

                return artworks;
            }
        }
    }
}
