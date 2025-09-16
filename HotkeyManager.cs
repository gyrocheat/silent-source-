using System;
using System.Collections.Generic;
using ImGuiNET;
using System.Runtime.InteropServices;

public class HotkeyManager
{
    private class Hotkey
    {
        public int KeyCode { get; set; } = -1;
        public bool IsActive { get; set; }
        public string DisplayName { get; set; }
        public Action ToggleAction { get; set; }
    }

    private readonly Dictionary<string, Hotkey> _hotkeys = new();
    private string _currentlySetting = null;
    private const float STATUS_DURATION = 3f;
    private float _statusTimer = 0f;
    private string _statusMessage = "";

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    public HotkeyManager()
    {
        InitializeDefaultHotkeys();
    }

    private void InitializeDefaultHotkeys()
    {
        AddHotkey("Freeze", "Ngưng đọng", () => ToggleFeature("Freeze"));
        AddHotkey("Ghost", "Tàng hình", () => ToggleFeature("Ghost"));
        AddHotkey("Telekill", "Dịch sát", () => ToggleFeature("Telekill"));
    }

    public void AddHotkey(string id, string displayName, Action toggleAction, int defaultKey = -1)
    {
        _hotkeys[id] = new Hotkey
        {
            KeyCode = defaultKey,
            DisplayName = displayName,
            ToggleAction = toggleAction
        };
    }

    public void Update()
    {
        if (_statusTimer > 0)
        {
            _statusTimer -= ImGui.GetIO().DeltaTime;
        }

        foreach (var hotkey in _hotkeys.Values)
        {
            if (hotkey.KeyCode == -1) continue;

            bool isPressed = (GetAsyncKeyState(hotkey.KeyCode) & 0x8000) != 0;
            if (isPressed && !hotkey.IsActive)
            {
                hotkey.ToggleAction.Invoke();
                hotkey.IsActive = true;
            }
            else if (!isPressed && hotkey.IsActive)
            {
                hotkey.IsActive = false;
            }
        }

        if (_currentlySetting != null)
        {
            for (int key = 0; key < 256; key++)
            {
                if ((GetAsyncKeyState(key) & 0x8000) != 0 && key != 0x1B) // 0x1B = ESC
                {
                    _hotkeys[_currentlySetting].KeyCode = key;
                    _statusMessage = $"{_hotkeys[_currentlySetting].DisplayName} set to {GetKeyName(key)}";
                    _currentlySetting = null;
                    _statusTimer = STATUS_DURATION;
                    break;
                }
            }
        }
    }

    private string GetKeyName(int keyCode)
    {
        // Chuyển đổi keyCode thành tên phím
        if (keyCode >= 0x41 && keyCode <= 0x5A) return ((char)keyCode).ToString(); // A-Z
        if (keyCode >= 0x30 && keyCode <= 0x39) return ((char)keyCode).ToString(); // 0-9
        return keyCode switch
        {
            0x70 => "F1",
            0x71 => "F2",
            0x72 => "F3", // Các phím F khác...
            0x1B => "ESC",
            0x20 => "SPACE",
            0x0D => "ENTER",
            0x09 => "TAB",
            0x10 => "SHIFT",
            0x11 => "CTRL",
            0x12 => "ALT",
            _ => $"Key 0x{keyCode:X2}"
        };
    }

    public void RenderUI()
    {
        ImGui.BeginGroup();
        ImGui.Text("Hotkey Settings");
        ImGui.Separator();

        foreach (var kvp in _hotkeys)
        {
            var hotkey = kvp.Value;
            string buttonLabel = _currentlySetting == kvp.Key
                ? "Press a key..."
                : (hotkey.KeyCode == -1 ? "Not set" : GetKeyName(hotkey.KeyCode));

            ImGui.Text($"{hotkey.DisplayName}:");
            ImGui.SameLine();

            if (ImGui.Button(buttonLabel, new System.Numerics.Vector2(120, 0)))
            {
                _currentlySetting = kvp.Key;
            }

            ImGui.SameLine();
            ImGui.Text(hotkey.IsActive ? "[ACTIVE]" : "");
        }

        if (_statusTimer > 0)
        {
            ImGui.TextColored(new System.Numerics.Vector4(1, 1, 0, 1), _statusMessage);
        }

        ImGui.EndGroup();
    }

    private void ToggleFeature(string featureName)
    {
        _statusMessage = $"{featureName} toggled";
        _statusTimer = STATUS_DURATION;
        // Thêm logic bật/tắt tính năng ở đây
    }
}