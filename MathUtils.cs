
using System.Numerics;

namespace AotForms
{
    internal static class MathUtils
    {
        const float SMALL_float = 0.0000000001f;


        static Quaternion LookRotation(Vector3 forwards, Vector3 upwards)
        {
            forwards = Normalized(forwards);
            upwards = Normalized(upwards);

            if (SqrMagnitude(forwards) < SMALL_float || SqrMagnitude(upwards) < SMALL_float)
                return Quaternion.Identity;

            if (1 - Math.Abs(Vector3.Dot(forwards, upwards)) < SMALL_float)
                return FromToRotation(forwards, upwards);

            Vector3 right = Normalized(Vector3.Cross(upwards, forwards));

            upwards = Vector3.Cross(forwards, right);

            Quaternion quaternion;

            float radicand = right.X + upwards.Y + forwards.Z;

            if (radicand > 0)
            {
                quaternion.W = MathF.Sqrt(1f + radicand) * 0.5f;
                float recip = 1f / (4f * quaternion.W);
                quaternion.X = (upwards.Z - forwards.Y) * recip;
                quaternion.Y = (forwards.X - right.Z) * recip;
                quaternion.Z = (right.Y - upwards.X) * recip;
            }
            else if (right.X >= upwards.Y && right.X >= forwards.Z)
            {
                quaternion.X = MathF.Sqrt(1f + right.X - upwards.Y - forwards.Z) * 0.5f;
                float recip = 1f / (4f * quaternion.X);
                quaternion.W = (upwards.Z - forwards.Y) * recip;
                quaternion.Z = (forwards.X + right.Z) * recip;
                quaternion.Y = (right.Y + upwards.X) * recip;
            }
            else if (upwards.Y > forwards.Z)
            {
                quaternion.Y = MathF.Sqrt(1f - right.X + upwards.Y - forwards.Z) * 0.5f;
                float recip = 1f / (4f * quaternion.Y);
                quaternion.Z = (upwards.Z + forwards.Y) * recip;
                quaternion.W = (forwards.X - right.Z) * recip;
                quaternion.X = (right.Y + upwards.X) * recip;
            }
            else
            {
                quaternion.Z = MathF.Sqrt(1f - right.X - upwards.Y + forwards.Z) * 0.5f;
                float recip = 1f / (4f * quaternion.Z);
                quaternion.Y = (upwards.Z + forwards.Y) * recip;
                quaternion.X = (forwards.X + right.Z) * recip;
                quaternion.W = (right.Y - upwards.X) * recip;
            }
            return quaternion;
        }

        static Quaternion FromToRotation(Vector3 forwards, Vector3 upwards)
        {
            var dot = Vector3.Dot(forwards, upwards);
            var k = MathF.Sqrt(SqrMagnitude(forwards) * SqrMagnitude(upwards));

            if (Math.Abs(dot / k + 1) < 0.00001)
            {
                var ortho = Orthogonal(forwards);

                return new Quaternion(Normalized(ortho), 0);
            }
            var cross = Vector3.Cross(forwards, upwards);
            return Normalized(new Quaternion(cross, dot + k));
        }

        static Quaternion Normalized(Quaternion rotation)
        {
            float norm = Norm(rotation);
            return new Quaternion(rotation.X / norm, rotation.Y / norm, rotation.Z / norm, rotation.W / norm);
        }

        static float Norm(Quaternion rotation)
        {
            return MathF.Sqrt(rotation.X * rotation.X +
                        rotation.Y * rotation.Y +
                        rotation.Z * rotation.Z +
                        rotation.W * rotation.W);
        }

        public static Vector3 GetRotationToLocation(Vector3 target, float smooth, Vector3 from)
        {
            Vector3 delta = target - from;
            float yaw = (float)(Math.Atan2(delta.Y, delta.X) * 180 / Math.PI);
            float pitch = (float)(-Math.Atan2(delta.Z, Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y)) * 180 / Math.PI);

            Vector3 aimAngle = new Vector3(pitch, yaw, 0);

            if (smooth > 0)
            {
                Vector3 currentRotation = InternalMemory.Read<Vector3>(Core.LocalPlayer + Offsets.AimRotation);
                aimAngle = SmoothAngle(currentRotation, aimAngle, smooth);
            }

            return aimAngle;
        }

        public static Vector3 SmoothAngle(Vector3 current, Vector3 target, float smooth)
        {
            Vector3 delta = target - current;
            return current + delta / smooth;
        }

        public static Vector3 SmoothRotation(Vector3 from, Vector3 to, float factor)
        {
            return Vector3.Lerp(from, to, factor); // mượt giống chuột thật
        }

        static Vector3 Orthogonal(Vector3 v)
        {
            return v.Z < v.X ? new Vector3(v.Y, -v.X, 0) : new Vector3(0, -v.Z, v.Y);
        }

        static Vector3 Normalized(Vector3 vector)
        {
            var mag = Magnitude(vector);

            if (mag == 0)
                return Vector3.Zero;
            return vector / mag;
        }

        static float Magnitude(Vector3 vector)
        {
            return MathF.Sqrt(SqrMagnitude(vector));
        }

        static float SqrMagnitude(this Vector3 v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }
        public static Vector3 QuaternionToEuler(Quaternion q)
        {
            // Normalize
            q = Quaternion.Normalize(q);

            Vector3 angles;

            // Pitch (X)
            double sinp = 2 * (q.W * q.X + q.Y * q.Z);
            double cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            angles.X = (float)Math.Atan2(sinp, cosp);

            // Yaw (Y)
            double siny = 2 * (q.W * q.Y - q.Z * q.X);
            if (Math.Abs(siny) >= 1)
                angles.Y = (float)Math.CopySign(Math.PI / 2, siny); // use 90 degrees if out of range
            else
                angles.Y = (float)Math.Asin(siny);

            // Roll (Z)
            double sinr = 2 * (q.W * q.Z + q.X * q.Y);
            double cosr = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            angles.Z = (float)Math.Atan2(sinr, cosr);

            return angles;
        }
        internal static Vector3 GetRotationToLocation(Vector3 targetHead, bool v, Vector3 cameraPos)
        {
            throw new NotImplementedException();
        }
    }
}
