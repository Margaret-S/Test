﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    class FixWorker:Worker
    {
        public FixWorker(int id, string name, double pay)
        {
            base.ID = id;
            base.Name = name;
            base.Pay = pay;
        }
        public override double CalculationPay()
        {
            return base.Pay;
        }
    }
}
