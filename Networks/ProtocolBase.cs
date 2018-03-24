using System;
using System.Collections.Generic;
using System.Linq;


public abstract class ProtocolBase
{
    public abstract string Expression { get; set; }
    public abstract string Name { get; set; }

    public abstract ProtocolBase Decode(byte[] bufferRead, int start, int length);
    public abstract byte[] Encode();

}

