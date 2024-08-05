/// <summary>
/// Model represents market information for a cryptocurrency
/// </summary>
public class Market
{
    public string ExchangeId { get; set; }
    public string BaseId { get; set; }
    public string QuoteId { get; set; }
    public double? PriceUsd { get; set; }
}