using AutoMapper;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;

namespace ToDoApp.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddTaskDto, TaskUser>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ReturnTaskDto, TaskUser>().ReverseMap();
        CreateMap<TaskBaseDto, TaskUser>().ReverseMap();
        CreateMap<ReturnUserDto, User>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<AddCategoryDto, Category>().ReverseMap();
        CreateMap<TaskUser, MainTaskDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

    }
}
