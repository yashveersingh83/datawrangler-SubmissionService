using AutoMapper;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MileStone, MileStoneDto>().ReverseMap(); 
        }
    }
}
