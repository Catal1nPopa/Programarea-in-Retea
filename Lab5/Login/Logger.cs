using EmailApp.Entities;

namespace EmailApp.Login
{
    public class Logger
    {
        private static Logger instance;
        private static Credentials storedCredentials; 

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        public void SetCredentials(Credentials credentials)
        {
            storedCredentials = credentials;
        }

        public Credentials GetCredentials()
        {
            return storedCredentials;
        }
    }
}
