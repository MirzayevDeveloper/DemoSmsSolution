using Sms.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.Domain
{
    public class ChatInfo : IChatInformation
    {
        private List<string>? _text;
        private UserAccount? _account;
        public string? Login { get; set; }
        public List<string>? Text { get => _text; set => _text = value; }
        public UserAccount? User { get => _account; set => _account = value; }

        public void AddChat()
        {
            DataBase.DataBase.Data.Add(this);
        }
        public void Remove()
        {
            DataBase.DataBase.Data.Remove(this);
        }
    }
}
