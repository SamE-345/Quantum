using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    
    public struct ComplexNum
    {
        public int Real;
        public int Imaginary;
        public double theta;
        public double r;
        public ComplexNum(int re, int i)
        {
            Real = re; Imaginary = i;
            theta = Math.Atan2(i, r);
            r = Magnitude();
        }
        public void ComplexConjugate()
        {
            Imaginary *= -1;
        }
        public void MultiplyByScalar(int scalar)
        {
            Real *= scalar;
            Imaginary *= scalar;
        }
        public void MultiplyByComplex(ComplexNum complexNum)
        {
            Real = complexNum.Real * Real + Imaginary * complexNum.Imaginary;
            Imaginary = complexNum.Imaginary * Real + complexNum.Real * Imaginary;
        }
        public void AddScalar(int scalar)
        {
            Real += scalar;
        }
        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(Real, 2)+Math.Pow(Imaginary,2));
        }
    }


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
    }
}
