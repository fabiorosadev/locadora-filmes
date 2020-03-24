using System;
using AutoMapper;
using MovieStore.Services.Genres.Dto;
using MovieStore.Services.MovieRentals.Dto;
using MovieStore.Services.Movies.Dto;
using MovieStore.Services.Rentals.Dto;
using MovieStore.Services.Users.Dto;

namespace MovieStore.Services
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MovieMapProfile>();
                cfg.AddProfile<GenreMapProfile>();
                cfg.AddProfile<MovieRentalMapProfile>();
                cfg.AddProfile<UserMapProfile>();
                cfg.AddProfile<RentalMapProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}