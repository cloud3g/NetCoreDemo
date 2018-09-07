
using System;
using System.Collections.Generic;
using System.Linq;
using NewApi.Models;
using NewApi.Data;

namespace NewApi.AppModel {
    public class PessoaAppModel
    {
        private readonly NewApiContext _newApiContext;
        private PessoaRepository _pessoaRepository;

        public PessoaAppModel(NewApiContext context) {
            _newApiContext = context;
            _pessoaRepository = new PessoaRepository(_newApiContext);
        }
        public Pessoa GetPessoa(Guid id)
        {
            return _pessoaRepository.Get(id);
        }

        public List<Pessoa> ListaPessoas()
        {
            return _pessoaRepository.List().ToList();
        }

        public void AddPessoa(Pessoa obj)
        {
            try {
                _pessoaRepository.Add(obj);
                _pessoaRepository.Commit();
            }catch {
                throw;
            }

        }

        public void UpdatePessoa(Pessoa obj)
        {
            try {
                _pessoaRepository.UpdateData(obj);
                _pessoaRepository.Commit();
            }catch {
                throw;
            }
        }

        public void Dispose() {
            _pessoaRepository = null;
        }
    }
}