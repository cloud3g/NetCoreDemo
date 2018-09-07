using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewApi.AppModel;
using NewApi.Models;
using NewApi.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    public class GrupoController : Controller
    {
        private readonly NewApiContext _newApiContext;
        public GrupoAppModel _grupoAppModel;

        public GrupoController(NewApiContext context) {
            _newApiContext = context;
            _grupoAppModel = new GrupoAppModel(_newApiContext);
        }

        [HttpGet("{id}")]
        public Grupo Get(Guid id) {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IEnumerable<Pessoa> Get() {
            throw new NotImplementedException();
        }

        [HttpPost]
        public HttpResponseMessage Post(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public HttpResponseMessage Put(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
