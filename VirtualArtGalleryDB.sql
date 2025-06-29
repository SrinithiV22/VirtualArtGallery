use virtualartgallery;

-- Table: Artist
create table artist (
    artistid int primary key identity(1,1),
    name nvarchar(100),
    biography text,
    birthdate date,
    nationality nvarchar(50),
    website nvarchar(100),
    contactinfo nvarchar(100)
);

-- Table: Artwork
create table artwork (
    artworkid int primary key identity(1,1),
    title nvarchar(100),
    description text,
    creationdate date,
    medium nvarchar(50),
    imageurl nvarchar(200),
    artistid int,
    foreign key (artistid) references artist(artistid)
);

-- Table: [User]
create table [user] (
    userid int primary key identity(1,1),
    username nvarchar(50),
    [password] nvarchar(50),
    email nvarchar(100),
    firstname nvarchar(50),
    lastname nvarchar(50),
    dateofbirth date,
    profilepicture nvarchar(200)
);

-- Table: Gallery
create table gallery (
    galleryid int primary key identity(1,1),
    name nvarchar(100),
    description text,
    location nvarchar(100),
    curatorid int,
    openinghours nvarchar(100),
    foreign key (curatorid) references artist(artistid)
);

-- Junction Table: User_Favorite_Artwork
create table user_favorite_artwork (
    userid int,
    artworkid int,
    primary key (userid, artworkid),
    foreign key (userid) references [user](userid),
    foreign key (artworkid) references artwork(artworkid)
);

-- Junction Table: Artwork_Gallery
create table artwork_gallery (
    artworkid int,
    galleryid int,
    primary key (artworkid, galleryid),
    foreign key (artworkid) references artwork(artworkid),
    foreign key (galleryid) references gallery(galleryid)
);

INSERT INTO artist ( name, biography, birthDate, nationality, website, contactinfo)
VALUES 
('Vincent van Gogh', 'Dutch post-impressionist painter', '1853-03-30', 'Dutch', 'https://vangogh.com', 'vangogh@example.com'),
('Leonardo da Vinci', 'Italian polymath of the Renaissance', '1452-04-15', 'Italian', 'https://davinci.com', 'davinci@example.com'),
('Frida Kahlo', 'Mexican painter known for self-portraits', '1907-07-06', 'Mexican', 'https://frida.com', 'frida@example.com');

INSERT INTO artwork ( title, description, creationdate, medium, imageurl)
VALUES 
('Starry Night', 'Famous painting by van Gogh', '1889-06-01', 'Oil on Canvas', 'https://example.com/starrynight.jpg'),
('Mona Lisa', 'Portrait by Leonardo da Vinci', '1503-01-01', 'Oil on Wood', 'https://example.com/monalisa.jpg'),
('The Two Fridas', 'Double self-portrait by Frida Kahlo', '1939-01-01', 'Oil on Canvas', 'https://example.com/twofridas.jpg');

INSERT INTO [user] ( username, password, email, firstname, lastname, dateofbirth, profilepicture)
VALUES 
('artlover1', 'pass123', 'artlover1@example.com', 'John', 'Doe', '1990-05-15', 'https://example.com/john.jpg'),
('artfanatic', 'pass456', 'artfanatic@example.com', 'Jane', 'Smith', '1995-08-22', 'https://example.com/jane.jpg');

INSERT INTO User_Favorite_Artwork (userid, artworkid)
VALUES 
(1, 4),
(2, 6),
(1 ,5); 

INSERT INTO gallery ( name, description, location, curatorid, openinghours)
VALUES 
( 'Classic Masters', 'A collection of classic masterpieces', 'Paris', 2, '9 AM - 5 PM'),
( 'Modern Visions', 'Modern and abstract art', 'New York', 3, '10 AM - 6 PM');

INSERT INTO Artwork_Gallery (artworkid, galleryid)
VALUES 
(4, 2), 
(5, 2), 
(6, 3); 

UPDATE artwork
SET artistid = 1
WHERE artworkid = 4; 

UPDATE artwork
SET artistid = 2
WHERE artworkid = 5;

UPDATE artwork
SET artistid = 3
WHERE artworkid = 6;

