using System;

namespace TmpMail
{
    public class EmailAddress
    {
        internal EmailAddress() { }

        public string Address { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} <{Address}>";
        }
    }
}
