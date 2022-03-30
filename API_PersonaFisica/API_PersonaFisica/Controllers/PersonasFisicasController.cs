using API_PersonaFisica.Data;
using API_PersonaFisica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_PersonaFisica.Controllers
{
    public class PersonasFisicasController : ApiController
    {
        // GET api/<controller>
        public List<PersonasFisicas> Get()
        {
            return PersonasFisicasData.ConsultarPersonasFisicas();
        }

        // GET api/<controller>/5
        public PersonasFisicas Get(int id)
        {
            return PersonasFisicasData.ConsultarUnaPersona(new PersonasFisicas { IdPersonaFisica = id });
        }

        // POST api/<controller>
        public DB_Result Post([FromBody]PersonasFisicas Persona)
        {
            return PersonasFisicasData.Registrar(Persona);
        }

        // PUT api/<controller>/5
        public DB_Result Put([FromBody]PersonasFisicas Persona)
        {
            return PersonasFisicasData.Actualizar(Persona);
        }

        // DELETE api/<controller>/5
        public DB_Result Delete(int id)
        {
            return PersonasFisicasData.Eliminar(new PersonasFisicas { IdPersonaFisica = id });
        }
    }
}