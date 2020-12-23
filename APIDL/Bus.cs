﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public class Bus
    {
        public state State { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime StartOfActivity { get; set; }
        public DateTime LastCheckup { get; set; }
        public int AllKilometrage { get; set; }
        public int KillFromLastCheckup { get; set; }
        public int KillFromRefueling { get; set; }
        public bool IsActive { get; set; }
        public int BusKey { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}