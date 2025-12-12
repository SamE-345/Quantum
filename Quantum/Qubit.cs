using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    internal class Qubit
    {
        private ComplexNum[] state = new ComplexNum[2];

        public Qubit(double alpha, double beta)
        {
            state[0] = new ComplexNum(alpha, 0);
            state[1] = new ComplexNum(beta, 0);
        }
        public Qubit(ComplexNum alpha, ComplexNum beta)
        {
            state[0] = alpha;
            state[1] = beta;
            if (!IsValidQubit())
                throw new ArgumentException("Qubit amplitudes must be normalized");
        }
        public int ReadQubit()
        {
            double prob0 = state[0].MagnitudeSquared();
            double prob1 = state[1].MagnitudeSquared();
            double rand = new Random().NextDouble();
            return rand < prob0 / (prob0 + prob1) ? 0 : 1;
        }
        private bool IsValidQubit()
        {
            double sum = state[0].MagnitudeSquared() + state[1].MagnitudeSquared();
            return Math.Abs(sum - 1.0) < 1e-6;
        }

    }
}