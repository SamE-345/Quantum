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
            state[0] = new ComplexNum(alpha,0);
            state[1] = new ComplexNum(beta,0);
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
    public class ComplexNum
    {
        public double Real;
        public double Imaginary;
        
        public ComplexNum(double re, double i)
        {
            Real = re; Imaginary = i;
            
        }
        public static ComplexNum FromPolar(double r, double theta)
        {
            return new ComplexNum(r * Math.Cos(theta), r * Math.Sin(theta));
        }
        public ComplexNum Conjugate() => new ComplexNum(Real, -Imaginary);
        
        public static ComplexNum operator *(ComplexNum a, ComplexNum b)
       => new ComplexNum(a.Real * b.Real - a.Imaginary * b.Imaginary,
                        a.Real * b.Imaginary + a.Imaginary * b.Real);


        public static ComplexNum operator +(ComplexNum a, ComplexNum b) => new ComplexNum(a.Real + b.Real, b.Imaginary + a.Imaginary);
        public static ComplexNum operator -(ComplexNum a, ComplexNum b) => new ComplexNum(a.Real - b.Real, b.Imaginary - a.Imaginary);
        public double Magnitude() => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));
        public static ComplexNum operator *(ComplexNum a, double scalar)
        => new ComplexNum(a.Real * scalar, a.Imaginary * scalar);

        public static ComplexNum operator /(ComplexNum a, double scalar)
            => new ComplexNum(a.Real / scalar, a.Imaginary / scalar);
        public double MagnitudeSquared() => Real * Real + Imaginary * Imaginary;
    }

}
