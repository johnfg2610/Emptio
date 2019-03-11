using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class DefaultIdGenerator : IdGenerator
    {
        public Guid GenerateID()
        {
            return Guid.NewGuid();
        }
    }
}
