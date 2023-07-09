using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPlantsCatalog.Model
{
    public class Plant
    {
        public int Id { get; set; }

        public string FolkName { get; set; } = null!;

        public string? ScienceName { get; set; }

        public string? Description { get; set; }

        public string? PositiveProp { get; set; }

        public string? NegativeProp { get; set; }

        public string? GrowthRegion { get; set; }

        public string? UrlPhoto { get; set; }
    }
}
