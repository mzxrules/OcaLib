namespace mzxrules.OcaLib.Helper
{
    public class Vector2<T1>
    {
        public T1 x;
        public T1 y;
        public Vector2()
        {
        }
        public Vector2(T1 inV)
        {
            x = inV;
            y = inV;
        }
        public Vector2(T1 inX, T1 inY)
        {
            x = inX;
            y = inY;
        }
    }
    public class Vector2<T1, T2>
    {
        public T1 x;
        public T2 y;
    }
    public class Vector3<T1>
    {
        public T1 x;
        public T1 y;
        public T1 z;
        public Vector3()
        {
        }
        public Vector3(T1 inV)
        {
            x = inV;
            y = inV;
            z = inV;
        }
        public Vector3(T1 inX, T1 inY, T1 inZ)
        {
            x = inX;
            y = inY;
            z = inZ;
        }
    }
    public class Vector3<T1, T2, T3>
    {
        public T1 x;
        public T2 y;
        public T3 z;
    }
}
