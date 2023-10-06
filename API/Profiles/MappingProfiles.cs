using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles() // Remember adding : Profile in the class
    {
        CreateMap<Cita, CitaDto>().ReverseMap();

        CreateMap<Ciudad, CiudadDto>().ReverseMap();

        CreateMap<Cliente, ClienteDto>().ReverseMap();

        CreateMap<ClienteDireccion, ClienteDireccionDto>().ReverseMap();

        CreateMap<ClienteTelefono, ClienteTelefonoDto>().ReverseMap();

        CreateMap<Departamento, DepartamentoDto>().ReverseMap();

        CreateMap<Mascota, MascotaDto>().ReverseMap();

        CreateMap<Pais, PaisDto>().ReverseMap();

        CreateMap<Raza, RazaDto>().ReverseMap();

        CreateMap<Servicio, ServicioDto>().ReverseMap();
    }
}
