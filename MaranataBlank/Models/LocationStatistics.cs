using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models
{
    public class LocationStatistics
    {
        public ulong Population { get; set; }
        public double Infected { get; set; }
        public double Died { get; set; }
        public double Recovered { get; set; }
        public string Source { get; set; }

        public float FatalityRate { 
            get
            {
                return (float)(this.Died / this.Infected);
            }
            private set { } 
        }
        public float InfectionRate { 
            get {
                return (float)(this.Infected / this.Population); 
            } 
            private set { }
        }
        public float RecoveryRate
        {
            get
            {
                return (float)(this.Recovered / this.Infected);
            }
            private set { }
        }
    }
}
