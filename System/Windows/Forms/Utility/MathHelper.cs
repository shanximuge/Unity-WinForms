﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    public class MathHelper
    {
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // originaly from https://github.com/mono/MonoGame/
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }
        public static int Clamp(int value, int min, int max)
        {
            return (value > max ? max : value) < min ? min : value;
        }
        public static float Clamp(float value, float min, float max)
        {
            return (value > max ? max : value) < min ? min : value;
        }
        public static ColorF ColorLerp(ColorF from, Color to, float speed)
        {
            if (from == to) return from;
            var r = FloatLerp(from.R, to.R, speed);
            var g = FloatLerp(from.G, to.G, speed);
            var b = FloatLerp(from.B, to.B, speed);
            var a = FloatLerp(from.A, to.A, speed);
            return ColorF.FromArgb(a, r, g, b);
        }
        public static double DistanceD(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        public static double DistanceD(PointF p1, PointF p2)
        {
            return DistanceD(p1.X, p1.Y, p2.X, p2.Y);
        }
        public static float DistanceF(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }
        public static float DistanceF(float x1, float y1, float x2, float y2)
        {
            return (float)DistanceD(x1, y1, x2, y2);
        }
        public static float DistanceF(PointF p1, PointF p2)
        {
            return (float)DistanceD(p1, p2);
        }
        /// <summary>
        /// Linear interpolation between two values.
        /// </summary>
        /// <param name="from_value"></param>
        /// <param name="to_value"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static float FloatLerp(float from_value, float to_value, float speed)
        {
            return from_value + (to_value - from_value) * speed * Application.DeltaTime;
        }
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // originaly from https://github.com/mono/MonoGame/
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }
        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // originaly from https://github.com/mono/MonoGame/
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = MathHelper.Clamp(amount * UnityEngine.Time.deltaTime, 0f, 1f);
            result = MathHelper.Hermite(value1, 0f, value2, 0f, result);

            return result;
        }
        public static float Step(float from_value, float to_value, float speed)
        {
            float uSpeed = speed * Application.DeltaTime;
            if (Math.Abs(from_value - to_value) < uSpeed) return to_value;

            if (from_value < to_value)
                return from_value + uSpeed;
            else
                return from_value - uSpeed;
        }
    }
}
