using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public static Vector4 Zero = new Vector4();
        public static Vector4 One = new Vector4(1f, 1f, 1f, 1f);
        public static Vector4 UnitX = new Vector4(1f, 0.0f, 0.0f, 0.0f);
        public static Vector4 UnitY = new Vector4(0.0f, 1f, 0.0f, 0.0f);
        public static Vector4 UnitZ = new Vector4(0.0f, 0.0f, 1f, 0.0f);
        public static Vector4 UnitW = new Vector4(0.0f, 0.0f, 0.0f, 1f);
        /// <summary>
        /// Gets or sets the x-component of the vector.
        /// </summary>
        public float X;
        /// <summary>
        /// Gets or sets the y-component of the vector.
        /// </summary>
        public float Y;
        /// <summary>
        /// Gets or sets the z-component of the vector.
        /// </summary>
        public float Z;
        /// <summary>
        /// Gets or sets the w-component of the vector.
        /// </summary>
        public float W;

        static Vector4()
        {
        }

        /// <summary>
        /// Initializes a new instance of Vector4.
        /// </summary>
        /// <param name="x">Initial value for the x-component of the vector.</param><param name="y">Initial value for the y-component of the vector.</param><param name="z">Initial value for the z-component of the vector.</param><param name="w">Initial value for the w-component of the vector.</param>
        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initializes a new instance of Vector4.
        /// </summary>
        /// <param name="value">A vector containing the values to initialize x and y components with.</param><param name="z">Initial value for the z-component of the vector.</param><param name="w">Initial value for the w-component of the vector.</param>
        public Vector4(Vector2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initializes a new instance of Vector4.
        /// </summary>
        /// <param name="value">A vector containing the values to initialize x, y, and z components with.</param><param name="w">Initial value for the w-component of the vector.</param>
        public Vector4(Vector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Creates a new instance of Vector4.
        /// </summary>
        /// <param name="value">Value to initialize each component to.</param>
        public Vector4(float value)
        {
            this.X = this.Y = this.Z = this.W = value;
        }


        // ...


        /// <summary>
        /// Determines whether the specified Object is equal to the Vector4.
        /// </summary>
        /// <param name="other">The Vector4 to compare with the current Vector4.</param>
        public bool Equals(Vector4 other)
        {
            if ((double)this.X == (double)other.X && (double)this.Y == (double)other.Y && (double)this.Z == (double)other.Z)
                return (double)this.W == (double)other.W;
            else
                return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">Object with which to make the comparison.</param>
        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector4)
                flag = this.Equals((Vector4)obj);
            return flag;
        }

        /// <summary>
        /// Gets the hash code of this object.
        /// </summary>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
        }
    }
}
