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
            Matrix mat = new Matrix(shape[1], shape[0]);
            for (int i = 0; i < shape[0]; i++)
            {
                for (int j = 0; j < shape[1]; j++)
                {
                    mat[j, i] = this[i, j];
                }
            }
            return mat;
        }
        public Matrix MatrixMultiply(Matrix mat)
        {
            Matrix result = new Matrix(shape[0], mat.shape[1]);
            for (int i = 0; i < shape[0]; i++)
            {
                int[] row = this.GetRow(i);
                for (int j = 0; j < mat.shape[1]; j++)
                {
                    int[] col = mat.GetColumn(j);
                    result[i, j] = DotProduct(row, col);
                }
            }

            return result;
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
                col[j] = Data[j, colIndex];
            }
            return col;
        }
        private int DotProduct(int[] a, int[] b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Vectors must be the same length for dot product.");
            }

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }
    }
}
