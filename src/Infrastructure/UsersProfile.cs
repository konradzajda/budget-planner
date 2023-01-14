using AutoMapper;
using FirebaseAdmin.Auth;
using Tivix.BudgetPlanner.Application.Entities;
using Tivix.BudgetPlanner.Application.ViewModels;

namespace Tivix.BudgetPlanner.Infrastructure;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<ExportedUserRecord, UserViewModel>()
            .ForMember(y => y.Id, e => e.MapFrom(s => s.Uid))
            .ForMember(y => y.Email, e => e.MapFrom(s => s.Email));

    }
}