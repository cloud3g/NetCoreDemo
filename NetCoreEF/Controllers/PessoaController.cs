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
    public class PessoaController : Controller
    {
        private readonly NewApiContext _newApiContext;
        public PessoaAppModel _pessoaAppModel;

        public PessoaController(NewApiContext context) {
            _newApiContext = context;
            _pessoaAppModel = new PessoaAppModel(_newApiContext);
        }

        [HttpGet("{id}")]
        public Pessoa Get(Guid id) {
            return _pessoaAppModel.GetPessoa(id);
        }

        [HttpGet]
        public IEnumerable<Pessoa> Get() {
            var retorno = _pessoaAppModel.ListaPessoas();
            return retorno;
        }

        [HttpPost]
        public HttpResponseMessage Post(Pessoa pessoa)
        {
            try {
                if(ModelState.IsValid)
                    _pessoaAppModel.AddPessoa(pessoa);
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }catch {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(Pessoa pessoa)
        {
            try {
                if(ModelState.IsValid)
                    _pessoaAppModel.UpdatePessoa(pessoa);
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            
                return new HttpResponseMessage(HttpStatusCode.OK);
            }catch {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
