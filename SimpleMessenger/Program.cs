// See https://aka.ms/new-console-template for more information
using DatabaseWorker;
using SimpleMessenger;
using System.Text.Json;
using System.Threading.Channels;
using MessegnerBackend.Models.Notification;


Console.WriteLine("Hello, World!");



Channel<int> c = Channel.CreateBounded<int>(10);


var writer = c.Writer;

var reader = c.Reader;



//UserManager userManager = new UserManager();

//ChatManager chatManager = new ChatManager();
//var json = JsonSerializer.Serialize(chatManager.GetChats(1));
//Console.WriteLine(json);
SimpleMessengerContext f = new TiedDBContext();

var chats = f.UpdateChat(113, "test123", "default.png");

Console.WriteLine(chats);

var generated_hash = AuthentificationLibrary.PasswordHasher.Hash("string", "string");
var hash = "5e2b66ce4f42f50784824384c3bfe6872c21c08a9cbfb1397e6e34d7fac53997";

Console.WriteLine(generated_hash);
Console.WriteLine(hash);
Console.WriteLine(generated_hash == hash);

//foreach (var user in a_users)
//{
//    Console.WriteLine(user.Name);
//}




//NotificationManager notificationSender = NotificationManager.GetInstance();

//var channels = Channel.CreateBounded<Notification>(10);

//void NotificationFor1(Notification notification)
//{
//    Console.WriteLine($"1 {notification.Message}");
//}
//void NotificationFor2(Notification notification)
//{
//    Console.WriteLine($"2 {notification.Message}");
//}
//notificationSender.AddReceiver(1, NotificationFor1);
//notificationSender.AddReceiver(2, NotificationFor2);

//var notification = new Notification { Message = "ewtr", Type = " edsfsdf" };
//notificationSender.Send([1,2], notification);

//notificationSender.RemoveReceiver(1, NotificationFor2);

//notificationSender.Send(1, notification);




