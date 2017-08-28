using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;



/// <summary>
/// The FonySession allows a developer to fake up a HTTP context, so that controllers can operate against something that looks like a real session.
/// </summary>
namespace TitaniumBunker.PhonySession
{
    public class PhonyUploadFile : HttpPostedFileBase
    {
        private System.IO.MemoryStream _inputStream = new System.IO.MemoryStream();
        private string  _fileName;
        private string _contentType;
        public PhonyUploadFile(string Filename, System.IO.Stream ContentStream, string MIMEtype)
        {
            _fileName = Filename;
            ContentStream.CopyTo( _inputStream );
            _contentType = MIMEtype;
             
        }

        public override string ContentType
        {
            get
            {
                return _contentType;
            }
        }
        public override string FileName
        {
            get
            {
                return _fileName;
            }
        }
        public override System.IO.Stream InputStream
        {
            get
            {
                return _inputStream;
            }
        }
        public override int ContentLength
        {
            get
            {
                return (int)_inputStream.Length;
            }
        }

        public static explicit operator HttpPostedFile (PhonyUploadFile o)
        {
            var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var obj = (HttpPostedFile)constructorInfo
            .Invoke(new object[] { o.FileName, o.ContentType, o.InputStream });
            return obj;
        }
    }
    public class FonyHttpContext : HttpContextBase
    {
        private FonyRequest _request = new FonyRequest();
 

        public override HttpRequestBase Request
        {
            get 
            {
                return _request;
            }
        }
 
    }
    public class FonyRequest : HttpRequestBase
    {
        public override Uri Url
        {
            get
            {
                return new Uri("http://fake");
            }
        }

        public override string ApplicationPath
        {
            get
            {
                return "/";
            }
        }

        private FonyFileCollection __files = new FonyFileCollection ();
        public override HttpFileCollectionBase Files
        {
            get
            {
                return __files;
            }
        }
        public void AddFile(PhonyUploadFile file)
        {
            __files.AddFonyFile(file);
        }
    }
    public class FonyFileCollection : HttpFileCollectionBase
    {
        private   NameValueCollection keyCollection = new NameValueCollection();
        List<HttpPostedFileBase> _files = new List<HttpPostedFileBase>();
        //Dictionary <string,HttpPostedFileBase> _files = new Dictionary<string,HttpPostedFileBase>();

        public override void CopyTo(Array dest, int index)
        {
            var x = _files.ToArray();
            x.CopyTo(dest, index);
        }
        public override int Count
        {
            get
            {
                return _files.Count();
            }
        }
        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _files.GetEnumerator();
        }
        public override HttpPostedFileBase Get(int index)
        {
            return _files[index];

        }

        public override KeysCollection Keys
        {
            get { return keyCollection.Keys; }
        }
        public override HttpPostedFileBase this[int index]
        {
            get
            {
                return _files[index];
             }
        }




        public void AddFonyFile(PhonyUploadFile file)
        {
            _files.Add(file );
        }
    }
    public class FonyKeyCollection  : ICollection, IEnumerable
    {
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class FonySession
    {
        public FonySession()
        {
            httpContext = new FonyHttpContext();

        }

        public FonyHttpContext httpContext { get; set; }

        public void AddFileUpload(PhonyUploadFile FileToUpload)
        {
            var f = (FonyRequest)this.httpContext.Request ;
            f.AddFile(FileToUpload);

        }
        public RequestContext BuildRequestContext()
        {
            return new RequestContext(httpContext, new RouteData());

        }
        public ControllerContext BuildControllerContext(ControllerBase Controller)
        {
            ControllerContext c = new ControllerContext(httpContext, new RouteData() ,Controller);

            return c;


        }

 
    }
}
