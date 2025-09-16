using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class FakeLagConfig
{
    // Tên process cần fake lag (không có .exe)
    public static string TargetProcessName = "HD-Player";

    // Độ trễ giữa nhận/gửi packet (ms)
    public static int LagMilliseconds = 1;
    public static ushort TargetUdpPort = 5555; // Cổng UDP của Hd-Player.exe

    // Có bật fake lag không?
    public static bool Enable = true;
}
