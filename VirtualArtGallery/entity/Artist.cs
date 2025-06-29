using System;

namespace VirtualArtGallery.entity
{
    public class Artist
    {
        private int artistId;
        private string name;
        private string biography;
        private DateTime birthDate;
        private string nationality;
        private string website;
        private string contactInfo;

        public Artist() { }

        public Artist(int artistId, string name, string biography, DateTime birthDate, string nationality, string website, string contactInfo)
        {
            this.artistId = artistId;
            this.name = name;
            this.biography = biography;
            this.birthDate = birthDate;
            this.nationality = nationality;
            this.website = website;
            this.contactInfo = contactInfo;
        }

        public int ArtistId { get => artistId; set => artistId = value; }
        public string Name { get => name; set => name = value; }
        public string Biography { get => biography; set => biography = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        public string Website { get => website; set => website = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
    }
}
