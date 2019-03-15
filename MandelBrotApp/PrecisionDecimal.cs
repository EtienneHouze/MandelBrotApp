using Java.Math;
using System;


/**
 * 
 * A Small class for arbitrary precision decimal numbers.
 * It relies on the fraction representation of such numbers, using BigIntegers to manage arbitrary precision.
 * Using them is obviously much slower than classical builtin types, such as double.
 * 
 */
namespace MandelBrotApp
{

    namespace Core
    {
        class PrecisionDecimal
        {

            BigInteger p;
            public BigInteger P
            {
                get
                {
                    return p;
                }
                set
                {
                    p = value;
                }
            }
            BigInteger q;
            public BigInteger Q
            {
                get
                {
                    return q;
                }
                set
                {
                    if (value != new BigInteger(new byte[1] { 0 }))
                    {
                        q = value;
                    }
                }
            }

            public PrecisionDecimal()
            {
                p = new BigInteger(new byte[1] { 0 });
                q = new BigInteger(new byte[1] { 1 });
            }

            public PrecisionDecimal(Int32 _p, Int32 _q)
            {
                p = new BigInteger(_p.ToString());
                q = new BigInteger(_q.ToString());
                BigInteger factor = p.Gcd(q);
                if (factor.CompareTo(BigInteger.One) != 0)
                {
                    p = p.Divide(factor);
                    q = q.Divide(factor);
                }
            }


            public static PrecisionDecimal operator *(PrecisionDecimal lhs, PrecisionDecimal rhs)
            {
                PrecisionDecimal ans = new PrecisionDecimal();
                ans.P = lhs.P.Multiply(rhs.P);
                ans.Q = lhs.Q.Multiply(rhs.Q);
                return ans;
            }

            public static PrecisionDecimal operator /(PrecisionDecimal lhs, PrecisionDecimal rhs)
            {
                PrecisionDecimal ans = new PrecisionDecimal();
                ans.P = lhs.P.Multiply(rhs.Q);
                ans.Q = lhs.Q.Multiply(rhs.P);
                return ans;
            }

            public static PrecisionDecimal operator +(PrecisionDecimal lhs, PrecisionDecimal rhs)
            {
                PrecisionDecimal ans = new PrecisionDecimal();
                ans.P = (lhs.P.Multiply(rhs.Q)).Add(lhs.Q.Multiply(rhs.P));
                ans.Q = lhs.Q.Multiply(rhs.Q);
                BigInteger factor = ans.P.Gcd(ans.Q);
                if (factor.Abs().CompareTo(BigInteger.One) != 0)
                {
                    ans.P = ans.P.Divide(factor);
                    ans.Q = ans.Q.Divide(factor);
                }
                return ans;
            }
            

            public bool Equals(PrecisionDecimal other)
            {
                int test = p.Multiply(other.Q).CompareTo(q.Multiply(other.P)); 
                return test == 0;
            }

            public static bool operator < (PrecisionDecimal lhs, PrecisionDecimal rhs)
            {
                int test =  lhs.P.Multiply(rhs.Q).CompareTo(lhs.Q.Multiply(rhs.P)); // 1 if lhs > rhs
                return test == -1;
            }

            public static bool operator > (PrecisionDecimal lhs, PrecisionDecimal rhs)
            {
                int test = lhs.P.Multiply(rhs.Q).CompareTo(lhs.Q.Multiply(rhs.P)); // 1 if lhs > rhs
                return test == 1;
            }

            override
            public string ToString()
            {
                return p.ToString() + " / " + q.ToString();
            }
        }
    }
}