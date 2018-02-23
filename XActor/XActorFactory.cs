﻿using System.Collections.Generic;
using System.Globalization;

namespace mzxrules.XActor
{
    public class XActorFactory
    {
        static Dictionary<short, XActorParser> OcarinaActorParsers;
        static Dictionary<short, XActorParser> MaskActorParsers;
        static XActorFactory()
        {
            var Document = XActors.LoadFromFile(Constants.OcaXmlFileLocation);
            OcarinaActorParsers = GetXActorParsers(Document, Game.Oca);

            Document = XActors.LoadFromFile(Constants.MaskXmlFileLoaction);
            MaskActorParsers = GetXActorParsers(Document, Game.Mask);
        }

        static Dictionary<short, XActorParser> GetXActorParsers(XActors root, Game game)
        {
            Dictionary<short, XActorParser> result = new Dictionary<short, XActorParser>();
            foreach (var item in root.Actor)
            {
                short id = short.Parse(item.id, NumberStyles.HexNumber);
                result.Add(id, new XActorParser(item, game));

            }
            return result;
        }

        public static OcaLib.Actor.ActorRecord NewOcaActor(short[] record)
        {
            var actor = record[0];
            if (!OcarinaActorParsers.TryGetValue(actor, out XActorParser xactorParser))
            {
                return new OcaLib.Actor.ActorRecord(record);
            }
            return new XActorRecord(record, xactorParser.Description, xactorParser.GetVariables(record, CaptureExpression.GetOcaActorValue));
        }

        public static OcaLib.Actor.ActorRecord NewMMActor(short[] record)
        {
            var actor = (short)(record[0] & 0xFFF);
            if (!MaskActorParsers.TryGetValue(actor, out XActorParser xActorParser))
            {
                return new OcaLib.Actor.MActorRecord(record);
            }
            return new XMActorRecord(record, xActorParser.Description, xActorParser.GetVariables(record, CaptureExpression.GetMMActorValue));
        }

        public static OcaLib.Actor.ActorRecord NewOcaTransitionActor(byte[] record)
        {
            return new OcaLib.Actor.TransitionActor(record);
        }

        public class XActorRecord : OcaLib.Actor.ActorRecord
        {
            string name;
            string vars;
            public XActorRecord(short[] record, string name, string variables) : base(record)
            {
                this.name = name;
                this.vars = variables;
            }
            protected override string GetActorName()
            {
                return name;
            }
            protected override string GetVariable()
            {
                return vars;
            }
        }



        public class XMActorRecord : OcaLib.Actor.MActorRecord
        {
            string name;
            string vars;
            public XMActorRecord(short[] record, string name, string variables) : base(record)
            {
                this.name = name;
                this.vars = variables;
            }
            protected override string GetActorName()
            {
                return name;
            }
            protected override string GetVariable()
            {
                return vars;
            }
        }
    }
}
