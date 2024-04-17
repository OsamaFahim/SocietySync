using SocietySync.DBcontext;
namespace SocietySync
{

    //This is A Singelton Class so that RollNumber of the user can be
    //easily accessed
    public class UserSession
    {
        private static UserSession instance;
        private SocietySyncContextFactory factory;

        public string LoggedInRollNumber { get; private set; }

        private UserSession()
        {
            factory = new SocietySyncContextFactory();
        }

        public static UserSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }

        public void SetLoggedInUser(string rollNumber)
        {
            LoggedInRollNumber = rollNumber;
        }

        public SocietySyncContext GetSocietySyncContext()
        {
            return factory.GetContext();
        }
    }
}
