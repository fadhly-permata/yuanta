using Web.Models;
using LumenWorks.Framework.IO.Csv;

namespace Web.Data;

internal class TrxData
{
    internal string filepath { get; set; }

    public TrxData(string? filepath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            this.filepath = filepath;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<List<TrxModel>> ReadCSV()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(this.filepath)) throw new ArgumentNullException(nameof(this.filepath));

            List<TrxModel> result = new List<TrxModel>();

            using (StreamReader reader = System.IO.File.OpenText(path: this.filepath))
            {
                string? headerLine = await reader.ReadLineAsync();
                string? textLine;

                while (!string.IsNullOrWhiteSpace(textLine = await reader.ReadLineAsync()))
                {
                    var chopped = textLine.Split(";");
                    result.Add(new TrxModel
                    {
                        TRX_CODE = int.Parse(chopped[0]),
                        TRX_SESS = int.Parse(chopped[1]),
                        TRX_TYPE = chopped[2],
                        BRK_COD2 = chopped[3],
                        INV_TYP2 = chopped[4],
                        BRK_COD1 = chopped[5],
                        INV_TYP1 = chopped[6],
                        STK_CODE = chopped[7],
                        STK_VOLM = int.Parse(chopped[8]),
                        STK_PRIC = int.Parse(chopped[9]),
                        TRX_DATE = DateTime.ParseExact(chopped[10], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        TRX_ORD2 = int.Parse(chopped[11]),
                        TRX_ORD1 = int.Parse(chopped[12]),
                        TRX_TIME = int.Parse(chopped[13]),
                    });
                }
            }

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<TrxModel> ReadCsv2()
    {
        try
        {
            if (string.IsNullOrEmpty(this.filepath)) throw new ArgumentNullException(nameof(this.filepath));

            List<TrxModel> result = new List<TrxModel>();
            using (CsvReader csv = new CsvReader(
                reader: new StreamReader(this.filepath, true),
                delimiter: ';'
            ))
            {
                while (csv.ReadNextRecord())
                {
                    result.Add(new TrxModel
                    {
                        TRX_CODE = int.Parse(csv[0]),
                        TRX_SESS = int.Parse(csv[1]),
                        TRX_TYPE = csv[2],
                        BRK_COD2 = csv[3],
                        INV_TYP2 = csv[4],
                        BRK_COD1 = csv[5],
                        INV_TYP1 = csv[6],
                        STK_CODE = csv[7],
                        STK_VOLM = int.Parse(csv[8]),
                        STK_PRIC = int.Parse(csv[9]),
                        TRX_DATE = DateTime.ParseExact(csv[10], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        TRX_ORD2 = int.Parse(csv[11]),
                        TRX_ORD1 = int.Parse(csv[12]),
                        TRX_TIME = int.Parse(csv[13]),
                    });
                }
            }

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<List<TrxRankedModel>> GetRank()
    {
        try
        {
            var cData = (await ReadCSV()).AsQueryable();

            var query =
                from buy in
                (
                    // get values
                    from trx in cData
                    group trx by new
                    {
                        trx.TRX_DATE,
                        trx.BRK_COD1
                    } into dateGroup
                    select new
                    {
                        TradeDate = dateGroup.Key.TRX_DATE,
                        BrokerCode = dateGroup.Key.BRK_COD1,

                        // buy national
                        BuyFreq = dateGroup.Where(x => x.INV_TYP1 == "I").Count(),
                        BuyVol = dateGroup.Where(x => x.INV_TYP1 == "I").Sum(x => x.STK_VOLM),
                        BuyValue = dateGroup.Where(x => x.INV_TYP1 == "I").Sum(x => x.TotalTradeValue),

                        // buy foreign
                        BuyFreqForeign = dateGroup.Where(x => x.INV_TYP1 == "A").Count(),
                        BuyVolForeign = dateGroup.Where(x => x.INV_TYP1 == "A").Sum(x => x.STK_VOLM),
                        BuyValueForeign = dateGroup.Where(x => x.INV_TYP1 == "A").Sum(x => x.TotalTradeValue),
                    }
                )
                from sell in
                (
                    // get values
                    from trx in cData
                    group trx by new
                    {
                        trx.TRX_DATE,
                        trx.BRK_COD2,
                    } into dateGroup
                    select new
                    {
                        TradeDate = dateGroup.Key.TRX_DATE,
                        BrokerCode = dateGroup.Key.BRK_COD2,

                        // buy national
                        SellFreq = dateGroup.Where(x => x.INV_TYP2 == "I").Count(),
                        SellVol = dateGroup.Where(x => x.INV_TYP2 == "I").Sum(x => x.STK_VOLM),
                        SellValue = dateGroup.Where(x => x.INV_TYP2 == "I").Sum(x => x.TotalTradeValue),

                        // buy foreign
                        SellFreqForeign = dateGroup.Where(x => x.INV_TYP2 == "A").Count(),
                        SellVolForeign = dateGroup.Where(x => x.INV_TYP2 == "A").Sum(x => x.STK_VOLM),
                        SellValueForeign = dateGroup.Where(x => x.INV_TYP2 == "A").Sum(x => x.TotalTradeValue)
                    }
                )
                where
                    buy.TradeDate == sell.TradeDate
                    &&
                    buy.BrokerCode == sell.BrokerCode
                // map values
                select new TrxRankedModel
                {
                    TradeDate = buy.TradeDate,
                    BrokerCode = buy.BrokerCode,

                    // Buy national
                    BuyFreq = buy.BuyFreq,
                    BuyVol = buy.BuyVol,
                    BuyValue = buy.BuyValue,

                    // Buy Foreign
                    BuyFreqForeign = buy.BuyFreqForeign,
                    BuyVolForeign = buy.BuyVolForeign,
                    BuyValueForeign = buy.BuyValueForeign,

                    // Sell national
                    SellFreq = sell.SellFreq,
                    SellVol = sell.SellVol,
                    SellValue = sell.SellValue,

                    // Sell Foreign
                    SellFreqForeign = sell.SellFreqForeign,
                    SellVolForeign = sell.SellVolForeign,
                    SellValueForeign = sell.SellValueForeign
                }
            ;

            query = query.OrderByDescending(x => x.TotalTradeValue);

            var result = query.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Rank = i + 1;
            }

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
