

namespace server.SRC.Utils
{
    public class Constant
    {
        public readonly static int lengthId = 10;
        public readonly static string contentTypeImage = "image/png";
        public readonly static string imgTagRegex = @"<img\s+src=""data:image\/(jpeg|png|gif|bmp|webp|svg\+xml);base64,([^""]*)""\s+alt=""([^""]*)""\s*\/?>";
    }

    public class DefaultPath
    {
        public readonly static string userPath = "./Default/user.png";
    }

    public class RequestHeader
    {
        public readonly static string AUTHORIZATION_HEADER = "Authorization";
        public readonly static string AUTH_HEADER = "auth";
    }

    public class Message
    {
        public readonly static string INTERNAL_ERROR_SERVER = "Internel Error Server";
        public readonly static string INVALID_TOKEN = "Invalid Token";
        public readonly static string FORBIDDEN_CLIENT = "You are not the admin";
        public readonly static string INVALID_PATH = "Invalid Path";
        public readonly static string INVALID_REQUEST = "Invalid Request";
    }
}