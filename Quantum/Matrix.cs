using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    
    

    public class Matrix
    {
        public int[] shape;
        public int[,] Data;

        public Matrix(int n, int m)
        {
            Data = new int[n, m];
            shape = new int[2];
            shape[0] = n;
            shape[1] = m;

        }
        public int this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }
        public Matrix Transpose()
        {
            int[,] tempArray = new int[shape[0], shape[1]];
            Array.Copy(Data, tempArray, Data.Length);
            Data = new int[shape[1], shape[0]];
            Matrix mat = new Matrix(shape[1], shape[0]);
            for (int i = 0; i < tempArray.GetLength(0);)
            {
                for(int ii=0; ii<tempArray.GetLength(1); ii++)
                {
                    Data[i,ii] = tempArray[ii,i];
                }
            }
            mat.SetValues(Data);
            return mat;

            //TO FINISH
        }
        public Matrix MatrixMultiply(Matrix mat)
        {
            return null;
        }
        public Matrix AddMatrix(Matrix mat)
        {
            if (mat.shape != shape)
            {
                throw new ArgumentException("Matrix shapes must be the same");
            }
            else
            {
                Matrix Output = new Matrix(shape[0], shape[1]);
                int[,] Result = new int[shape[0], shape[1]];
                for (int i = 0; i < shape[0]; i++)
                {
                    for (int ii = 0; ii < shape[1]; ii++)
                    {
                        Result[i, ii] = mat[i, ii] + this[i, ii];
                    }
                }
                Output.SetValues(Result);
                return Output;
            }

        }
        public void SetValues(int[,] ints)
        {
            if (Data.Length != ints.Length)
            {
                throw new ArgumentException("Matrix shape and data shape do not match");
            }
            else
            {
                Data = ints;
            }
        }
        public int[] GetRow(int rowIndex)
        {
            int cols = Data.GetLength(1);
            int[] row = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = Data[rowIndex, j];
            }
            return row;
        }
        public int[] GetColumn(int colIndex)
        {
            int rows = Data.GetLength(0);
            int[] col = new int [rows];
            for (int j = 0; j < rows; j++)
            {
                col[j] = Data[colIndex, j];
            }
            return col;
        }
    }
}
