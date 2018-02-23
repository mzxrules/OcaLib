﻿using mzxrules.OcaLib.Actor;
using mzxrules.Helper;
using System;

namespace mzxrules.ZActor.MActors
{
    public class MActorFactory
    {
        public static ActorRecord NewActor(short[] record)
        {
            ushort actor;
            MActorRecord ar = new MActorRecord(record);

            actor = ar.Actor;
            switch (actor)
            {
                case 0x0000: return new MSimpleActor(record);
                default:
                    return new MSimpleActor(record);
            }
        }
    }

    public class MTransitionActorFactory
    {
        public static MTransitionActor New(byte[] record)
        {
            ushort actor;
            Endian.Convert(out actor, record, 4);
            switch (actor)
            {
                //case 0x0009: return new StandardDoorActor(record);
                //case 0x0023: return new TransitionPlaneActor(record);
                //case 0x002E: return new LiftingDoorActor(record);
                default: return new MTransitionActor(record);
            }
        }
    }
    public class MTransitionActor : OcaLib.Actor.TransitionActor
    {
        byte SwitchToFrontRoom;
        byte SwitchToFrontCamera;
        byte SwitchToBackRoom;
        byte SwitchToBackCamera;
        public MTransitionActor(byte[] record)
        {
            SwitchToFrontRoom = record[0];
            SwitchToFrontCamera = record[1];
            SwitchToBackRoom = record[2];
            SwitchToBackCamera = record[3];

            Endian.Convert(out ushort actor, record, 4);
            Actor = actor;
            //ActorDataA = (byte)(Actor >> 12);
            Actor &= 0xFFF;

            Endian.Convert(out Coords.x, record, 6);
            Endian.Convert(out Coords.y, record, 8);
            Endian.Convert(out Coords.z, record, 10);
            Rotation.x = 0;
            Endian.Convert(out Rotation.y,record, 12);
            Rotation.z = 0;
            Endian.Convert(out ushort variable, record, 14);
            Variable = variable;


            //DayFlags = (ushort)(((ushort)record[9] << 7) + record[11]);

            //rotation.x = (ushort)(record[8] * 2);
            //rotation.y = (ushort)(record[10] * 2);
            //rotation.z = (ushort)(record[12] * 2);

            //DataC = record[13];

            //Endian.Convert(out Variable, record, 6);

        }
        public sealed override string Print()
        {
            string varString;

            varString = GetVariable();
            return
                String.Format("{0}, {1:X4}:{2:X4}, {3}, {4}{5}",
                PrintTransition(),
                Actor,
                Variable,
                GetActorName(),
                (varString.Length > 0)? varString + ", ": "",
                PrintCoordAndRotation());
        }

        new protected string PrintTransition()
        {
            return String.Format("{0:D2} {1:X2} -> {2:D2} {3:X2}",
                SwitchToBackRoom,
                SwitchToBackCamera,
                SwitchToFrontRoom,
                SwitchToFrontCamera);
        }
    }
}
