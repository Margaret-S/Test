using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public abstract class Worker
    {
        private int id;
        private string name;
        private double pay;

        public Worker() { }

        public Worker(int id, string name, double pay)
        {
            this.id = id;
            this.name = name;
            this.pay = pay;
        }

        abstract public double CalculationPay();

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public double Pay
        {
            get { return this.pay; }
            set { this.pay = value; }
        }
    }
}
