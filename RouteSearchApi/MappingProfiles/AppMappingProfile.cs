using AutoMapper;
using RouteSearchApi.Models;
using RouteSearchApi.Models.ProviderOne;
using RouteSearchApi.Models.ProviderTwo;
using Route = RouteSearchApi.Models.Route;

namespace RouteSearchApi.MappingProfiles;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<SearchRequest, ProviderOneSearchRequest>()
            .ForMember(request => request.From, opt => opt.MapFrom(x => x.Origin))
            .ForMember(request => request.To, opt => opt.MapFrom(x => x.Destination))
            .ForMember(request => request.DateFrom, opt => opt.MapFrom(x => x.OriginDateTime))
            .ForMember(request => request.DateTo, opt => opt.MapFrom(x => x.Filters != null ? x.Filters.DestinationDateTime : null))
            .ForMember(request => request.MaxPrice, opt => opt.MapFrom(x => x.Filters != null ? x.Filters.MaxPrice : null));

        CreateMap<SearchRequest, ProviderTwoSearchRequest>()
            .ForMember(request => request.Departure, opt => opt.MapFrom(x => x.Origin))
            .ForMember(request => request.Arrival, opt => opt.MapFrom(x => x.Destination))
            .ForMember(request => request.DepartureDate, opt => opt.MapFrom(x => x.OriginDateTime))
            .ForMember(request => request.MinTimeLimit, opt => opt.MapFrom(x => x.Filters != null ? x.Filters.MinTimeLimit : null));

        CreateMap<ProviderOneRoute, Route>()
            .ForMember(route => route.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(route => route.Origin, opt => opt.MapFrom(x => x.From))
            .ForMember(route => route.Destination, opt => opt.MapFrom(x => x.To))
            .ForMember(route => route.OriginDateTime, opt => opt.MapFrom(x => x.DateFrom))
            .ForMember(route => route.DestinationDateTime, opt => opt.MapFrom(x => x.DateTo));

        CreateMap<ProviderTwoRoute, Route>()
            .ForMember(route => route.Id, opt => opt.MapFrom(x => new Guid()))
            .ForMember(route => route.Origin, opt => opt.MapFrom(x => x.Departure.Point))
            .ForMember(route => route.Destination, opt => opt.MapFrom(x => x.Arrival.Point))
            .ForMember(route => route.OriginDateTime, opt => opt.MapFrom(x => x.Departure.Date))
            .ForMember(route => route.DestinationDateTime, opt => opt.MapFrom(x => x.Arrival.Date));
    }
}
