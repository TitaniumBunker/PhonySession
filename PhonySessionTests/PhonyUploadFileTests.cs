using NUnit.Framework;
using TitaniumBunker.PhonySession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhonySessionTests.Properties;
using System.IO;
using System.Drawing.Imaging;
using System.Web;

namespace TitaniumBunker.PhonySession.Tests
{
    [TestFixture()]
    public class PhonyUploadFileTests
    {
 


        [Test()]
        public void PhonyUploadFileTest()
        {   FonyFileCollection fc = new FonyFileCollection();
            MemoryStream stream = new MemoryStream();
            Resources.phony_logo.Save(stream, ImageFormat.Bmp);

            fc.AddFonyFile(new PhonyUploadFile("FooBar", stream, "png"));
            HttpPostedFileBase firstFile10 = fc[0];
          

            foreach (var item in fc)
            {

            }


        }
    }
}