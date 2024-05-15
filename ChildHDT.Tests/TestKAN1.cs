using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices;

namespace ChildHDT.Tests
{
    public class TestKAN1
    {
        static async Task Main(string[] args)
        {
            var messaging = new Messaging();

            var publisher = new Child(name: "Publisher", surname: "Tester", age: 10, classroom: "3ºA");
            var subscriber = new Child(name: "Publisher", surname: "Tester", age: 10, classroom: "3ºA");

            // SUBSCRIBE
            await messaging.Subscribe(subscriber, "test/topic");

            // PUBLISH
            await messaging.Publish(publisher, "test/topic", "This is a trial");

            await Task.Delay(2000);

            Console.WriteLine("Mensaje enviado y recibido correctamente.");

            Console.ReadLine();
        }

    }
}