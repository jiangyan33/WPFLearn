using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.CourseManagement.Common;

namespace Zhaoxi.CourseManagement.Model
{
    public class UserModel : NotifyBase
    {
        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; DoNotify(); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; DoNotify(); }
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; DoNotify(); }
        }

    }
}
