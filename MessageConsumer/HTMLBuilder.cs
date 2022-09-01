namespace MessageConsumer
{
    public class HTMLBuilder
    {
        public static string GenerateHtml(UserMessage message)
        {
            return $"<td>{message.UserName}</td><td>{message.Message}</td>";
        }
    }
}