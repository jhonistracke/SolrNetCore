using System;

namespace HttpWebAdaptersCore
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest Create(Uri url);
    }
}