using System;
using Microsoft.AspNetCore.Http.Headers;

namespace WebApi3
{
    public class WebAppData
    {
        public int Guid { get; set; }
        public Nullable<DateTimeOffset> LastModified { get; set; }
        public string UserData { get; set; }
    }
}

