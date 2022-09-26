using Aranda.Productos.Api.Dto;
using Aranda.Productos.BusinessLogic.EntitiesDomain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Productos.Api.Mapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {            
            CreateMap<ProductoDTO, ProductoDomain>().ReverseMap();
            
            CreateMap<ProductoAddImagenDTO, ProductoDomain>().ForMember(x => x.Imagen, o => o.Ignore());
        }

    }
}
