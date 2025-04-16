using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задания_1_10
{
    public class ToyEx5
    {
        string _name;
        decimal _price;
        int _ageFrom;
        int _ageTo;

        public ToyEx5() { }

        public  ToyEx5(string name, decimal price, int ageFrom, int ageTo)
        {
            _name = name;
            _price = price;
            _ageFrom = ageFrom;
            _ageTo = ageTo;
        }

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public decimal Price 
        { 
            get { return _price; } 
            set { _price = value; } 
        }
        public int AgeFrom
        { 
            get { return _ageFrom; } 
            set { _ageFrom = value; } 
        }
        public int AgeTo
        {
            get { return _ageTo; }
            set { _ageTo = value; }
        }
    }
}
