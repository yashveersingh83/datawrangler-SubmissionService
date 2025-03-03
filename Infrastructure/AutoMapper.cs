using AutoMapper;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MileStone, MileStoneDto>().ReverseMap(); 
            CreateMap<OrganizationalUnitHead, OrganizationalUnitHeadDto>().ReverseMap(); 
            CreateMap<Recipient,RecipientDto>().ReverseMap(); 
            CreateMap<InformationRequest,InformationRequestDto>().ReverseMap(); 
        }
    }
}
