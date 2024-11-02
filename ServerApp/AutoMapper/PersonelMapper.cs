using AutoMapper;
using ServerApp.Entities;
using System;
using ServerApp.Protos;
namespace ServerApp.AutoMapper
{
    public class PersonelMapper : Profile
    {
        public PersonelMapper()
        {
            CreateMap<Personel, PersonelDetail>().ReverseMap();
        }
    }
}
