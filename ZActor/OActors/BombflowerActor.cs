﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZActor.OActors
{
    class BombflowerActor:ActorRecord
    {
        public BombflowerActor(byte[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
        }
        protected override string GetActorName()
        {
            return "Bombflower";
        }
    }
}