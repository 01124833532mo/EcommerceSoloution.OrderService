namespace BussnissLogicLayer.RabbitMQ
{
    public record ProductNameUpdateMessage(Guid ProductID, string? NewName);
}
