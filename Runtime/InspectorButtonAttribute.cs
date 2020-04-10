using System;

namespace HGrandry.Helpers
{
    public class InspectorButtonAttribute : Attribute
    {
        public string Name;

        public InspectorButtonAttribute()
        {
            Name = null;
        }

        public InspectorButtonAttribute(string name)
        {
            Name = name;
        }
    }
}