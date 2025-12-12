using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;



namespace Quantum
{
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



public interface IGate
    {
        string name { get; }
        int numQubits { get; }
        ComplexNum[,] GetMatrix();

    }

    public class XGate : IGate
    {
        public string name => "X";
        public int numQubits => 1; //Acts on 1 qubit
        public ComplexNum[,] GetMatrix()
        {
            return new ComplexNum[,] {
            { new ComplexNum(0,0), new ComplexNum(1,0) },
            { new ComplexNum(1,0), new ComplexNum(0,0) }
            };
        }

    }
    public class YGate : IGate
    {
        public string name => "Y";
        public int numQubits => 1;
        public ComplexNum[,] GetMatrix()
        {
            return new ComplexNum[,] {
                { new ComplexNum(0, 0), new ComplexNum(0, -1) },
                { new ComplexNum(0, 1), new ComplexNum(0, 0) }
            };
        }
    }
    public class ZGate : IGate 
    {
        public string name => "Z";
        public int numQubits => 1;
        public ComplexNum[,] GetMatrix() {
            return new ComplexNum[,] {
            { new ComplexNum(1,0), new ComplexNum(0,0)},
               {new ComplexNum(0,0), new ComplexNum(0,-1) }
            };
        }
    }
    public record CircuitOperation(IGate Gate, int[] Targets);
    public class QuantumCicuit
    {
        private List<CircuitOperation> operations = new();
        private ComplexNum[] state;
        public int qubitCount { get; }
        public QuantumCicuit(int qubitCount)
        {
            this.qubitCount = qubitCount;
            int dim = 1 << qubitCount; // Logic Shift Right 
            state = new ComplexNum[dim];
            state[0] = new ComplexNum(1, 0);
            for (int i = 1; i < dim; i++)
            {
                state[i] = new ComplexNum(0, 0);
            }
        }
    }

}