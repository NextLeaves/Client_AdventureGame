using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class NetworkManager
{
    public static Connection ConnClient = new Connection();

    public static void Update()
    {
        ConnClient.Update();
    }

    public static ProtocolBase GetHeartBeatProtocol()
    {
        ProtocolByte protocol = new ProtocolByte();
        protocol.AddInfo<string>("HeartBeat");
        return protocol;
    }
}

