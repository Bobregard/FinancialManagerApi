using AutoMapper;
using FinancialManager.Core.DTOs;
using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Core.Mappings
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();

            CreateMap<LocationCreateDto, Location>();
        }
    }
}
