using Confluent.Kafka;

namespace MessageConsumer
{
    public class Facade
    {
        private readonly ConsumerService consumerService;
        private readonly MessageRepository messageRepository;

        public Facade()
        {
            messageRepository = new MessageRepository();
            consumerService = new ConsumerService();

            TokenSrc = new();
        }

        public Task? LoopTask { get; set; }
        public CancellationTokenSource TokenSrc { get; set; }

        public void StartLoop()
        {
            LoopTask = Task.Run(() => consumerService.ConsumeLoop(LoopAction, TokenSrc.Token));
        }

        private void LoopAction(Message<string, string> message)
        {
            UserMessage userMessage = new()
            {
                UserName = message.Key,
                Message = message.Value,
            };
            userMessage.Html = HTMLBuilder.GenerateHtml(userMessage);

            messageRepository.Chat.Add(userMessage);
            messageRepository.SaveChanges();
        }
    }

}