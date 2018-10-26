namespace LeakysBlueprinter.Model
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        static Vector3()
        {
        }

        /// <summary>
        /// Initializes a new instance of Vector3.
        /// </summary>
        /// <param name="x">Initial value for the x-component of the vector.</param><param name="y">Initial value for the y-component of the vector.</param><param name="z">Initial value for the z-component of the vector.</param>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(double x, double y, double z)
        {
            this.X = (float)x;
            this.Y = (float)y;
            this.Z = (float)z;
        }
    }
}
