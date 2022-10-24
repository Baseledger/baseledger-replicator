using AutoMapper;
using Microsoft.AspNetCore.Identity;
using baseledger_replicator.DTOs.Auth;

namespace baseledger_replicator.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IdentityUser, LoggedUserDto>();
    }
}