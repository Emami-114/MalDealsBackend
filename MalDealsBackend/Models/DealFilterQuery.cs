using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MalDeals.Models
{
   public class DealFilterQuery
{
    public string? SearchTerm { get; set; } = null;

    public string? Title { get; set; } = null;

    public string? Category { get; set; } = null;

    public string? Provider { get; set; } = null;

    public string? Tag { get; set; } = null;

    private int _page = 1;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value; // Mindestens 1
    }

    private int _limit = 20;

    public int Limit
    {
        get => _limit;
        set => _limit = value < 1 ? 20 : (value > 100 ? 100 : value); // Mind. 1, Max. 100
    }
}
}