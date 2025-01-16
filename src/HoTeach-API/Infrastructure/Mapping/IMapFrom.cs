using AutoMapper;

namespace HoTeach.API.Infrastructure.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType());
    }
}
