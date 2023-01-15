using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PE.TabelaFipe.Repository.Models.Exceptions
{
    public  class ErrorRequest : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public object DadosErro { get; set; }

        public ErrorRequest(string mensagemErro, HttpStatusCode httpStatusCode, object dadosErro)
            : base(mensagemErro)
        {
            HttpStatusCode = httpStatusCode;
            DadosErro = dadosErro;
        }
    }
}
