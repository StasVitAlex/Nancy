using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db = ToDoList.DataAccessLayer.Models;
using dto = ToDoList.BusinessLogic.Core.DTO;

namespace ToDoList.BusinessLogic.Core
{
    internal class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                _mapper = InitMapper();
            }

            return _mapper;
        }

        private static IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<db.ToDo, dto.ToDoDto>();
            });

            return config.CreateMapper();
        }
    }
}
