

namespace server.SRC.Utils
{
    public class Constant
    {
        public readonly static int lengthId = 10;
    }

    public class RequestHeader
    {
        public readonly static string AUTHORIZATION_HEADER = "Authorization";
    }

    public class Message
    {
        public readonly static string INTERNAL_ERROR_SERVER = "Internel Error Server";
        public readonly static string INVALID_TOKEN = "Invalid Token";
        public readonly static string FORBIDDEN_CLIENT = "You are not the admin";
    }
}