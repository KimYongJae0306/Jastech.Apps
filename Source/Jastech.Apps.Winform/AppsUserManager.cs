using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Users;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsUserManager
    {
        public UserHanlder UserHanlder { get; private set; } = new UserHanlder();

        public User CurrentUser { get; private set; } = new User();

        private static AppsUserManager _instance = null;

        public static AppsUserManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsUserManager();
            }
            
            return _instance;
        }

        public void Initialize()
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Config, "User.cfg");
            if (File.Exists(filePath) == false)
            {
                UserHanlder.AddUser(new User(AuthorityType.None, ""));
                UserHanlder.AddUser(new User(AuthorityType.Engineer, "1"));
                
                Save(filePath);
            }
            else
            {
                Load(filePath);
            }

            // Cogfig 관리 안함
            UserHanlder.AddUser(new User(AuthorityType.Maker, "6644"));

#if DEBUG
            CurrentUser = UserHanlder.GetUser(AuthorityType.Maker);
#else
            CurrentUser = UserHanlder.GetUser(AuthorityType.None);
#endif

        }

        public void SetCurrentUser(string id)
        {
            if(UserHanlder.GetUser(id) is User user)
            {
                CurrentUser = user;
            }
        }

        public void Save(string filePath)
        {
            UserHanlder.RemoveMaker();
            UserHanlder.Save(filePath);
        }

        public void Load(string filePath)
        {
            UserHanlder.ClearUser();
            UserHanlder.Load(filePath);
        }
    }
}
