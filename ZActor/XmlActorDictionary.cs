using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mzxrules.ZActor
{
    public static class XmlActorDictionary
    {
        public static Dictionary<int, ActorDefinition> ActorDefs;

        static XmlActorDictionary()
        {
            LoadFile(@"C:\Users\mzxrules\Roms\N64\Hack Tools\SceneNavi-v10-beta2\XML\ActorDefinitions\ZL\ActorDefDB.xml");
        }

        public static void LoadFile(string file)
        {
            XDocument doc = XDocument.Load(file);
            ActorDefs = new Dictionary<int, ActorDefinition>();

            try
            {
                foreach (XElement definition in doc.Root.Elements("Definition"))
                {
                    if (definition.Attribute("IsDefault") == null)
                    {
                        ActorDefinition def = new ActorDefinition();
                        XAttribute attr = definition.Attribute("Number");
                        def.ActorNumber = (ushort)new System.ComponentModel.UInt16Converter().ConvertFromString(attr.Value);
                        attr = definition.Attribute("Description");
                        def.Name = (attr != null) ? attr.Value : def.ActorNumber.ToString("X4");

                        foreach (XElement item in definition.Elements("Item"))
                        {
                            def.AddItem(item);
                        }
                        ActorDefs.Add(def.ActorNumber, def);
                    }
                }
            }
            catch //(Exception e)
            {
                ;
            }
        }

        public static bool TryGetActorDefinition(int num, out ActorDefinition def)
        {
            return ActorDefs.TryGetValue(num, out def);
        }
    }

    public class ActorDefinition
    {
        public UInt16 ActorNumber;
        public string Name;
        List<ActorDefinitionItem> Items = new List<ActorDefinitionItem>();

        public string GetDescription(byte[] init)
        {
            StringBuilder result = new StringBuilder();

            //var test = items.OrderBy(x => x.ItemUsage);
            //foreach (ActorDefinitionItem item in test)

            foreach (ActorDefinitionItem item in Items)
            {
                throw new NotImplementedException();
                //result.AppendFormat("{0} ", item.GetDescription(init));
            }

            return result.ToString();
        }

        public void AddItem(XElement item)
        {
            Items.Add(new ActorDefinitionItem(item));
        }

        public IEnumerable<Tuple<ushort, string, ActorDefinitionItem.Usage>> GetValues(byte[] init)
        {
            foreach (ActorDefinitionItem item in Items)
            {
                yield return item.GetValueDescriptionUsage(init);
            }
        }

        //public ushort GetValueByUsageAttribute(ActorDefinitionItem.Usage usage, byte[] init)
        //{
        //    ActorDefinitionItem item;
        //    item = items.SingleOrDefault(x => x.ItemUsage == usage);
        //    if (item == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    else
        //    {
        //        return item.GetValue(init);
        //    }
        //}

        //public bool TryGetValueByUsageAttribute(ActorDefinitionItem.Usage usage, byte[] init, out ushort value)
        //{
        //    ActorDefinitionItem item;
        //    item = items.SingleOrDefault(x => x.ItemUsage == usage);
        //    if (item == null)
        //    {
        //        value = 0;
        //        return false;
        //    }
        //    else
        //    {
        //        value = item.GetValue(init);
        //        return true;
        //    }
        //}

        //internal bool TryGetValueByUsageAttribute(ActorDefinitionItem.Usage usage, byte[] init, out short p)
        //{
        //    ushort value;
        //    bool result;

        //    result = TryGetValueByUsageAttribute(usage, init, out value);
        //    p = (short)value;
        //    return result;
        //}
    }

    public class ActorDefinitionItem
    {
        class Option
        {
            public UInt16 Value;
            public string Description;

            public Option(XElement item)
            {
                XAttribute attr;
                attr = item.Attribute("Value");
                this.Value = (attr != null) ?
                    (ushort)new System.ComponentModel.UInt16Converter().ConvertFromString(attr.Value) :
                    (ushort)0xFFFF;
                this.Description = item.Attribute("Description").Value;
            }
        }

        public enum Usage
        {
            ActorNumber,
            PositionX,
            PositionY,
            PositionZ,
            RotationX,
            RotationY,
            RotationZ,
            Standard,
            CollectFlag,
            ChestFlag,
            SwitchFlag,
        }
        public byte Index;
        public Type CType;
        public ActorDefinitionItem.Usage ItemUsage;
        public string Description;
        public string DisplayStyle;

        public UInt16 Mask { get; set; }

        Dictionary<UInt16, ActorDefinitionItem.Option> Options = new Dictionary<UInt16, ActorDefinitionItem.Option>();

        public ActorDefinitionItem(XElement item)
        {
            Initialize(item);
        }

        private void Initialize(XElement item)
        {
            XAttribute attr;

            this.Index = byte.Parse(item.Attribute("Index").Value);
            this.CType = Type.GetType(item.Attribute("ValueType").Value);
            this.Description = item.Attribute("Description").Value;
            attr = item.Attribute("Usage");
            this.SetUsage((attr != null) ? attr.Value : "Standard");
            attr = item.Attribute("DisplayStyle");
            this.DisplayStyle = (attr != null) ? attr.Value : "Hexadecimal";
            attr = item.Attribute("Mask");
            this.Mask = (attr != null) ?
                (ushort)new System.ComponentModel.UInt16Converter().ConvertFromString(attr.Value) :
                (ushort)0xFFFF;

            foreach (XElement option in item.Elements("Option"))
            {
                ActorDefinitionItem.Option temp = new ActorDefinitionItem.Option(option);
                Options.Add(temp.Value, temp);
            }
        }

        private void SetUsage(string p)
        {
            try
            {
                ItemUsage = (Usage)Enum.Parse(typeof(Usage), p);
            }
            catch (ArgumentException)
            {
                ItemUsage = Usage.Standard;
            }
        }

        public override string ToString()
        {
            return Description;
        }

        public UInt16 GetValue(byte[] init)
        {
            UInt16 optionValue;
            optionValue = GetOptionValue(init);

            return GetBitPackedValue(optionValue);
        }

        public Tuple<UInt16, String, Usage> GetValueDescriptionUsage(byte[] init)
        {
            UInt16 optionValue;
            UInt16 value;
            //Tuple<UInt16, string, Usage> result;// = new Tuple<ushort,string,Usage>();
            string description;
            ActorDefinitionItem.Option option;
            //extract value
            optionValue = GetOptionValue(init);

            value = GetBitPackedValue(optionValue);

            if (ItemUsage == Usage.SwitchFlag)
            {
                mzxrules.ZActor.OActors.SwitchFlag sf = (byte)value;
                description = sf.ToString();
            }
            else if (Options.TryGetValue(optionValue, out option))
            {
                description = option.Description;
            }
            else
            {
                description = Description;//= optionValue.ToString("X4");
            }
            return new Tuple<ushort, string, Usage>(value, description, ItemUsage);
        }

        private ushort GetBitPackedValue(ushort optionValue)
        {
            //Get our reduced value
            int shift;
            int tempMask = Mask;

            for (shift = 0; tempMask > 0 && ((tempMask & 1) == 0); tempMask = Mask >> ++shift) ;

            return (ushort)(optionValue >> shift);
        }

        private ushort GetOptionValue(byte[] init)
        {
            int result;
            if (CType == typeof(UInt16) || CType == typeof(Int16))
            {
                result = (init[Index] << 8) | init[Index + 1];
                result &= Mask;
                return (UInt16)result;
            }
            else if (CType == typeof(byte))
            {
                result = init[Index] & Mask;
                return (UInt16)result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}