using SocietySync.DBcontext;
namespace SocietySync
{

    //This is A Singelton Class so that RollNumber of the user can be
    //easily accessed
    public sealed class UserSession
    {
        private static readonly UserSession instance = new UserSession();
        private readonly SocietySyncContextFactory factory;

        public string LoggedInRollNumber { get; private set; }

        static UserSession()
        {

        }

        private UserSession()
        {
            factory = new SocietySyncContextFactory();
        }


        public static UserSession Instance => instance;

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
