using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public interface IArithmetic<T>
    {
        T Add(T a, T b);
        T Multiply(T a, T b);
        T Zero {  get; }
        T Subtract(T a, T b);
        T Squared(T a);
        T Divide(T a, T b);
        
    }
    public class IntArithmetic : IArithmetic<int>
    {
        public int Add(int a, int b) => a + b;
        public int Multiply(int a, int b) => a * b;
        public int Subtract(int a, int b) => a - b;
        public int Zero => 0;
        public int Squared(int a) => a*a;
        public int Divide(int a, int b) => a/b;
        
    }
    public class ComplexArithmetic : IArithmetic<ComplexNum>
    {
        public ComplexNum Add(ComplexNum a, ComplexNum b) => a + b;
        public ComplexNum Multiply(ComplexNum a, ComplexNum b) => a * b;
        public ComplexNum Subtract(ComplexNum a, ComplexNum b) => a - b; 
        public ComplexNum Zero => new ComplexNum(0, 0);
        public ComplexNum Conjugate(ComplexNum a) => a.Conjugate();
        public ComplexNum Squared(ComplexNum a) => a*a;
        public ComplexNum Divide(ComplexNum a, ComplexNum b)
        {
            return (a * b.Conjugate())/(Math.Pow(b.Real,2)* Math.Pow(b.Imaginary,2));
        }
    }
    public class DoubleArithmetic : IArithmetic<double>
    {
        public double Add(double a, double b) => a + b;
        public double Multiply(double a, double b) => a * b;
        public double Subtract(double a, double b) => a - b;
        public double Zero => 0.0;
        public double Squared(double a) => a*a;
        public double Divide(double a, double b) => a/b;
    }

    public static class Arithmetic<T>
    {
        public static IArithmetic<T> Default
        {
            get
            {
                if (typeof(T) == typeof(int))
                    return (IArithmetic<T>)(object)new IntArithmetic();
                if (typeof(T) == typeof(double))
                    return (IArithmetic<T>)(object)new DoubleArithmetic();
                if (typeof(T) == typeof(ComplexNum))
                    return (IArithmetic<T>)(object)new ComplexArithmetic();

                throw new NotSupportedException($"No default arithmetic defined for {typeof(T)}");
            }
        }
    }

    public class Matrix<T>
    {
        public int[] shape;
        public T[,] Data;
        private readonly IArithmetic<T> math;
        public Matrix(int n, int m, IArithmetic<T> math=null)
        {
            Data = new T[n, m];
            shape = new int[2];
            shape[0] = n;
            shape[1] = m;
            this.math = math ?? Arithmetic<T>.Default;
        }
        public T this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }
        public Matrix<T> Transpose()
        {
            Matrix<T> mat = new Matrix<T>(shape[1], shape[0], math);
            for (int i = 0; i < shape[0]; i++)
            {
                for (int j = 0; j < shape[1]; j++)
                {
                    mat[j, i] = this[i, j];
                }
            }
            return mat;
        }
        public Matrix<T> MatrixMultiply(Matrix<T> mat)
        {
            Matrix<T> result = new Matrix<T>(shape[0], mat.shape[1], math);
            for (int i = 0; i < shape[0]; i++)
            {
                T[] row = this.GetRow(i);
                for (int j = 0; j < mat.shape[1]; j++)
                {
                    T[] col = mat.GetColumn(j);
                    result[i, j] = Dotproduct(row, col);
                }
            }

            return result;
        }

        public Matrix<T> AddMatrix(Matrix<T> mat)
        {
            if (shape[0] != mat.shape[0] || shape[1] != mat.shape[1])
                throw new ArgumentException("Matrix shapes must be the same");

            if (typeof(T) != typeof(int) && typeof(T) != typeof(ComplexNum))
            {
                throw new ArgumentException("Unsupported type");
            }
            else
            {
                Matrix<T> Output = new Matrix<T>(shape[0], shape[1], math);
                for (int i = 0; i < shape[0]; i++)
                {
                     for (int ii = 0; ii < shape[1]; ii++)
                     {
                        Output[i, ii] = math.Add( mat[i, ii] ,this[i, ii]);
                     }
                }
                return Output;
            }

        }
        public void SetValues(T[,] ints)
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
        public T[] GetRow(int rowIndex)
        {
            int cols = Data.GetLength(1);
            T[] row = new T[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = Data[rowIndex, j];
            }
            return row;
        }
        public T[] GetColumn(int colIndex)
        {
            int rows = Data.GetLength(0);
            T[] col = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                col[j] = Data[j, colIndex];
            }
            return col;
        }
        
        private T Dotproduct(T[] a, T[] b) 
        {
            T result = math.Zero;

            for (int i = 0; i < a.Length; i++)
            {
                result = math.Add(result, math.Multiply(a[i], b[i]));
            }
            return result;
        }

    }
}
