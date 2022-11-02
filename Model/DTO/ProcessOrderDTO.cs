namespace Model.DTO;

public class ProcessOrderDTO
{
    public Guid OrderId { get; set; }

    public ProcessOrderDTO()
    {
    }

    public ProcessOrderDTO(Guid orderId)
    {
        OrderId = orderId;
    }
}
