using System.Runtime.InteropServices;

public static class Mouse
{
    [DllImport("user32.dll")]
    private static extern void mouse_event(uint flags, uint dx, uint dy, uint data, int extraInfo);

    private const uint MOUSEEVENTF_MOVE = 0x0001;

    public static void Move(int dx, int dy)
    {
        mouse_event(MOUSEEVENTF_MOVE, (uint)dx, (uint)dy, 0, 0);
    }
}
