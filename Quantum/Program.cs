using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;



namespace Quantum
{
    public abstract class Gate<T>
    {
        protected T[,] Data;

        
        protected T[] GetRow(int rowIndex)
        {
            int cols = Data.GetLength(1);
            T[] row = new T[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = Data[rowIndex, j];
            }
            return row;
        }
        protected T[] GetColumn(int colIndex)
        {
            int rows = Data.GetLength(0);
            T[] col = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                col[j] = Data[j, colIndex];
            }
            return col;
        }
        
        protected virtual double Dotproduct(double[] input1, int[] input2)
        {
            if (input1.Length != input2.Length)
            {
                throw new Exception("Two list lengths are not equal");
            }
            else
            {
                double sum = 0;
                for (int i = 0; i < input1.Length; i++)
                {
                    sum += input1[i] * input2[i];
                }
                return sum;
            }
        }
        protected virtual ComplexNum Dotproduct(ComplexNum[] a, ComplexNum[] b)
        {
            if (a.Length != b.Length) throw new ArgumentException("Lengths must match");
            ComplexNum sum = new ComplexNum(0, 0);
            for (int i = 0; i < a.Length; i++)
            {
                
                sum += a[i].Conjugate() * b[i]; 
            }
            return sum;
        }


    }

    public class XGate : Gate<int>
    {
        public XGate() {
            
            Data[0, 0] = 0; Data[0, 1] = 1;
            Data[1, 0] = 1; Data[1, 1] = 0;

        }
          
    }
    public class YGate : Gate<ComplexNum>
    {
        
        public YGate()
        {

            Data[0, 0] = new ComplexNum(0, 0);
            Data[1, 1] = new ComplexNum(0, 0);
            Data[0, 1] = new ComplexNum(0, -1);
            Data[1, 0] = new ComplexNum(0, 1);
        }
        public YGate ComplexConjugate()
        {
            YGate yGate = new YGate();
            yGate.Data[0, 1] = new ComplexNum(0, 1);
            yGate.Data[1, 0] = new ComplexNum(0, -1);
            return yGate;   
        }
        
    }
    public class ZGate : Gate<int>
    {
        public ZGate()
        {
        
            Data[0, 0] = 1;
            Data[1, 1] = -1;
            Data[0, 1] = 0;
            Data[1, 0] = 0;
            
        }          
    }

}