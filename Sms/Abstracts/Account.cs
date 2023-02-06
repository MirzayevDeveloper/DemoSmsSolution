using Sms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.Abstracts
{
    public abstract class Account
    {
        public abstract int Id { get; set; }
        public abstract string? Name { get; set; }
        public abstract string? Login { get; set; }
        public abstract string? Password { get; set; }
        //public abstract List<ChatInfo?>? Chat { get; set; }

    }
}
