using Jastech.Framework.Config;
using Jastech.Framework.Users;
using System.IO;

namespace Jastech.Apps.Winform
{
    public class UserManager
    {
        public UserHanlder UserHandler { get; private set; } = new UserHanlder();

        public User CurrentUser { get; private set; } = new User();

        private static UserManager _instance = null;

        public static UserManager Instance()
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            
            return _instance;
        }

        public void Initialize()
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Config, "User.cfg");
            if (File.Exists(filePath) == false)
            {
                UserHandler.AddUser(new User(AuthorityType.None, ""));
                UserHandler.AddUser(new User(AuthorityType.Engineer, "1"));
                
                Save(filePath);
            }
            else
            {
                Load(filePath);
            }

            // Cogfig 관리 안함
            UserHandler.AddUser(new User(AuthorityType.Maker, "6644"));

#if DEBUG
            CurrentUser = UserHandler.GetUser(AuthorityType.Maker);
#else
            CurrentUser = UserHandler.GetUser(AuthorityType.None);
#endif

        }

        public void SetCurrentUser(string id)
        {
            if(UserHandler.GetUser(id) is User user)
            {
                CurrentUser = user;
            }
        }

        public void Save(string filePath)
        {
            UserHandler.RemoveMaker();
            UserHandler.Save(filePath);
        }

        public void Load(string filePath)
        {
            UserHandler.ClearUser();
            UserHandler.Load(filePath);
        }
    }
}
