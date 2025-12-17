namespace Quantum.Core
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
        int numQubits { get; } //Shows how many qubits the gate acts on
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
        public ComplexNum[,] GetMatrix()
        {
            return new ComplexNum[,] {
            { new ComplexNum(1,0), new ComplexNum(0,0)},
               {new ComplexNum(0,0), new ComplexNum(0,-1) }
            };
        }
    }
    public record CircuitOperation(IGate Gate, int[] Targets);
    public class QuantumCircuit
    {
        private List<CircuitOperation> operations = new();
        private ComplexNum[] state;
        public int qubitCount { get; }
        public QuantumCircuit(int qubitCount)
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
        public void AddGate(IGate g, params int[] targets)
        {
            if (targets.Length != g.numQubits)
            {
                throw new ArgumentException("The number of targets is not equal to the requirements of the gate");
            }
            else if (targets.Max() >= state.Length || targets.Min() < 0)
            {
                throw new Exception("The target is out of range of the state vector");
            }
            else
            {
                CircuitOperation op = new CircuitOperation(g, targets);
                operations.Add(op);
            }
        }
        private void ApplyGate(CircuitOperation op)
        {
            ComplexNum[,] gateMatrix = op.Gate.GetMatrix();
            ComplexNum[,] mat = BuildMatrix(gateMatrix, op.Targets);
            state = MatMultiply(state, mat);
        }
        public void Run()
        {
            foreach (var op in operations)
            {
                ApplyGate(op);
            }
        }
        public ComplexNum[] GetState() => state;

        public ComplexNum[,] BuildMatrix(ComplexNum[,] gateMatrix, int[] targets)
        {
            ComplexNum[,] result = new ComplexNum[,] { { new ComplexNum(1, 0) } };
            for (int i = 0; i < qubitCount; i++)
            {
                bool isTarget = Array.IndexOf(targets, i) != -1;
                ComplexNum[,] toTensor = isTarget ? gateMatrix : Identity2();
                result = TensorProduct(result, toTensor);
            }
            return result;
        }
        private ComplexNum[,] TensorProduct(ComplexNum[,] A, ComplexNum[,] B)
        {
            int aRows = A.GetLength(0);
            int aCols = A.GetLength(1);
            int bRows = B.GetLength(0);
            int bCols = B.GetLength(1);
            var result = new ComplexNum[aRows * bRows, aCols * bCols];

            for (int i = 0; i < aRows; i++)
            {
                for (int ii = 0; ii < aCols; ii++)
                {
                    for (int iii = 0; iii < bRows; iii++)
                    {
                        for (int iv = 0; iv < bCols; iv++)
                        {
                            result[i * bRows + iii, ii * bCols + iv] = A[i, ii] * B[iii, iv];
                        }
                    }
                }
            }
            return result;
        }
        private ComplexNum[,] Identity2()
        {
            return new ComplexNum[,] {
                { new ComplexNum(1,0), new ComplexNum(0,0) },
                { new ComplexNum(0,0), new ComplexNum(1,0) }
            };
        }
        private ComplexNum[] MatMultiply(ComplexNum[] v, ComplexNum[,] M)
        {
            int rows = M.GetLength(0);
            int cols = M.GetLength(1);

            var result = new ComplexNum[rows];
            for (int r = 0; r < rows; r++)
            {
                ComplexNum sum = new ComplexNum(0, 0);
                for (int c = 0; c < cols; c++)
                {
                    sum += M[r, c] * v[c];
                }
                result[r] = sum;
            }
            return result;
        }

    }
}
