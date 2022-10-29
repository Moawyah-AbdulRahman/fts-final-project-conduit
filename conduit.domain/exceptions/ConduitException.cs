using conduit.domain.models;

namespace conduit.domain.exceptions;

[Serializable]
public class ConduitException : Exception
{
    public HttpResponseCode ResponseCode { get; }
    public string Massage { get; }

    public ConduitException(string massage, HttpResponseCode responseCode) : base(massage)
    {
        Massage = massage;
        ResponseCode = responseCode;
    }

}