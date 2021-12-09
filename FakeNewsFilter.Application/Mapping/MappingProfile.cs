using System;
using AutoMapper;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Catalog.Media;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Media, MediaViewModel>()
                .ForMember(d => d.MediaId, o => o.MapFrom(s => s.MediaId))
                 .ForMember(d => d.Caption, o => o.MapFrom(s => s.Caption))
                  .ForMember(d => d.PathMedia, o => o.MapFrom(s => s.PathMedia))
                   .ForMember(d => d.FileSize, o => o.MapFrom(s => s.FileSize))
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                      .ForMember(d => d.DateCreated, o => o.MapFrom(s => s.DateCreated));

            CreateMap<TopicNews, TopicViewModel>()
                .ForMember(d => d.TopicId, o => o.MapFrom(s => s.TopicId))
                 .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                  .ForMember(d => d.Label, o => o.MapFrom(s => s.Label))
                   .ForMember(d => d.Media, o => o.MapFrom(s => s.Media))
                    .ForMember(d => d.Tag, o => o.MapFrom(s => s.Tag))
                     .ForMember(d => d.Timestamp, o => o.MapFrom(s => s.Timestamp))
                ;
            CreateMap<User, UserViewModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                  .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                   .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
                    
                ;
            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.CommentId, o => o.MapFrom(s => s.CommentId))
                .ForMember(x => x.Content, o => o.MapFrom(s => s.Content))
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(x => x.ParentId, o => o.MapFrom(s => s.ParentId))
                .ForMember(x => x.NewsId, o => o.MapFrom(s => s.NewsId))
                .ForMember(x => x.Timestamp, o => o.MapFrom(s => s.Timestamp));
        }
    }
}