using AutoMapper;
using Booky.DAL.Models;
using Booky.PL.ViewModels;

namespace Booky.PL.MappingProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UsersViewModel>().ReverseMap();
        }
    }
}
