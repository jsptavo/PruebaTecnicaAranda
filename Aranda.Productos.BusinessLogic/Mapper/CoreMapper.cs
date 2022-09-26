using Aranda.Productos.BusinessLogic.EntitiesDomain;
using Aranda.Productos.Persistence.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.BusinessLogic.Mapper
{
    public class CoreMapper : Profile
    {
        public CoreMapper()
        {
            CreateMap<ProductoDomain, Producto>().ReverseMap();
        }
        
    }
}
