using AutoMapper;
using BokAdventure.Application.Users.Dtos;
using BokAdventure.Domain.Entities;

namespace BokAdventure.Application.Users;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, AccountDto>();
    }
}
