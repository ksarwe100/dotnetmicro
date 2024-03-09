namespace MICROSERVICE.AZ.Transaction.Messages
{
    public interface IEventBus
    {
        Task<bool> PublishMessage(object request);
    }
}
