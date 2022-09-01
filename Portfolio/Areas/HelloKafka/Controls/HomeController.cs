using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.HelloKafka.Models;
using Portfolio.Areas.HelloKafka.Services.Kafka;
using Portfolio.Models;

namespace Portfolio.Areas.HelloKafka.Controls
{
    [Area("HelloKafka")]
    public class HomeController : Controller
    {
        private readonly KafkaProducer _producer;
        private readonly ApplicationDbContext database;

        public HomeController(KafkaProducer producer, ApplicationDbContext db)
        {
            _producer = producer;
            database = db;
        }

        public IActionResult Index()
        {
            List<UserMessage> messages = database.Chat
                .OrderByDescending(m => m.Id)
                .Take(20)
                .ToList();

            return View(messages);
        }
        public IActionResult SendMessage(string messageString = "Hello kafka")
        {
            string user = User.Identity?.Name ?? "Anonymous";

            Message<string, string> message = new()
            {
                Key = user,
                Value = messageString,
            };
            _producer.SendMessageAsync(message, "FastChat");

            return RedirectToAction(nameof(Index));
        }
    }
}
