using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.API;

[ApiController]
[Route("api/[controller]")]
public class TrxController : ControllerBase
{
    // http://localhost:5091/api/trx
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            TrxData tData = new TrxData("wwwroot/assets/DT230216.csv");
            return Ok(await tData.ReadCSV());
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // http://localhost:5091/api/trx/GetV2
    [HttpGet("GetV2")]
    public IActionResult GetV2()
    {
        try
        {
            TrxData tData = new TrxData("wwwroot/assets/DT230216.csv");
            return Ok(tData.ReadCsv2());
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // http://localhost:5091/api/trx/getranked
    [HttpGet("GetRanked")]
    public async Task<IActionResult> GetRanked()
    {
        try
        {
            TrxData tData = new TrxData("wwwroot/assets/DT230216.csv");
            return Ok(await tData.GetRank());
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
