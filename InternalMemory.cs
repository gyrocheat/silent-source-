using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace AotForms
{
    internal static class InternalMemory
    {
        [DllImport("AotBst.dll", SetLastError = true)]
        static extern nint CPU(nint pVM, uint cpuId);
        [DllImport("AotBst.dll", SetLastError = true)]
        static extern int InternalRead(nint pVM, ulong address, nint buffer, uint size);
        [DllImport("AotBst.dll", SetLastError = true)]
        static extern int Cast(nint pVCpu, ulong address, out ulong physAddress);
        [DllImport("AotBst.dll", SetLastError = true)]
        static extern int InternalWrite(nint pVM, ulong address, nint buffer, uint size);

        static nint pVMAddr;
        static nint cpuAddr;
        internal static Dictionary<ulong, ulong> Cache;
       

        internal static void Initialize(nint pVM)
        {
            if (pVM == IntPtr.Zero)
                throw new ArgumentException("Invalid VM pointer", nameof(pVM));

            pVMAddr = pVM;
            cpuAddr = CPU(pVM, 0);
            if (cpuAddr == IntPtr.Zero)
                throw new InvalidOperationException("Failed to initialize CPU");

            Cache = new Dictionary<ulong, ulong>();
        }

        internal static bool Convert(ulong address, out ulong phys)
        {
            phys = 0;
            if (Cache.TryGetValue(address, out var cachedPhys))
            {
                phys = cachedPhys;
                return true;
            }

            try
            {
                cpuAddr = CPU(pVMAddr, 0);
                if (cpuAddr == IntPtr.Zero)
                    return false;

                var status = Cast(cpuAddr, address, out phys);
                if (status == 0 && !Config.NoCache)
                {
                    Cache[address] = phys;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool Read<T>(ulong address, out T data) where T : struct
        {
            data = default;
            try
            {
                var result = Convert(address, out address);
                if (!result)
                    return false;

                var size = (uint)Marshal.SizeOf<T>();
                var buffer = Marshal.AllocHGlobal((int)size);
                try
                {
                    var status = InternalRead(pVMAddr, address, buffer, size);
                    if (status == 0)
                    {
                        data = Marshal.PtrToStructure<T>(buffer);
                        return true;
                    }
                    return false;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool ReadArray<T>(ulong address, ref T[] array) where T : struct
        {
            try
            {
                var result = Convert(address, out address);
                if (!result)
                    return false;

                var size = (uint)((ulong)Marshal.SizeOf<T>() * (ulong)array.Length);
                var buffer = Marshal.AllocHGlobal((int)size);
                try
                {
                    var status = InternalRead(pVMAddr, address, buffer, size);
                    if (status == 0)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            array[i] = Marshal.PtrToStructure<T>(buffer + i * Marshal.SizeOf<T>());
                        }
                        return true;
                    }
                    return false;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static string ReadString(ulong address, int size, bool unicode = true)
        {
            try
            {
                var stringBytes = new byte[size];
                var read = ReadArray(address, ref stringBytes);
                if (!read) return string.Empty;

                var encoding = unicode ? Encoding.Unicode : Encoding.Default;
                var readString = encoding.GetString(stringBytes);
                var nullIndex = readString.IndexOf('\0');
                return nullIndex >= 0 ? readString.Substring(0, nullIndex) : readString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        internal static bool Write<T>(ulong address, T value) where T : struct
        {
            try
            {
                var result = Convert(address, out address);
                if (!result)
                    return false;

                var size = (uint)Marshal.SizeOf<T>();
                var buffer = Marshal.AllocHGlobal((int)size);
                try
                {
                    Marshal.StructureToPtr(value, buffer, false);
                    var status = InternalWrite(pVMAddr, address, buffer, size);
                    return status == 0;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Ví dụ handle process được lấy trước đó
        private static IntPtr processHandle = IntPtr.Zero;

        public static void SetProcessHandle(IntPtr handle)
        {
            processHandle = handle;
        }

        // Hàm đọc bộ nhớ an toàn, trả về bool
        public static bool Read<T>(IntPtr address, out T value) where T : struct
        {
            value = default;
            if (processHandle == IntPtr.Zero)
                return false;

            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];

            bool success = ReadProcessMemory(processHandle, address, buffer, size, out int bytesRead) && bytesRead == size;
            if (!success)
                return false;

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                value = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
                return true;
            }
            finally
            {
                handle.Free();
            }
        }

        // Hàm ghi bộ nhớ an toàn
        public static bool Write<T>(IntPtr address, T value) where T : struct
        {
            if (processHandle == IntPtr.Zero)
                return false;

            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
                return WriteProcessMemory(processHandle, address, buffer, size, out int bytesWritten) && bytesWritten == size;
            }
            finally
            {
                handle.Free();
            }
        }

        // Import WinAPI để đọc và ghi bộ nhớ process
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] buffer, int size, out int bytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out int bytesWritten);
        public static class MathUtils
        {
            public static Vector3 CalculateRotation(Vector3 from, Vector3 to)
            {
                Vector3 direction = Vector3.Normalize(to - from);
                float pitch = -MathF.Asin(direction.Y) * (180f / MathF.PI);
                float yaw = MathF.Atan2(direction.X, direction.Z) * (180f / MathF.PI);
                return new Vector3(pitch, yaw, 0);
            }

            public static Vector3 LerpRotation(Vector3 current, Vector3 target, float smoothFactor)
            {
                return new Vector3(
                    Lerp(current.X, target.X, smoothFactor),
                    Lerp(current.Y, target.Y, smoothFactor),
                    0f
                );
            }

            public static float Lerp(float a, float b, float t)
            {
                return a + (b - a) * Clamp01(t);
            }

            public static float Clamp01(float value)
            {
                if (value < 0) return 0;
                if (value > 1) return 1;
                return value;
            }

            public static float RandomFloat(float min, float max)
            {
                return (float)(new Random().NextDouble() * (max - min) + min);
            }

            public static int RandomInt(int min, int max)
            {
                return new Random().Next(min, max);
            }
        }

        internal static bool Read(object value, out uint enemyRootBonePtr)
        {
            throw new NotImplementedException();
        }

        internal static T Read<T>(ulong v)
        {
            throw new NotImplementedException();
        }

        internal static object Read<T>(object value, out T lPEIEILIKGC2)
        {
            throw new NotImplementedException();
        }

        internal static T Read<T>(nint offsetAimPosition)
        {
            throw new NotImplementedException();
        }
    }
}