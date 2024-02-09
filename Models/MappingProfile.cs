using AutoMapper;
using MilitiaDuty.Models.Assignments;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Militias;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Militia, MilitiaDto>();
            CreateMap<MilitiaDto, Militia>();
            CreateMap<DutyDate, DutyDateDto>();
            CreateMap<DutyDateDto, DutyDate>();
            CreateMap<Assignments.Task, TaskDto>();
            CreateMap<TaskDto, Assignments.Task>();
            CreateMap<Mission, MissionDto>();
            CreateMap<MissionDto, Mission>();
            CreateMap<Rule, RuleDto>();
            CreateMap<RuleDto, Rule>();
            CreateMap<Shift, ShiftDto>()
                .ForMember(dto => dto.DutyDate, options => options.MapFrom(s => s.DutyDate != null ? s.DutyDate.Date : (DateTime?)null))
                .ForMember(dto => dto.Militia, options => options.MapFrom(s => s.Militia != null ? s.Militia.Name : null));
            CreateMap<ShiftDto, Shift>()
                .ForMember(s => s.DutyDate, options => options.Ignore())
                .ForMember(s => s.Militia, options => options.Ignore());
        }
    }
}
