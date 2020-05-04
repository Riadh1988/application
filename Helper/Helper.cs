using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Consume.Helper
{
    public class MyProjectAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://montasksapi.azurewebsites.net/");
            return client;
        }
    }
}