namespace server.SRC.Utils
{
    public class Library
    {
        private static readonly string _patterns = "1234567890qwertyuiopasdfghjklzxcvbnm";
        public static string GenerateId(int lengthId)
        {
            Random random = new Random();
            string id = "";
            for (int i=0; i < lengthId; i++){
                id += _patterns[random.Next(_patterns.Length)];
            }
            return id;
        }
    }
}