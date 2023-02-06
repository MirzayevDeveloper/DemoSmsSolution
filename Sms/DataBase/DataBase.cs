using Sms.Domain;


namespace Sms.DataBase
{
    public class DataBase
    {
        public static List<UserAccount>? Users = new()
        {
            new UserAccount() {Id = 1, Name = "Admin", Login = "Admin", Password = "Admin", PhoneNumber = "1010"},
            new UserAccount() {Id = 2, Name = "Abdulatif",   Login = "Mirzayev", Password = "2020",  PhoneNumber = "+998333082020"},
            new UserAccount() {Id = 3, Name = "Ulug'bek",   Login = "Ulug'bek", Password = "1111",  PhoneNumber = "+998123456789"},
            new UserAccount() {Id = 2, Name = "Xurshid",   Login = "Abdujalilov", Password = "7777",  PhoneNumber = "+998112233456"}
        };

        public static List<ChatInfo> Data = new();


        public void Run()
        {
            ClickToAdd?.Invoke();
        }

        //public EventHandler<EventArgs> ClickAddData = delegate { };

        public delegate void Delegate();

        public event Delegate? ClickToAdd;
    }
}
