using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Tarpe23CristianTestingXUnit.Mock
{
    public class MockIHostEnvoirnment : IHostEnvironment
    {

        public string EnvironmentName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string ContentRootPath
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IFileProvider ContentRootFileProvider
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        IFileProvider IHostEnvironment.ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
