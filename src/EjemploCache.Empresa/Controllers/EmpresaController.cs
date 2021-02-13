using EjemploCache.Empresa.Dto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EjemploCache.Empresa.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        [HttpGet("{id}")]
        public EmpresaDto Get(int id)
        {
            if(id == 1)
            {
                return new EmpresaDto
                {
                    Ciudad = "Madrid",
                    Id = 1,
                    Nombre = "Empresa número 1",
                    Pais = "Españita"
                };
            }
            if (id == 3)
            {
                return new EmpresaDto
                {
                    Ciudad = "París",
                    Id = 2,
                    Nombre = "La segunda empresa",
                    Pais = "Francia"
                };
            }
            if (id == 2)
            {
                return new EmpresaDto
                {
                    Ciudad = "Londres",
                    Id = 1,
                    Nombre = "La empresa 3",
                    Pais = "Inglaterra"
                };
            }


            throw new NotImplementedException("este codigo es un ejemplo no funcional");

        }
    }
}
