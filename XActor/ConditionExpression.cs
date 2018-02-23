using System;
using System.Globalization;

namespace mzxrules.XActor
{
    internal class ConditionExpression
    {
        private string condition;

        public Func<short[], CaptureExpression.GetValueDelegate, bool> Test;

        public ConditionExpression(string condition)
        {
            this.condition = condition;

            if (string.IsNullOrWhiteSpace(condition))
            {
                Test = (x,y) => true;
            }
            else
            {
                var splitIndex = condition.IndexOf("==");
                var captureStr = condition.Substring(0, splitIndex).Trim();
                CaptureExpression capture = new CaptureExpression(captureStr);
                var valueStr = condition.Substring(splitIndex + 2).Trim();

                ushort value;
                if (valueStr.StartsWith("0x"))
                {
                    value = ushort.Parse(valueStr.Substring(2), NumberStyles.HexNumber);
                }
                else
                {
                    value = ushort.Parse(valueStr);
                }
                Test = (x, getValueDelegate) =>
                {
                    var GetValue = getValueDelegate(capture);
                    var v = GetValue(x);
                    return v == value;
                };
            }
        }
    }
}