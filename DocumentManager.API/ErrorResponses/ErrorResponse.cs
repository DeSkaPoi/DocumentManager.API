using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManager.API.ErrorResponses
{
    public class ErrorResponse
    {
        public string ErrorMesage { get; set; }
        public ErrorResponse(string errorMesage)
        {
            ErrorMesage = errorMesage;
        }
    }
}
