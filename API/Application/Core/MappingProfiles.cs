using System;
using AutoMapper;
using API.Domain;

namespace API.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Grupo, Grupo>();
            CreateMap<Rol, Rol>();
            CreateMap<Proceso, Proceso>();
        }
    }
}