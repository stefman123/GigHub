using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

    //var config = new MapperConfiguration( cfg => { 
    //            cfg.CreateMap<ApplicationUser, UserDto>();
    //            cfg.CreateMap<Genre, GenreDto>();
    //            cfg.CreateMap<Gig, GigDto>();
    //            cfg.CreateMap<Notification, NotificationDto>();
    //        });

            CreateMap<ApplicationUser, UserDto>();
            CreateMap <Genre, GenreDto>();
            CreateMap <Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();


        }
    }
}