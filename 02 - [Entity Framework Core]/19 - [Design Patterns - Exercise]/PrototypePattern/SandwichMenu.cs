using System.Collections.Generic;

namespace PrototypePattern
{
    public class SandwichMenu
    {
        private readonly Dictionary<string, SandwichPrototype> sandwiches = new Dictionary<string, SandwichPrototype>();

        public SandwichPrototype this[string name]
        {
            get
            {
                return this.sandwiches[name];
            }
            set
            {
                this.sandwiches[name] = value;
            }
        }
    }
}