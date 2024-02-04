namespace Delivery.Models.DTOs;

public class RebateDto
{
    public required uint NewPrice { get; set; }
    public required bool IsRebate { get; set; }
}