using AutoMapper;
using Booky.DAL.Models;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Booky.PL.MappingProfile
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RolesViewModel>().ReverseMap();
            Console.WriteLine("RoleProfile is registered!");
        }
    }
}
