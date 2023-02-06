using Sms.Domain;

namespace Sms
{
    class Program
    {

        static void Main(string[] args)
        {
            bool isActive = true;
            int choice;

            while (isActive)
            {
                Console.Clear();
                Console.WriteLine("         Welcome our Chat");
                Console.Write("1.Sign in\n2.Sign up\n3.Exit\nchoose option: ");
                string? s = Console.ReadLine();
                int.TryParse(s, out choice);

                switch (choice)
                {
                    case 1:
                        {
                            var sign = SignIn();
                            if (sign != null) SMSMenu(sign);

                        }
                        break;
                    case 2:
                        {
                            var user = SignUp();
                            if (user != null) SMSMenu(user);
                        }
                        break;
                    case 3: isActive = false; break;
                    default: break;
                }
            }
        }

        static UserAccount? SignIn()
        {
            Console.Clear();
            Console.Write("enter login: ");
            string? login = Console.ReadLine();
            Console.Write("enter password: ");
            string? password = Console.ReadLine();

            UserAccount? user = DataBase.DataBase.Users?.Find(x => x.Login == login);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    Console.WriteLine("Error password!");
                    Print();
                }
            }
            else
            {
                Console.WriteLine("Not found like this user");
                Print();
            }
            return null;
        }

        static UserAccount? SignUp()
        {

            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter your phone number: +(998) ");
            string? phone = "+998" + Console.ReadLine();
            Console.Write("Create login: ");
            string? login = Console.ReadLine();
            Console.Write("Create password: ");
            string? password = Console.ReadLine();

            Console.WriteLine("Do you change something [y/n]? : ");
            string? answer = Console.ReadLine();
            if (answer != "y")
            {
                int size = DataBase.DataBase.Users?.Count - 1 ?? -1;

                UserAccount user = new()
                {
                    Id = 1 + DataBase.DataBase.Users[size].Id,
                    Name = name,
                    PhoneNumber = phone,
                    Login = login,
                    Password = password,
                };
                Console.WriteLine("Successfully registred!");

                #region EventForAddData
                DataBase.DataBase data = new();
                data.ClickToAdd += user.Add;
                data.Run();
                data.ClickToAdd -= user.Add;
                #endregion

                Print();
                return user;
            }
            else
            {
                return null;
            }
        }

        static void SMSMenu(UserAccount user)
        {
            bool isActive = true;
            int choice;

            while (isActive)
            {
                Console.Clear();
                Console.Write("1.My Contact\n2.Add Contact\n3.Exit\nchoose option: ");
                string? option = Console.ReadLine();
                int.TryParse(option, out choice);

                switch (choice)
                {
                    case 1: MyContact(user); Print(); break;
                    case 2: AddContact(user); break;
                    case 3: isActive = false; break;
                    default: Console.Clear(); break;
                }
            }
        }

        static void AddContact(UserAccount user)
        {
            bool isActive = true;
            var users = DataBase.DataBase.Users;
            while (isActive)
            {
                Console.Clear();
                int j = 1;
                for (int i = 0; i < users?.Count; i++)
                {
                    if (users[i].Login == user.Login) continue;

                    Console.WriteLine(j + "-" + users[i].Login);
                    j++;
                }
                Console.Write("\n0.Back\nWrite one login to add your contact: ");
                string? contact = Console.ReadLine();
                if (contact == "0") isActive = false;
                else
                {
                    UserAccount? friend = DataBase.DataBase.Users?.Find(b => b.Login == contact);
                    if (friend != null)
                    {
                        ChatInfo chat = new()
                        {
                            Login = user.Login,
                            User = friend,
                            Text = new List<string> { "No message here" }
                        };
                        DataBase.DataBase.Data.Add(chat);
                        Console.WriteLine("Successfully added friend");
                    }
                    else
                    {
                        Console.WriteLine("friend not found");
                    }
                    Print();
                }
            }
        }

        static void MyContact(UserAccount user)
        {
            bool isActive = true;

            while (isActive)
            {
                var list = DataBase.DataBase.Data;
                if (list.Count > 0)
                {
                    int j = 1;
                    List<string> friends = new List<string>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].User.Login == user.Login) continue;
                        else if (!friends.Contains(list[i].User.Login))
                        {
                            Console.WriteLine($"{j}-{list[i]?.User?.Login}");
                            j++;
                            friends.Add(list[i].User.Login);
                        }
                    }
                    Console.Write("\n0.Back\nWrite one to chatting: ");
                    string? option = Console.ReadLine();
                    if (option == "0") isActive = false;

                    var receiver = DataBase.DataBase.Data.Find(a => a.User?.Login == option && a.Login == user.Login);

                    if (receiver != null)
                    {
                        bool isTrue = true;

                        while (isTrue)
                        {
                            Console.Clear();
                            List<string>? text = receiver?.Text;

                            for (int i = 0; i < text?.Count; i++)
                            {
                                Console.WriteLine(text[i]);
                            }

                            Console.Write("\n0.Back\nWrite message here: ");
                            string? message = Console.ReadLine();

                            if (message != "0")
                            {
                                string str = $"                 {DateTime.Now.ToString("MM/dd/yyyy H:mm")}\nfrom {receiver?.Login}: {message}";

                                receiver?.Text?.Add(str);

                                Console.WriteLine("Successfull messaged!");
                            }
                            else if (message == "0")
                            {

                                DataBase.DataBase data = new();
                                var a = DataBase.DataBase.Data.Find(a => a.User.Login == option && a.Login == user.Login);
                                data.ClickToAdd += DataBase.DataBase.Data.Find(a => a.User.Login == option && a.Login == user.Login).Remove;
                                data.Run();
                                data.ClickToAdd -= a.Remove;
                                ChatInfo chat = new()
                                {
                                    Login = receiver?.Login,
                                    User = receiver?.User,
                                    Text = receiver?.Text
                                };

                                chat.AddChat();
                                var chat2 = new ChatInfo()
                                {
                                    Login = chat?.User?.Login,
                                    User = user,
                                    Text = receiver?.Text
                                };
                                chat2.AddChat();
                                Console.WriteLine("Successfull added");
                                //// second
                                //a = DataBase.DataBase.Data.Find(a => a.User.Login == option && a.Login == user.Login);
                                //data.ClickToAdd += DataBase.DataBase.Data.Find(a => a.User.Login == option && a.Login == user.Login).Remove;
                                //data.Run();
                                //data.ClickToAdd -= a.Remove;
                                //chat = new()
                                //{
                                //    Login = receiver?.Login,
                                //    User = receiver?.User,
                                //    Text = receiver?.Text
                                //};

                                Print();
                                isTrue = false;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have not any friends yet!");
                    isActive = false;
                }



            }
        }

        static void Print()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void PrintUserInfo(UserAccount userAccount)
        {
            Console.Clear();
            Console.WriteLine($"Name: {userAccount.Name}\nPhone: {userAccount.PhoneNumber}\n" +
                $"Login: {userAccount.Login}\nPassword: {userAccount.Password}");
            Print();
        }

    }
}



