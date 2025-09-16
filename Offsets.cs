using System;

namespace AotForms
{
    internal static class Offsets
    {
        internal static uint Il2Cpp;
        internal static uint Unity = 0x0C250000;
        internal static uint InitBase = 0xA403C5C;
        internal static uint StaticClass = 0x5C;

        internal static uint CurrentMatch = 0x50;
        internal static uint MatchStatus = 0x8;
        internal static uint LocalPlayer = 0x44;
        internal static uint DictionaryEntities = 0x68;

        internal static uint Player_IsDead = 0x4c;
        internal static uint Player_Name = 0x22C;
        internal static uint Player_Data = 0x44;
        internal static uint Player_ShadowBase = 0x131c;
        internal static uint XPose = 0x78;

        internal static uint AvatarManager = 0x3F8;
        internal static uint Avatar = 0x94;
        internal static uint Avatar_IsVisible = 0x7c;
        internal static uint Avatar_Data = 0x10;
        internal static uint Avatar_Data_IsTeam = 0x49;

        internal static uint FollowCamera = 0x38C;
        internal static uint Camera = 0x14;
        internal static uint AimRotation = 0x344;
        internal static uint MainCameraTransform = 0x1B0;
        internal static uint Weapon = 0x338;
        internal static uint WeaponData = 0x38;
        internal static uint WeaponRecoil = 0xc;
        internal static uint ViewMatrix = 0xBC;



        internal static uint LocalPlayerAttributes = 0x404;
        internal static uint NoReload = 0x89;

        // Added for teleport
        internal static uint Bones_Root = 0xB0;                 // offset đến bone gốc
        internal static uint TransformOffset1 = 0x8;            // transform + 0x8
        internal static uint TransformOffset2 = 0x8;            // transformObj + 0x8
        internal static uint MatrixOffset = 0x20;               // matrix tại offset này
        internal static uint FinalPositionOffset = 0x80;        // offset ghi vị trí cuối

        // silent aim compatibility
        internal static uint Acess_PlayerAttributes = 0x10;
        internal static uint Class_PlayerAttributes_RunSpeedUpScale = 0x24;

        // silent sw
        internal static uint SilentBulletDataPtrOffset = 0x83c;
        internal static uint FiringSilent = 0x478;
        internal static uint arma = 0x38;
        internal static uint tiro = 0x2c;

        internal static uint GameTimer = 0x10; //protected TimeService m_SimulationTimer; // 0x10
        internal static uint FixedDeltaTime = 0x24;  //private Single m_FixedDeltaTime; // 0x24



        internal static uint RightcameraOffset = 0x38;

        // aim ai
        internal static uint HeadCoilder = 0x3F0;

        // silent 360
        internal static uint sAim3 = 0x38;
        internal static uint sAim4 = 0x2c;

        internal static uint sAim1 = WinAPI.Convert1(new byte[] { 0xE0, 0x02, 0x00, 0x00 }) + MainCameraTransform;
        internal static uint sAim2 = WinAPI.Convert1(new byte[] { 0x74, 0x06, 0x00, 0x00 }) + MainCameraTransform;

    }
}