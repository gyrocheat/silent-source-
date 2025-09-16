using System.Numerics;
using System.Collections.Generic;

namespace AotForms
{
    internal class Entity
    {
        internal bool IsKnown;
        internal Bool3 IsTeam;
        internal Vector3 Head;
        internal Vector3 Root;
        internal Vector3 Neck;
        internal Vector3 Pelvis;
        internal Vector3 ShoulderR;
        internal Vector3 ShoulderL;
        internal Vector3 ElbowR;
        internal Vector3 ElbowL;
        internal Vector3 HandR;
        internal Vector3 HandL;
        internal Vector3 FootR;
        internal Vector3 FootL;
        internal short Health;
        internal bool IsDead;
        internal bool IsKnocked;
        internal string Name;
        internal float Distance;
        internal string WeaponName;
        public Vector3 Position { get; set; }
        internal IntPtr BaseAddress { get; set; }

        public Dictionary<Bones, Vector3> Offsets { get; set; }


        public int Id;



        public bool Status { get; internal set; }
        public uint Address { get; internal set; }






        internal Vector3 LeftWrist;
        internal Vector3 RightWrist;
        internal Vector3 Spine;

        internal Vector3 Hip;
        internal Vector3 RightCalf;
        internal Vector3 LeftCalf;
        internal Vector3 RightFoot;
        internal Vector3 LeftFoot;
        internal Vector3 LeftHand;
        internal Vector3 LeftShoulder;
        internal Vector3 RightShoulder;
        internal Vector3 RightWristJoint;
        internal Vector3 LeftWristJoint;
        internal Vector3 RightElbow;
        internal Vector3 LeftElbow;

        public Vector2 HeadScreenPos { get; set; }
        public bool IsEnemy { get; internal set; }
        public bool IsVisible { get; internal set; }

        //internal string Name;

        internal bool isVisible;
        internal short Gun;

    }
}