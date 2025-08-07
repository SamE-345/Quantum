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
                col[j] = Data[colIndex, j];
            }
            return col;
        }
        public virtual Vectors ApplyGate(Vectors vec)
        {
            Vectors output = new Vectors(vec.shape);
            int[] tempData = new int[vec.shape];

            for (int i = 0; i < vec.shape; i++)
            {
                int[] row = GetRow(i) as int[];
                output[i] = Dotproduct(vec.Data, row);
            }
            return output;
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
        protected virtual ComplexNum Dotproduct(ComplexNum[] input1, ComplexNum[] input2)
        {
            return input2[0]; //TODO
        }


    }

    public class XGate : Gate<int>
    {
          int[,] Data = { { 0, 1 }, { 1, 0 } };
    }
    public class YGate : Gate<ComplexNum>
    {
        private ComplexNum[,] Data = new ComplexNum[2, 2];
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
            Data = new int[2,2];
            Data[0, 0] = 1;
            Data[1, 1] = -1;
            Data[0, 1] = 0;
            Data[1, 0] = 0;
            
        }          
    }

}