using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public class Vectors <T> 
    {
        public T[] Data;
        public int shape;
        private readonly IArithmetic<T> math;
        public Vectors(int dimensions, IArithmetic<T> math=null)
        {
            Data = new T[dimensions];
            shape = dimensions;
            this.math = math ?? Arithmetic<T>.Default;
        }
        public T this[int i]
        {
            get => Data[i];
            set => Data[i] = value;
        }

        public T DotProduct(Vectors<T> vector)
        {
            CheckSize(vector);
            T result = math.Zero;
            for (int i = 0; i < Data.Length; i++)
            {
                result =math.Add(result, math.Multiply(Data[i], vector.Data[i]));
            }
            return result;
        }
        public double Magnitude() //Returns the magnitude of the vector
        {
            double sum = 0;
            for (int i = 0; i < shape; i++)
            {
                if (typeof(T) == typeof(int))
                {
                    int val = (int)(object)this[i];
                    sum += val * val;
                }
                else if (typeof(T) == typeof(double))
                {
                    double val = (double)(object)this[i];
                    sum += val * val;
                }
                else if (typeof(T) == typeof(ComplexNum))
                {
                    var val = (ComplexNum)(object)this[i];
                    sum += val.Real * val.Real + val.Imaginary * val.Imaginary;
                }
                else
                {
                    throw new NotSupportedException($"Norm not implemented for {typeof(T)}");
                }
            }
            return Math.Sqrt(sum);
        }
       
        public void SetValue(int index, T value) //Sets the value of the vector at the given index
        {
            if (index < 0 || index >= Data.Length)
            {
                throw new IndexOutOfRangeException();
            }
            Data[index] = value;
        }
        public void MultiplyByScalar(T scalar)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = math.Multiply(Data[i], scalar);
            }
        }
        public void Add(Vectors<T> vector)
        {
            CheckSize(vector);
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = math.Add(Data[i], vector.Data[i]);
            }
        }
        public void DivideByScalar(T scalar)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = math.Divide(Data[i], scalar);
            }
        }
        public void Subtract(Vectors<T> vector)
        {
            CheckSize(vector);
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = math.Subtract(Data[i], vector.Data[i]);
            }
        }
        public void Subtract(T scalar)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = math.Subtract(Data[i], scalar);
            }
        }
        private void CheckSize(Vectors<T> vector)
        {
            if (Data.Length != vector.Data.Length)
            {
                throw new ArgumentException("Vectors must be of the same length");
            }
        }
    }

}
