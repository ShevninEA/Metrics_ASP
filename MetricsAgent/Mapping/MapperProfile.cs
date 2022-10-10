using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;

namespace MetricsAgent.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CpuMetric
            CreateMap<CpuMetricCreateRequest, CpuMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<CpuMetric, CpuMetricDto>();

            //DotnetMetric
            CreateMap<DotnetMetricCreateRequest, DotnetMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<DotnetMetric, DotnetMetricDto>();

            //HddMetric
            CreateMap<HddMetricCreateRequest, HddMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<HddMetric, HddMetricDto>();

            //NetworkMetric
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<NetworkMetric, NetworkMetricDto>();

            //RamworkMetric
            CreateMap<RamMetricCreateRequest, RamMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}
