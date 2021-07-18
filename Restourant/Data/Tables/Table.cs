using Microsoft.AspNetCore.Identity;
using Restourant.Data.MappingTables;
using Restourant.Data.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restourant.Data.Tables
{
    public class Table : ITable
    {
        public int Id { get; init; }
        public int Capacity { get; init; }
        public int NumberOfPeople { get; init; }
        public bool IsReserved { get; init; }
        public decimal Bill { get; init; }

    }


}
