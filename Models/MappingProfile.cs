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
            CreateMap<DutyDate, DutyDateDto>()
                .ForMember(d => d.Date, options => options.MapFrom(dto => dto.Date.Date));
            CreateMap<DutyDateDto, DutyDate>()
                .ForMember(dto => dto.Date, options => options.MapFrom(d => d.Date.Date));
            CreateMap<Assignments.Task, TaskDto>();
            CreateMap<TaskDto, Assignments.Task>();
            CreateMap<Mission, MissionDto>();
            CreateMap<MissionDto, Mission>();
            CreateMap<Rule, RuleDto>()
                .ForMember(r => r.StartDate, options => options.MapFrom(dto => dto.StartDate.Date))
                .ForMember(r => r.EndDate, options => options.MapFrom(dto => dto.EndDate.HasValue ? dto.EndDate.Value.Date : (DateTime?)null));
            CreateMap<RuleDto, Rule>()
                .ForMember(dto => dto.StartDate, options => options.MapFrom(r => r.StartDate.Date))
                .ForMember(dto => dto.EndDate, options => options.MapFrom(r => r.EndDate.HasValue ? r.EndDate.Value.Date : (DateTime?)null));
            CreateMap<Shift, ShiftDto>()
                .ForMember(dto => dto.DutyDate, options => options.MapFrom(s => s.DutyDate != null ? s.DutyDate.Date : (DateTime?)null))
                .ForMember(dto => dto.Militia, options => options.MapFrom(s => s.Militia != null ? s.Militia.Name : null));
            CreateMap<ShiftDto, Shift>()
                .ForMember(s => s.DutyDate, options => options.Ignore())
                .ForMember(s => s.Militia, options => options.Ignore());
        }
    }
}
