using System;

namespace _20200613_TankLibrary
{
    public class DamageObjNotFoundException : Exception
    {
        public DamageObjNotFoundException()
        {
        }

        public DamageObjNotFoundException(string message)
            : base(message)
        {
        }

        public DamageObjNotFoundException(string message, Exception exp)
            : base(message, exp)
        {
        }
    }
}
