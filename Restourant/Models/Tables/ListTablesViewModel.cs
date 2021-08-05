using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Models.Tables
{
    public class ListTablesViewModel
    {
        public int Id { get; init; }
        public int Capacity { get; init; }

        public bool IsReserved { get; init; }

        public string UserId { get; init; }
    }
}
