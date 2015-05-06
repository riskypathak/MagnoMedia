using System;
using System.Collections.Generic;
using System.Text;
using MyDownloader;
using System.IO;

namespace VidSoom
{
    class HttpVideo : HttpServer
    {
            public HttpVideo(int port)
                : base(port)
            {
            }
            public static void WriteTo(Stream sourceStream, StreamWriter targetStream)
            {
                byte[] buffer = new byte[0x10000];
                int n;
                while ((n = sourceStream.Read(buffer, 0, buffer.Length)) != 0)
                    targetStream.Write(buffer);
            }

            public override void handleGETRequest(HttpProcessor p)
            {
                Console.WriteLine("request: {0}", p.http_url);
                p.writeSuccess();
                HttpVideo.WriteTo(frmMain.PreviewDownload.Segments[0].OutputStream, p.outputStream);
            }

            public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
            {
                Console.WriteLine("POST request: {0}", p.http_url);
                string data = inputData.ReadToEnd();

                p.outputStream.WriteLine("<html><body><h1>test server</h1>");
                p.outputStream.WriteLine("<a href=/test>return</a><p>");
                p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
            }
        }
}
