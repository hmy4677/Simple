﻿using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Mapper
{
    public class Mapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.ForType<Entity, Dto>()
            //      .Map(dest => dest.FullName, src => src.FirstName + src.LastName)
            //      .Map(dest => dest.IdCard, src => src.IdCard.Replace("1234", "****"));
        }
    }
}
