using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestProject1.Data
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
