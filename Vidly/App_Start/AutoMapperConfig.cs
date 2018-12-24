using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<MembershipType, MembershipTypeDto>();
                cfg.CreateMap<MovieDto, Movie>();
                cfg.CreateMap<Movie, MovieDto>();
                cfg.CreateMap<RentalDto, Rental>();
                cfg.CreateMap<Rental, RentalDto>();
            });
        }
    }
}