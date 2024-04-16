using AutoMapper;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Create;
using ETıcaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.AppUsers.Profiles
{
    public class AppUserMappingProfile:Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<CreateUserCommandRequest, AppUser>().ReverseMap();

                //.ForMember(destinationMember: a => a.NameSurname, memberOptions: opt => opt.MapFrom(a => a.NameSurname))
                //.ForMember(destinationMember: a => a.UserName, memberOptions: opt => opt.MapFrom(a => a.UserName))
                //.ForMember(destinationMember: a => a.Email, memberOptions: opt => opt.MapFrom(a => a.Email));
                
        }
    }
}
