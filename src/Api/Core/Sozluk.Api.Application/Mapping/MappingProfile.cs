using AutoMapper;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Features.Command.Entry.CreateEntry;
using Sozluk.Api.Application.Features.Command.EntryComment.CreateEntryComment;
using Sozluk.Api.Application.Features.Command.User.CreateUser;
using Sozluk.Api.Application.Features.Command.User.LoginUser;
using Sozluk.Api.Application.Features.Command.User.UpdateUser;
using Sozluk.Api.Application.Features.Queries.Entry.GetEntries;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<CreateUserDto, CreateUserCommandRequest>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();

            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, UpdateUserCommandRequest>().ReverseMap();

            CreateMap<User, UserDetailViewModel>().ReverseMap();
        
            CreateMap<Entry, CreateEntryCommandRequest>().ReverseMap();
            CreateMap<Entry, GetEntriesQueryResponse>()
                .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count))
                .ReverseMap();

            CreateMap<EntryComment, CreateEntryCommentCommandRequest>().ReverseMap();

        
        }
    }
}
