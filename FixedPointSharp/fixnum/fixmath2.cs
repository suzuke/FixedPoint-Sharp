﻿using System.Runtime.CompilerServices;

namespace FixedPoint
{
    public partial struct fixmath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Min(fp2 a, fp2 b)
        {
            fp ret;
            if (a.x.value < b.x.value)
            {
                ret = a.x;
            }
            else
            {
                ret = b.x;
            }

            fp ret1;
            if (a.y.value < b.y.value)
            {
                ret1 = a.y;
            }
            else
            {
                ret1 = b.y;
            }

            return new fp2(ret, ret1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Max(fp2 a, fp2 b)
        {
            fp ret;
            if (a.x.value > b.x.value)
            {
                ret = a.x;
            }
            else
            {
                ret = b.x;
            }

            fp ret1;
            if (a.y.value > b.y.value)
            {
                ret1 = a.y;
            }
            else
            {
                ret1 = b.y;
            }

            return new fp2(ret, ret1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Dot(fp2 a, fp2 b)
        {
            fp2 a1 = a;
            fp2 b1 = b;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cross(fp2 a, fp2 b)
        {
            return (a.x * b.y) - (a.y * b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Cross(fp2 a, fp s)
        {
            return new fp2(s * a.y, -s * a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Cross(fp s, fp2 a)
        {
            return new fp2(-s * a.y, s * a.x);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Clamp(fp2 num, fp2 min, fp2 max)
        {
            fp2 r;

            if (num.x.value < min.x.value)
            {
                r.x = min.x;
            }
            else
            {
                if (num.x.value > max.x.value)
                {
                    r.x = max.x;
                }
                else
                {
                    r.x = num.x;
                }
            }

            if (num.y.value < min.y.value)
            {
                r.y = min.y;
            }
            else
            {
                if (num.y.value > max.y.value)
                {
                    r.y = max.y;
                }
                else
                {
                    r.y = num.y;
                }
            }

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 ClampMagnitude(fp2 v, fp length)
        {
            fp2 value = v;
            fp r;

            r.value =
                ((value.x.value * value.x.value) >> fixlut.PRECISION) +
                ((value.y.value * value.y.value) >> fixlut.PRECISION);
            if (r.value <= ((length.value * length.value) >> fixlut.PRECISION))
            {
            }
            else
            {
                fp2 v1 = value;
                fp m = default(fp);
                fp r2;

                r2.value =
                    ((v1.x.value * v1.x.value) >> fixlut.PRECISION) +
                    ((v1.y.value * v1.y.value) >> fixlut.PRECISION);
                fp r1;

                if (r2.value == 0)
                {
                    r1.value = 0;
                }
                else
                {
                    var b = (r2.value >> 1) + 1L;
                    var c = (b + (r2.value / b)) >> 1;

                    while (c < b)
                    {
                        b = c;
                        c = (b + (r2.value / b)) >> 1;
                    }

                    r1.value = b << (fixlut.PRECISION >> 1);
                }

                m = r1;

                if (m.value <= fp.epsilon.value)
                {
                    v1 = default(fp2);
                }
                else
                {
                    v1.x.value = ((v1.x.value << fixlut.PRECISION) / m.value);
                    v1.y.value = ((v1.y.value << fixlut.PRECISION) / m.value);
                }

                value = v1 * length;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Magnitude(fp2 v)
        {
            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b = (r.value >> 1) + 1L;
                var c = (b + (r.value / b)) >> 1;

                while (c < b)
                {
                    b = c;
                    c = (b + (r.value / b)) >> 1;
                }

                r1.value = b << (fixlut.PRECISION >> 1);
            }

            return r1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp MagnitudeSqr(fp2 v)
        {
            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Distance(fp2 a, fp2 b)
        {
            fp2 v;

            v.x.value = a.x.value - b.x.value;
            v.y.value = a.y.value - b.y.value;

            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b1 = (r.value >> 1) + 1L;
                var c  = (b1 + (r.value / b1)) >> 1;

                while (c < b1)
                {
                    b1 = c;
                    c  = (b1 + (r.value / b1)) >> 1;
                }

                r1.value = b1 << (fixlut.PRECISION >> 1);
            }

            return r1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp DistanceSqr(fp2 a, fp2 b)
        {
            var x = a.x.value - b.x.value;
            var z = a.y.value - b.y.value;

            fp r;
            r.value = ((x * x) >> fixlut.PRECISION) + ((z * z) >> fixlut.PRECISION);
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Normalize(fp2 v)
        {
            fp2 v1 = v;
            fp m = default(fp);
            fp r;

            r.value =
                ((v1.x.value * v1.x.value) >> fixlut.PRECISION) +
                ((v1.y.value * v1.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b = (r.value >> 1) + 1L;
                var c = (b + (r.value / b)) >> 1;

                while (c < b)
                {
                    b = c;
                    c = (b + (r.value / b)) >> 1;
                }

                r1.value = b << (fixlut.PRECISION >> 1);
            }

            m = r1;

            if (m.value <= fp.epsilon.value)
            {
                v1 = default(fp2);
            }
            else
            {
                v1.x.value = ((v1.x.value << fixlut.PRECISION) / m.value);
                v1.y.value = ((v1.y.value << fixlut.PRECISION) / m.value);
            }

            return v1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Lerp(fp2 from, fp2 to, fp t)
        {
            fp t1 = t;
            if (t1.value < fp.zero.value)
            {
                t1 = fp.zero;
            }
            else
            {
                if (t1.value > fp.one.value)
                {
                    t1 = fp.one;
                }
                else
                {
                    t1 = t1;
                }
            }

            return new fp2(@from.x + (to.x - @from.x) * t1, @from.y + (to.y - @from.y) * t1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Angle(fp2 a, fp2 b)
        {
            fp2 v = a;
            fp m = default(fp);
            fp r2;

            r2.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r2.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b2 = (r2.value >> 1) + 1L;
                var c  = (b2 + (r2.value / b2)) >> 1;

                while (c < b2)
                {
                    b2 = c;
                    c  = (b2 + (r2.value / b2)) >> 1;
                }

                r1.value = b2 << (fixlut.PRECISION >> 1);
            }

            m = r1;

            if (m.value <= fp.epsilon.value)
            {
                v = default(fp2);
            }
            else
            {
                v.x.value = ((v.x.value << fixlut.PRECISION) / m.value);
                v.y.value = ((v.y.value << fixlut.PRECISION) / m.value);
            }

            fp2 v1 = b;
            fp m1 = default(fp);
            fp r3;

            r3.value =
                ((v1.x.value * v1.x.value) >> fixlut.PRECISION) +
                ((v1.y.value * v1.y.value) >> fixlut.PRECISION);
            fp r4;

            if (r3.value == 0)
            {
                r4.value = 0;
            }
            else
            {
                var b3 = (r3.value >> 1) + 1L;
                var c1 = (b3 + (r3.value / b3)) >> 1;

                while (c1 < b3)
                {
                    b3 = c1;
                    c1 = (b3 + (r3.value / b3)) >> 1;
                }

                r4.value = b3 << (fixlut.PRECISION >> 1);
            }

            m1 = r4;

            if (m1.value <= fp.epsilon.value)
            {
                v1 = default(fp2);
            }
            else
            {
                v1.x.value = ((v1.x.value << fixlut.PRECISION) / m1.value);
                v1.y.value = ((v1.y.value << fixlut.PRECISION) / m1.value);
            }

            fp2 a1 = v;
            fp2 b1 = v1;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;
            var dot = r;
            fp min = -fp.one;
            fp max = +fp.one;
            fp ret;
            if (dot.value < min.value)
            {
                ret = min;
            }
            else
            {
                if (dot.value > max.value)
                {
                    ret = max;
                }
                else
                {
                    ret = dot;
                }
            }

            return new fp(fixlut.acos(ret.value)) * fp.rad2deg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Radians(fp2 a, fp2 b)
        {
            fp2 v = a;
            fp m = default(fp);
            fp r2;

            r2.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r2.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b2 = (r2.value >> 1) + 1L;
                var c  = (b2 + (r2.value / b2)) >> 1;

                while (c < b2)
                {
                    b2 = c;
                    c  = (b2 + (r2.value / b2)) >> 1;
                }

                r1.value = b2 << (fixlut.PRECISION >> 1);
            }

            m = r1;

            if (m.value <= fp.epsilon.value)
            {
                v = default(fp2);
            }
            else
            {
                v.x.value = ((v.x.value << fixlut.PRECISION) / m.value);
                v.y.value = ((v.y.value << fixlut.PRECISION) / m.value);
            }

            fp2 v1 = b;
            fp m1 = default(fp);
            fp r3;

            r3.value =
                ((v1.x.value * v1.x.value) >> fixlut.PRECISION) +
                ((v1.y.value * v1.y.value) >> fixlut.PRECISION);
            fp r4;

            if (r3.value == 0)
            {
                r4.value = 0;
            }
            else
            {
                var b3 = (r3.value >> 1) + 1L;
                var c1 = (b3 + (r3.value / b3)) >> 1;

                while (c1 < b3)
                {
                    b3 = c1;
                    c1 = (b3 + (r3.value / b3)) >> 1;
                }

                r4.value = b3 << (fixlut.PRECISION >> 1);
            }

            m1 = r4;

            if (m1.value <= fp.epsilon.value)
            {
                v1 = default(fp2);
            }
            else
            {
                v1.x.value = ((v1.x.value << fixlut.PRECISION) / m1.value);
                v1.y.value = ((v1.y.value << fixlut.PRECISION) / m1.value);
            }

            fp2 a1 = v;
            fp2 b1 = v1;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;
            var dot = r;
            fp min = -fp.one;
            fp max = +fp.one;
            fp ret;
            if (dot.value < min.value)
            {
                ret = min;
            }
            else
            {
                if (dot.value > max.value)
                {
                    ret = max;
                }
                else
                {
                    ret = dot;
                }
            }

            return new fp(fixlut.acos(ret.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSigned(fp2 a, fp2 b)
        {
            fp2 v = a;
            fp m = default(fp);
            fp r2;

            r2.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);
            fp r1;

            if (r2.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b2 = (r2.value >> 1) + 1L;
                var c  = (b2 + (r2.value / b2)) >> 1;

                while (c < b2)
                {
                    b2 = c;
                    c  = (b2 + (r2.value / b2)) >> 1;
                }

                r1.value = b2 << (fixlut.PRECISION >> 1);
            }

            m = r1;

            if (m.value <= fp.epsilon.value)
            {
                v = default(fp2);
            }
            else
            {
                v.x.value = ((v.x.value << fixlut.PRECISION) / m.value);
                v.y.value = ((v.y.value << fixlut.PRECISION) / m.value);
            }

            fp2 v1 = b;
            fp m1 = default(fp);
            fp r3;

            r3.value =
                ((v1.x.value * v1.x.value) >> fixlut.PRECISION) +
                ((v1.y.value * v1.y.value) >> fixlut.PRECISION);
            fp r4;

            if (r3.value == 0)
            {
                r4.value = 0;
            }
            else
            {
                var b3 = (r3.value >> 1) + 1L;
                var c1 = (b3 + (r3.value / b3)) >> 1;

                while (c1 < b3)
                {
                    b3 = c1;
                    c1 = (b3 + (r3.value / b3)) >> 1;
                }

                r4.value = b3 << (fixlut.PRECISION >> 1);
            }

            m1 = r4;

            if (m1.value <= fp.epsilon.value)
            {
                v1 = default(fp2);
            }
            else
            {
                v1.x.value = ((v1.x.value << fixlut.PRECISION) / m1.value);
                v1.y.value = ((v1.y.value << fixlut.PRECISION) / m1.value);
            }

            fp2 a1 = v;
            fp2 b1 = v1;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;
            var dot = r;
            fp min = -fp.one;
            fp max = +fp.one;
            fp ret;
            if (dot.value < min.value)
            {
                ret = min;
            }
            else
            {
                if (dot.value > max.value)
                {
                    ret = max;
                }
                else
                {
                    ret = dot;
                }
            }

            var rad  = new fp(fixlut.acos(ret.value));
            var sign = ((a.x * b.y - a.y * b.x).value <  fixlut.ZERO) ? fp.minus_one : fp.one;

            return rad * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSkipNormalize(fp2 a, fp2 b)
        {
            fp2 a1 = a;
            fp2 b1 = b;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;
            var dot = r;
            fp min = -fp.one;
            fp max = +fp.one;
            fp ret;
            if (dot.value < min.value)
            {
                ret = min;
            }
            else
            {
                if (dot.value > max.value)
                {
                    ret = max;
                }
                else
                {
                    ret = dot;
                }
            }

            return new fp(fixlut.acos(ret.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSignedSkipNormalize(fp2 a, fp2 b)
        {
            fp2 a1 = a;
            fp2 b1 = b;
            var x = ((a1.x.value * b1.x.value) >> fixlut.PRECISION);
            var z = ((a1.y.value * b1.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;
            var dot = r;
            fp min = -fp.one;
            fp max = +fp.one;
            fp ret;
            if (dot.value < min.value)
            {
                ret = min;
            }
            else
            {
                if (dot.value > max.value)
                {
                    ret = max;
                }
                else
                {
                    ret = dot;
                }
            }

            var rad  = new fp(fixlut.acos(ret.value));
            var sign = ((a.x * b.y - a.y * b.x).value < fixlut.ZERO) ? fp.minus_one : fp.one;

            return rad * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Reflect(fp2 vector, fp2 normal)
        {
            fp dot = (vector.x * normal.x) + (vector.y * normal.y);

            fp2 result;

            result.x = vector.x - ((fp.two * dot) * normal.x);
            result.y = vector.y - ((fp.two * dot) * normal.y);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Rotate(fp2 vector, fp angle)
        {
            fp2 vector1 = vector;
            var cs = fixmath.Cos(angle);
            var sn = fixmath.Sin(angle);

            var px = (vector1.x * cs) - (vector1.y * sn);
            var pz = (vector1.x * sn) + (vector1.y * cs);

            vector1.x = px;
            vector1.y = pz;

            return vector1;
        }
    }
}