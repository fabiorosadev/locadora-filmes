using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Data.Migrations
{
    public partial class SeedGenresAndMoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Adicionando Gêneros
            migrationBuilder
                .Sql("INSERT INTO Genres (Name) Values ('Ação')");
            migrationBuilder
                .Sql("INSERT INTO Genres (Name) Values ('Aventura')");
            migrationBuilder
                .Sql("INSERT INTO Genres (Name) Values ('Comédia')");
            migrationBuilder
                .Sql("INSERT INTO Genres (Name) Values ('Ficção Cinetífica')");
            migrationBuilder
                .Sql("INSERT INTO Genres (Name) Values ('Musical')");

            //Adicionando Filmes
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('Vingadores Ultimato', (SELECT Id FROM Genres WHERE Name = 'Ação'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('John Wick 3', (SELECT Id FROM Genres WHERE Name = 'Ação'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('Toy Story 4', (SELECT Id FROM Genres WHERE Name = 'Aventura'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('Aladdin', (SELECT Id FROM Genres WHERE Name = 'Aventura'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('Parasita', (SELECT Id FROM Genres WHERE Name = 'Comédia'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('Star Wars: Episódio IX', (SELECT Id FROM Genres WHERE Name = 'Ficção Cinetífica'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Movies (Name, GenreId, Status) Values ('O Rei Leão', (SELECT Id FROM Genres WHERE Name = 'Musical'), 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Movies");
            migrationBuilder
                .Sql("DELETE FROM Genres");
        }
    }
}
