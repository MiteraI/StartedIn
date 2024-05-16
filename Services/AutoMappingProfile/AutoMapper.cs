﻿using AutoMapper;
using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutoMappingProfile
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            MapRegister();
        }
        private void MapRegister()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
        
    }
}