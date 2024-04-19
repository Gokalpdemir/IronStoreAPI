using AutoMapper;
using ETıcaretAPI.Application.Dtos.User;
using ETıcaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Profiles.User
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDto,AppUser>().ReverseMap();
        }
    }
}
