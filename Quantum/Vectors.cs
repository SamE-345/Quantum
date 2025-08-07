using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public class Vectors
    {
        public double[] Data;
        public int shape;
        public Vectors(int dimensions)
        {
            Data = new double[dimensions];
            shape = dimensions;
        }
        public double this[int i]
        {
            get => Data[i];
            set => Data[i] = value;
        }

        public double DotProduct(Vectors vector)
        {
            CheckSize(vector);
            double result = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                result += Data[i] * vector.Data[i];
            }
            return result;
        }
        public double Magnitude() //Returns the magnitude of the vector
        {
            double result = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                result += Data[i] * Data[i];
            }
            return (double)Math.Sqrt(result);
        }
        public double CosineSimilarity(Vectors vector) //Returns the value of cos(x), where x is the angle between the two vectors
        {
            CheckSize(vector);
            return (DotProduct(vector) / (Magnitude() * vector.Magnitude()));
        }
        public void SetValue(int index, int value) //Sets the value of the vector at the given index
        {
            if (index < 0 || index >= Data.Length)
            {
                throw new IndexOutOfRangeException();
            }
            Data[index] = value;
        }
        public void MultiplyByScalar(int scalar)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] *= scalar;
            }
        }
        public void Add(Vectors vector)
        {
            CheckSize(vector);
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] += vector.Data[i];
            }
        }
        public void DivideByScalar(int scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException();
            }
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] /= scalar;
            }
        }
        public void Subtract(Vectors vector)
        {
            CheckSize(vector);
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] -= vector.Data[i];
            }
        }
        public void Subtract(int scalar)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] -= scalar;
            }
        }
        private void CheckSize(Vectors vector)
        {
            if (Data.Length != vector.Data.Length)
            {
                throw new ArgumentException("Vectors must be of the same length");
            }
        }
    }

}
