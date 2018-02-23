using mzxrules.OcaLib.Actor;
using System;

namespace mzxrules.ZActor.OActors
{
    class TimerActor:ActorRecord_Wrapper
    {
        SwitchFlag flag; //write on map clear
        int timerSecs; //clock timer to clear room, in in seconds
        bool isTimer;
        public TimerActor(short[] record, params int[] p)
            : base(record)
        {
            objectDependencies = p;
            flag = (byte)((Variable & 0xFC00) >> 10);
            timerSecs = (Variable & 0x3FF);
            isTimer = (timerSecs != 0x3FF);
        }
        protected override string GetActorName()
        {
            return "Timer";
        }
        protected override string GetVariable()
        {
            string result;
            int timer; 

            timer = (timerSecs > 600)? 600: timerSecs;
            
            if (isTimer)
            {
                result = String.Format("Room Clear Time: {0}:{1} ",
                    (timer / 60).ToString("D2"),
                    (timer % 60).ToString("D2"));
            }
            else
            {
                result =  "Clear to Switch Flag converter ";
            }
            result += flag.ToString();
            return result;
        }
    }
}
