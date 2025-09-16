using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AotForms
{
    internal static class Core
    {


        public static uint CameraPtr = 0; // hoặc IntPtr, tùy cách bạn đọc pointer
                                          // Các thành phần khác như:

    public static uint CameraPtr2 = 0;

        internal static IntPtr Handle;
        internal static int Width = -1;
        internal static int Height = -1;
        internal static bool HaveMatrix = false;
        internal static Matrix4x4 CameraMatrix;
        internal static ulong LocalPlayer;
        internal static Vector3 LocalMainCamera;
        public static ConcurrentDictionary<long, Entity> Entities = new();

    }
}
