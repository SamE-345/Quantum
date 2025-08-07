using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    internal class Qubit
    {
        private Vectors state = new Vectors(2);
        private XGate xgate = new XGate();
        private YGate ygate = new YGate();
        public Qubit(double alpha, double beta)
        {
            state[0] = alpha;
            state[1] = beta;
        }
        public int ReadQubit()
        {
            if (state[0] == 1)
            {
                return 0;
            }
            else if (state[1] == 1)
            {
                return 1;
            }
            else
            {
                return 0; //Randomiser
            }
        }
        private bool validQubit()
        {
            if (Math.Pow(state[0], 2)+Math.Pow(state[1], 2) != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
