using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.C_TFTPClient
{
    class opCodes
    {
        private static readonly byte[] REQUEST = new byte[] { 0x0, 0x01 };

        private static readonly byte[] DATA = new byte[] { 0x0, 0x03 };

        private static readonly byte[] ACK = new byte[] { 0x0, 0x04 };

        private static readonly byte[] ERROR = new byte[] { 0x0, 0x05 };

        private static readonly byte[] ASCII_ZERO = new byte[] { 0x0 };
    }
}
