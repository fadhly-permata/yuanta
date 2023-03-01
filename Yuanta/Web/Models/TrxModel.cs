namespace Web.Models;

public class TrxModel
{
    #region FROM CSV
    public int TRX_CODE { get; set; }
    public int TRX_SESS { get; set; }
    public string? TRX_TYPE { get; set; }
    public string? BRK_COD2 { get; set; }
    public string? INV_TYP2 { get; set; }
    public string? BRK_COD1 { get; set; }
    public string? INV_TYP1 { get; set; }
    public string? STK_CODE { get; set; }
    public int STK_VOLM { get; set; }
    public int STK_PRIC { get; set; }
    public DateTime TRX_DATE { get; set; }
    public int TRX_ORD2 { get; set; }
    public int TRX_ORD1 { get; set; }
    public int TRX_TIME { get; set; }
    #endregion

    #region Calculated
    public float TotalTradeValue => this.STK_VOLM * this.STK_PRIC;
    #endregion
}