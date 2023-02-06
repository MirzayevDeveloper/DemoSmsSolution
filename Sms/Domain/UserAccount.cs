using Sms.Abstracts;
using Sms.DataBase;
using Sms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.Domain
{
    public class UserAccount : Account, IPhone
    {
        private int _id;
        private string? _name, _phoneNumber, _login, _password;
        //private List<ChatInfo?>? _chatInfo;
        public override required int Id { get => _id; set => _id = value; }
        public override string? Name { get => _name; set => _name = value; }
        public string? PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public override string? Login { get => _login; set => _login = value; }
        public override string? Password { get => _password; set => _password = value; }
        //public override List<ChatInfo?>? Chat { get => _chatInfo; set => _chatInfo = value; }

        public void Add()
        {
            DataBase.DataBase.Users?.Add(this);
        }

        public static void Remove(UserAccount account)
        {
            DataBase.DataBase.Users?.Remove(account);
        }
    }
}
