using AutoMapper;
using CardApplication.Business.Models;
using CardApplication.DataAccess.Models;

namespace CardApplication.Business
{
    public class MapperFunctions : Profile
    {
        public MapperFunctions()
        {
            CreateMap<CardDbModel, CardContract>();
            CreateMap<CardContract, CardDbModel>();
        }
    }
}
