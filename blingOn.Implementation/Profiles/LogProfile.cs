using AutoMapper;
using blingOn.Application.DTOs;
using blingOn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Profiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<UseCaseLog, LogDto>();
        }
    }
}
