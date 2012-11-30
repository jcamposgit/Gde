#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System.IO;
using System.IO.Compression;

namespace System.Web.Mvc
{
    public static class ControllerExt
    {
        /// <summary>
        /// Sets the response expiration to 5 years from now.
        /// </summary>
        public static void SetFarFutureExpire(this Controller controller)
        {
            controller.Response.ExpiresAbsolute = DateTime.Now.AddYears(5);
        }

        /// <summary>
        /// Compresses the Response to GZIP or DEFLATE 
        /// if GZIP or DEFLATE are accepted
        /// </summary>
        public static void Compress(this Controller controller)
        {
            var Response = controller.Response;
            var Request = controller.Request;
            Stream filter = Response.Filter;

            // if the stream is already GZip or Deflate
            // there is no need to re-optimize
            if (filter == null || filter is GZipStream || filter is DeflateStream)
                return;

            string gzip = "gzip";
            string deflate = "deflate";

            Func<string, bool> isAccepted = (encoding) => Request.Headers["Accept-encoding"] != null &&
                                                          Request.Headers["Accept-encoding"].Contains(encoding);

            Action<string> setEncoding = (encoding) => Response.AppendHeader("Content-encoding", encoding);

            if (isAccepted(gzip))
            {
                Response.Filter = new GZipStream(filter, CompressionMode.Compress);
                setEncoding(gzip);
            }
            else if (isAccepted(deflate))
            {
                Response.Filter = new DeflateStream(filter, CompressionMode.Compress);
                setEncoding(deflate);
            }
        }
    }
}