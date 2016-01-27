// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*============================================================
**
**
**
[System.Runtime.InteropServices.ComVisible(true)]
** A class to hold public guids for languages types.
**
** 
===========================================================*/
namespace System.Diagnostics.SymbolStore {
    // Only statics, does not need to be marked with the serializable attribute
    using System;

[System.Runtime.InteropServices.ComVisible(true)]
    public class SymLanguageType
    {
        public static readonly Guid C = new Guid(0x63a08714, unchecked((short) 0xfc37), 0x11d2, 0x90, 0x4c, 0x0, 0xc0, 0x4f, 0xa3, 0x02, 0xa1);
        public static readonly Guid CPlusPlus = new Guid(0x3a12d0b7, unchecked((short)0xc26c), 0x11d0, 0xb4, 0x42, 0x0, 0xa0, 0x24, 0x4a, 0x1d, 0xd2);
    
        public static readonly Guid CSharp = new Guid(0x3f5162f8, unchecked((short)0x07c6), 0x11d3, 0x90, 0x53, 0x0, 0xc0, 0x4f, 0xa3, 0x02, 0xa1);
    
        public static readonly Guid Basic = new Guid(0x3a12d0b8, unchecked((short)0xc26c), 0x11d0, 0xb4, 0x42, 0x0, 0xa0, 0x24, 0x4a, 0x1d, 0xd2);
    
        public static readonly Guid Java = new Guid(0x3a12d0b4, unchecked((short)0xc26c), 0x11d0, 0xb4, 0x42, 0x0, 0xa0, 0x24, 0x4a, 0x1d, 0xd2);
    
        public static readonly Guid Cobol = new Guid(unchecked((int)0xaf046cd1), unchecked((short)0xd0e1), 0x11d2, 0x97, 0x7c, 0x0, 0xa0, 0xc9, 0xb4, 0xd5, 0xc);
    
        public static readonly Guid Pascal = new Guid(unchecked((int)0xaf046cd2), unchecked((short) 0xd0e1), 0x11d2, 0x97, 0x7c, 0x0, 0xa0, 0xc9, 0xb4, 0xd5, 0xc);
    
        public static readonly Guid ILAssembly = new Guid(unchecked((int)0xaf046cd3), unchecked((short)0xd0e1), 0x11d2, 0x97, 0x7c, 0x0, 0xa0, 0xc9, 0xb4, 0xd5, 0xc);
    
        public static readonly Guid JScript = new Guid(0x3a12d0b6, unchecked((short)0xc26c), 0x11d0, 0xb4, 0x42, 0x00, 0xa0, 0x24, 0x4a, 0x1d, 0xd2);
    
        public static readonly Guid SMC = new Guid(unchecked((int)0xd9b9f7b), 0x6611, unchecked((short)0x11d3), 0xbd, 0x2a, 0x0, 0x0, 0xf8, 0x8, 0x49, 0xbd);
    
        public static readonly Guid MCPlusPlus = new Guid(0x4b35fde8, unchecked((short)0x07c6), 0x11d3, 0x90, 0x53, 0x0, 0xc0, 0x4f, 0xa3, 0x02, 0xa1);
    }
}
