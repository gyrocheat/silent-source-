using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

public static class telekill
{
    private const string DllName = "WinDivert.dll";
    private const string SysName = "WinDivert64.sys";

    private static readonly string AppDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WumBui");

    private static readonly string DllPath = Path.Combine(AppDataPath, DllName);
    private static readonly string SysPath = Path.Combine(AppDataPath, SysName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr WinDivertOpen(string filter, int layer, short priority, ulong flags);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool WinDivertClose(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool WinDivertRecv(IntPtr handle, byte[] pPacket, int packetLen, int flags, IntPtr pAddr, ref int readLen);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool WinDivertSend(IntPtr handle, byte[] pPacket, int packetLen, int flags, IntPtr pAddr);

    private static IntPtr handle = IntPtr.Zero;
    private static Thread divertThread;
    private static bool isRunning = false;
    private static bool dllLoaded = false;

    public static bool IsRunning => isRunning;
    public static int LagMilliseconds = 1; // Cho phép chỉnh độ trễ

    static telekill()
    {
        Directory.CreateDirectory(AppDataPath);
    }

    public static bool IsDllLoaded() => dllLoaded;

    private static bool LoadWinDivertDll()
    {
        if (dllLoaded) return true;

        try
        {
            if (!File.Exists(DllPath)) throw new FileNotFoundException($"{DllName} not found at {DllPath}");
            if (!File.Exists(SysPath)) throw new FileNotFoundException($"{SysName} not found at {SysPath}");

            IntPtr hModule = LoadLibrary(DllPath);
            if (hModule == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Exception($"Failed to load {DllName}. Error code: {errorCode}");
            }

            dllLoaded = true;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FakeLag] Load error: {ex.Message}");
            return false;
        }
    }

    public static bool Start()
    {
        if (isRunning) return true;

        if (!LoadWinDivertDll())
        {
            Console.WriteLine("Failed to start FakeLag: DLL not loaded.");
            return false;
        }

        try
        {
            string filterPart1 = "udp.PayloadLength >= 34 and udp.PayloadLength <= 230 and";
            string filterPart2 = "not udp.DstPort == 53 and not udp.DstPort == 123 and not udp.DstPort == 67 and not udp.DstPort == 1900";
            string filter = filterPart1 + " " + filterPart2;    
            handle = WinDivertOpen(filter, 0, 0, 0);
            if (handle == IntPtr.Zero)
            {
                Console.WriteLine($"[FakeLag] WinDivertOpen failed: {Marshal.GetLastWin32Error()}");
                return false;
            }

            isRunning = true;
            divertThread = new Thread(Run) { IsBackground = true, Priority = ThreadPriority.Highest };
            divertThread.Start();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FakeLag] Start error: {ex.Message}");
            Stop();
            return false;
        }
    }

    public static void Stop()
    {
        if (!isRunning) return;

        isRunning = false;

        try
        {
            divertThread?.Join(1); // Cho thread dừng nhẹ nhàng

            if (handle != IntPtr.Zero)
            {
                WinDivertClose(handle);
                handle = IntPtr.Zero;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FakeLag] Stop error: {ex.Message}");
        }
        finally
        {
            divertThread = null;
        }
    }

    private static void Run()
    {
        byte[] packet = new byte[65535];
        IntPtr addr = Marshal.AllocHGlobal(64);
        int readLen = 0;

        try
        {
            while (isRunning)
            {
                bool received = WinDivertRecv(handle, packet, packet.Length, 0, addr, ref readLen);
                if (!received)
                {
                    Thread.SpinWait(1); // Tránh Thread.Sleep (tạo cảm giác delay)
                    continue;
                }

                // Optional delay (tùy chỉnh được)
                if (LagMilliseconds > 0)
                {
                    Thread.Sleep(LagMilliseconds);
                }

                WinDivertSend(handle, packet, readLen, 0, addr);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FakeLag] Run error: {ex.Message}");
        }
        finally
        {
            Marshal.FreeHGlobal(addr);
        }
    }
}
