namespace Web.Models;

public class TrxRankedModel
{
    public DateTime TradeDate { get; set; }
    public string? BrokerCode { get; set; }
    public int Rank { get; set; }
    
    // Buy national
    public int BuyFreq { get; set; }
    public int BuyVol { get; set; }
    public float BuyValue { get; set; }

    // Buy Foreign
    public int BuyFreqForeign { get; set; }
    public int BuyVolForeign { get; set; }
    public float BuyValueForeign { get; set; }
    
    // Sell National
    public int SellFreq { get; set; }
    public int SellVol { get; set; }
    public float SellValue { get; set; }

    // Sell Foreign
    public int SellFreqForeign { get; set; }
    public int SellVolForeign { get; set; }
    public float SellValueForeign { get; set; }

    public float TotalTradeValue => this.BuyValue + this.BuyValueForeign + this.SellValue + this.SellValueForeign;
}