using Sms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sms.Interfaces
{
    public interface IChatInformation
    {
        string? Login { get; }
        UserAccount? User { get; }
        List<string>? Text { get; }
    }
}
