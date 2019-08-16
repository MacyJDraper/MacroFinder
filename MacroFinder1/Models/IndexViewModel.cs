using System;
namespace MacroFinder1.Models
{
    public class IndexViewModel
    {
        public Product Prod { get; set; }
        public decimal TargetCal { get; set; }
        public decimal TargetFat { get; set; }
        public decimal TargetCarb { get; set; }
        public decimal TargetPro { get; set; }
        public int CalPriority { get; set; }
        public int FatPriority { get; set; }
        public int CarbPriority { get; set; }
        public int ProPriority { get; set; }

    }
}
