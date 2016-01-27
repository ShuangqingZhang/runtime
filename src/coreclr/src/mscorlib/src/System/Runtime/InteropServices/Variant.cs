// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Runtime.InteropServices {
    using System.Diagnostics;

    /// <summary>
    /// Variant is the basic COM type for late-binding. It can contain any other COM data type.
    /// This type definition precisely matches the unmanaged data layout so that the struct can be passed
    /// to and from COM calls.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [System.Security.SecurityCritical]
    internal struct Variant {

#if DEBUG
        static Variant() {
            // Variant size is the size of 4 pointers (16 bytes) on a 32-bit processor, 
            // and 3 pointers (24 bytes) on a 64-bit processor.
            int variantSize = Marshal.SizeOf(typeof(Variant));
            if (IntPtr.Size == 4) {
                BCLDebug.Assert(variantSize == (4 * IntPtr.Size), "variant");
            } else {
                BCLDebug.Assert(IntPtr.Size == 8, "variant");
                BCLDebug.Assert(variantSize == (3 * IntPtr.Size), "variant");
            }
        }
#endif

        // Most of the data types in the Variant are carried in _typeUnion
        [FieldOffset(0)] private TypeUnion _typeUnion;

        // Decimal is the largest data type and it needs to use the space that is normally unused in TypeUnion._wReserved1, etc.
        // Hence, it is declared to completely overlap with TypeUnion. A Decimal does not use the first two bytes, and so
        // TypeUnion._vt can still be used to encode the type.
        [FieldOffset(0)] private Decimal _decimal;

        [StructLayout(LayoutKind.Sequential)]
        private struct TypeUnion {
            internal ushort _vt;
            internal ushort _wReserved1;
            internal ushort _wReserved2;
            internal ushort _wReserved3;

            internal UnionTypes _unionTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Record {
            private IntPtr _record;
            private IntPtr _recordInfo;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
        [StructLayout(LayoutKind.Explicit)]
        private struct UnionTypes {
            #region Generated Variant union types

            // *** BEGIN GENERATED CODE ***
            // generated by function: gen_UnionTypes from: generate_comdispatch.py

            [FieldOffset(0)] internal SByte _i1;
            [FieldOffset(0)] internal Int16 _i2;
            [FieldOffset(0)] internal Int32 _i4;
            [FieldOffset(0)] internal Int64 _i8;
            [FieldOffset(0)] internal Byte _ui1;
            [FieldOffset(0)] internal UInt16 _ui2;
            [FieldOffset(0)] internal UInt32 _ui4;
            [FieldOffset(0)] internal UInt64 _ui8;
            [FieldOffset(0)] internal Int32 _int;
            [FieldOffset(0)] internal UInt32 _uint;
            [FieldOffset(0)] internal Int16 _bool;
            [FieldOffset(0)] internal Int32 _error;
            [FieldOffset(0)] internal Single _r4;
            [FieldOffset(0)] internal Double _r8;
            [FieldOffset(0)] internal Int64 _cy;
            [FieldOffset(0)] internal double _date;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
            [FieldOffset(0)] internal IntPtr _bstr;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
            [FieldOffset(0)] internal IntPtr _unknown;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
            [FieldOffset(0)] internal IntPtr _dispatch;

            // *** END GENERATED CODE ***

            #endregion

            [FieldOffset(0)] internal IntPtr _pvarVal;
            [FieldOffset(0)] internal IntPtr _byref;
            [FieldOffset(0)] internal Record _record;
        }

        /// <summary>
        /// Primitive types are the basic COM types. It includes valuetypes like ints, but also reference types
        /// like BStrs. It does not include composite types like arrays and user-defined COM types (IUnknown/IDispatch).
        /// </summary>
        internal static bool IsPrimitiveType(VarEnum varEnum) {
            switch(varEnum) {
                #region Generated Variant IsPrimitiveType

                // *** BEGIN GENERATED CODE ***
                // generated by function: gen_IsPrimitiveType from: generate_comdispatch.py

                case VarEnum.VT_I1:
                case VarEnum.VT_I2:
                case VarEnum.VT_I4:
                case VarEnum.VT_I8:
                case VarEnum.VT_UI1:
                case VarEnum.VT_UI2:
                case VarEnum.VT_UI4:
                case VarEnum.VT_UI8:
                case VarEnum.VT_INT:
                case VarEnum.VT_UINT:
                case VarEnum.VT_BOOL:
                case VarEnum.VT_R4:
                case VarEnum.VT_R8:
                case VarEnum.VT_DECIMAL:
                case VarEnum.VT_DATE:
                case VarEnum.VT_BSTR:

                // *** END GENERATED CODE ***

                #endregion
                    return true;
            }

            return false;
        }

        unsafe public void CopyFromIndirect(object value) {

            VarEnum vt = (VarEnum)(((int)this.VariantType) & ~((int)VarEnum.VT_BYREF));

            if (value == null) {
                if (vt == VarEnum.VT_DISPATCH || vt == VarEnum.VT_UNKNOWN || vt == VarEnum.VT_BSTR) {
                    *(IntPtr*)this._typeUnion._unionTypes._byref = IntPtr.Zero;
                }
                return;
            }

            switch (vt) {
                case VarEnum.VT_I1:
                    *(sbyte*)this._typeUnion._unionTypes._byref = (sbyte)value;
                    break;

                case VarEnum.VT_UI1:
                    *(byte*)this._typeUnion._unionTypes._byref = (byte)value;
                    break;

                case VarEnum.VT_I2:
                    *(short*)this._typeUnion._unionTypes._byref = (short)value;
                    break;

                case VarEnum.VT_UI2:
                    *(ushort*)this._typeUnion._unionTypes._byref = (ushort)value;
                    break;

                case VarEnum.VT_BOOL:
                    *(short*)this._typeUnion._unionTypes._byref = (bool)value ? (short)-1 : (short)0;
                    break;

                case VarEnum.VT_I4:
                case VarEnum.VT_INT:
                    *(int*)this._typeUnion._unionTypes._byref = (int)value;
                    break;

                case VarEnum.VT_UI4:
                case VarEnum.VT_UINT:
                    *(uint*)this._typeUnion._unionTypes._byref = (uint)value;
                    break;

                case VarEnum.VT_ERROR:
                    *(int*)this._typeUnion._unionTypes._byref = ((ErrorWrapper)value).ErrorCode;
                    break;

                case VarEnum.VT_I8:
                    *(Int64*)this._typeUnion._unionTypes._byref = (Int64)value;
                    break;

                case VarEnum.VT_UI8:
                    *(UInt64*)this._typeUnion._unionTypes._byref = (UInt64)value;
                    break;

                case VarEnum.VT_R4:
                    *(float*)this._typeUnion._unionTypes._byref = (float)value;
                    break;

                case VarEnum.VT_R8:
                    *(double*)this._typeUnion._unionTypes._byref = (double)value;
                    break;

                case VarEnum.VT_DATE:
                    *(double*)this._typeUnion._unionTypes._byref = ((DateTime)value).ToOADate();
                    break;

                case VarEnum.VT_UNKNOWN:
                    *(IntPtr*)this._typeUnion._unionTypes._byref = Marshal.GetIUnknownForObject(value);
                    break;

                case VarEnum.VT_DISPATCH:
                    *(IntPtr*)this._typeUnion._unionTypes._byref = Marshal.GetIDispatchForObject(value);
                    break;

                case VarEnum.VT_BSTR:
                    *(IntPtr*)this._typeUnion._unionTypes._byref = Marshal.StringToBSTR((string)value);
                    break;

                case VarEnum.VT_CY:
                    *(long*)this._typeUnion._unionTypes._byref = decimal.ToOACurrency((decimal)value);
                    break;

                case VarEnum.VT_DECIMAL:
                    *(decimal*)this._typeUnion._unionTypes._byref = (decimal)value;
                    break;

                case VarEnum.VT_VARIANT:
                    Marshal.GetNativeVariantForObject(value, this._typeUnion._unionTypes._byref);
                    break;

                default:
                    throw new ArgumentException("invalid argument type");
            }
        }

        /// <summary>
        /// Get the managed object representing the Variant.
        /// </summary>
        /// <returns></returns>
        public object ToObject() {
            // Check the simple case upfront
            if (IsEmpty) {
                return null;
            }

            switch (VariantType) {
                case VarEnum.VT_NULL: return DBNull.Value;

                #region Generated Variant ToObject

                // *** BEGIN GENERATED CODE ***
                // generated by function: gen_ToObject from: generate_comdispatch.py

                case VarEnum.VT_I1: return AsI1;
                case VarEnum.VT_I2: return AsI2;
                case VarEnum.VT_I4: return AsI4;
                case VarEnum.VT_I8: return AsI8;
                case VarEnum.VT_UI1: return AsUi1;
                case VarEnum.VT_UI2: return AsUi2;
                case VarEnum.VT_UI4: return AsUi4;
                case VarEnum.VT_UI8: return AsUi8;
                case VarEnum.VT_INT: return AsInt;
                case VarEnum.VT_UINT: return AsUint;
                case VarEnum.VT_BOOL: return AsBool;
                case VarEnum.VT_ERROR: return AsError;
                case VarEnum.VT_R4: return AsR4;
                case VarEnum.VT_R8: return AsR8;
                case VarEnum.VT_DECIMAL: return AsDecimal;
                case VarEnum.VT_CY: return AsCy;
                case VarEnum.VT_DATE: return AsDate;
                case VarEnum.VT_BSTR: return AsBstr;
                case VarEnum.VT_UNKNOWN: return AsUnknown;
                case VarEnum.VT_DISPATCH: return AsDispatch;
                // VarEnum.VT_VARIANT is handled by Marshal.GetObjectForNativeVariant below

                // *** END GENERATED CODE ***

                #endregion

                default:
                    try {
                        unsafe {
                            fixed (void* pThis = &this) {
                                return Marshal.GetObjectForNativeVariant((System.IntPtr)pThis);
                            }
                        }
                    }
                    catch (Exception ex) {
                        throw new NotImplementedException("Variant.ToObject cannot handle" + VariantType, ex);
                    }
            }
        }

        /// <summary>
        /// Release any unmanaged memory associated with the Variant
        /// </summary>
        /// <returns></returns>
        public void Clear() {
            // We do not need to call OLE32's VariantClear for primitive types or ByRefs
            // to safe ourselves the cost of interop transition.
            // ByRef indicates the memory is not owned by the VARIANT itself while
            // primitive types do not have any resources to free up.
            // Hence, only safearrays, BSTRs, interfaces and user types are 
            // handled differently.
            VarEnum vt = VariantType;
            if ((vt & VarEnum.VT_BYREF) != 0) {
                VariantType = VarEnum.VT_EMPTY;
            } else if (
                ((vt & VarEnum.VT_ARRAY) != 0) ||
                ((vt) == VarEnum.VT_BSTR) ||
                ((vt) == VarEnum.VT_UNKNOWN) ||
                ((vt) == VarEnum.VT_DISPATCH) ||
                ((vt) == VarEnum.VT_VARIANT) ||
                ((vt) == VarEnum.VT_RECORD) ||
                ((vt) == VarEnum.VT_VARIANT)
                ) {
                unsafe {
                    fixed (void* pThis = &this) {
                        NativeMethods.VariantClear((IntPtr)pThis);
                    }
                }
                BCLDebug.Assert(IsEmpty, "variant");
            } else {
                VariantType = VarEnum.VT_EMPTY;
            }
        }

        public VarEnum VariantType { 
            get { 
                return (VarEnum)_typeUnion._vt; 
            }
            set {
                _typeUnion._vt = (ushort)value;
            }
        }

        internal bool IsEmpty { 
            get { 
                return _typeUnion._vt == ((ushort)VarEnum.VT_EMPTY); 
            }
        }

        internal bool IsByRef {
            get {
                return (_typeUnion._vt & ((ushort)VarEnum.VT_BYREF)) != 0;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")] 
        public void SetAsNULL() {
            BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
            VariantType = VarEnum.VT_NULL;
        }

        #region Generated Variant accessors

        // *** BEGIN GENERATED CODE ***
        // generated by function: gen_accessors from: generate_comdispatch.py

        // VT_I1

        public SByte AsI1 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_I1, "variant");
                return _typeUnion._unionTypes._i1;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_I1;
                _typeUnion._unionTypes._i1 = value;
            }
        }

        // VT_I2

        public Int16 AsI2 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_I2, "variant");
                return _typeUnion._unionTypes._i2;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_I2;
                _typeUnion._unionTypes._i2 = value;
            }
        }

        // VT_I4

        public Int32 AsI4 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_I4, "variant");
                return _typeUnion._unionTypes._i4;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_I4;
                _typeUnion._unionTypes._i4 = value;
            }
        }

        // VT_I8

        public Int64 AsI8 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_I8, "variant");
                return _typeUnion._unionTypes._i8;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_I8;
                _typeUnion._unionTypes._i8 = value;
            }
        }

        // VT_UI1

        public Byte AsUi1 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UI1, "variant");
                return _typeUnion._unionTypes._ui1;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UI1;
                _typeUnion._unionTypes._ui1 = value;
            }
        }

        // VT_UI2

        public UInt16 AsUi2 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UI2, "variant");
                return _typeUnion._unionTypes._ui2;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UI2;
                _typeUnion._unionTypes._ui2 = value;
            }
        }

        // VT_UI4

        public UInt32 AsUi4 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UI4, "variant");
                return _typeUnion._unionTypes._ui4;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UI4;
                _typeUnion._unionTypes._ui4 = value;
            }
        }

        // VT_UI8

        public UInt64 AsUi8 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UI8, "variant");
                return _typeUnion._unionTypes._ui8;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UI8;
                _typeUnion._unionTypes._ui8 = value;
            }
        }

        // VT_INT

        public Int32 AsInt {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_INT, "variant");
                return _typeUnion._unionTypes._int;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_INT;
                _typeUnion._unionTypes._int = value;
            }
        }

        // VT_UINT

        public UInt32 AsUint {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UINT, "variant");
                return _typeUnion._unionTypes._uint;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UINT;
                _typeUnion._unionTypes._uint = value;
            }
        }

        // VT_BOOL

        public bool AsBool {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_BOOL, "variant");
                return _typeUnion._unionTypes._bool != 0;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_BOOL;
                _typeUnion._unionTypes._bool = value ? (short)-1 : (short)0;
            }
        }

        // VT_ERROR

        public Int32 AsError {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_ERROR, "variant");
                return _typeUnion._unionTypes._error;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_ERROR;
                _typeUnion._unionTypes._error = value;
            }
        }

        // VT_R4

        public Single AsR4 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_R4, "variant");
                return _typeUnion._unionTypes._r4;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_R4;
                _typeUnion._unionTypes._r4 = value;
            }
        }

        // VT_R8

        public Double AsR8 {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_R8, "variant");
                return _typeUnion._unionTypes._r8;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_R8;
                _typeUnion._unionTypes._r8 = value;
            }
        }

        // VT_DECIMAL

        public Decimal AsDecimal {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_DECIMAL, "variant");
                // The first byte of Decimal is unused, but usually set to 0
                Variant v = this;
                v._typeUnion._vt = 0;
                return v._decimal;
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_DECIMAL;
                _decimal = value;
                // _vt overlaps with _decimal, and should be set after setting _decimal
                _typeUnion._vt = (ushort)VarEnum.VT_DECIMAL;
            }
        }

        // VT_CY

        public Decimal AsCy {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_CY, "variant");
                return Decimal.FromOACurrency(_typeUnion._unionTypes._cy);
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_CY;
                _typeUnion._unionTypes._cy = Decimal.ToOACurrency(value);
            }
        }

        // VT_DATE

        public DateTime AsDate {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_DATE, "variant");
                return DateTime.FromOADate(_typeUnion._unionTypes._date);
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_DATE;
                _typeUnion._unionTypes._date = value.ToOADate();
            }
        }

        // VT_BSTR

        public String AsBstr {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_BSTR, "variant");
                return (string)Marshal.PtrToStringBSTR(this._typeUnion._unionTypes._bstr);
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_BSTR;
                this._typeUnion._unionTypes._bstr = Marshal.StringToBSTR(value);
            }
        }

        // VT_UNKNOWN

        public Object AsUnknown {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_UNKNOWN, "variant");
                if (_typeUnion._unionTypes._unknown == IntPtr.Zero)
                    return null;
                return Marshal.GetObjectForIUnknown(_typeUnion._unionTypes._unknown);
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_UNKNOWN;
                if (value == null)
                    _typeUnion._unionTypes._unknown = IntPtr.Zero;
                else
                    _typeUnion._unionTypes._unknown = Marshal.GetIUnknownForObject(value);
            }
        }

        // VT_DISPATCH

        public Object AsDispatch {
            get {
                BCLDebug.Assert(VariantType == VarEnum.VT_DISPATCH, "variant");
                if (_typeUnion._unionTypes._dispatch == IntPtr.Zero)
                    return null;
                return Marshal.GetObjectForIUnknown(_typeUnion._unionTypes._dispatch);
            }
            set {
                BCLDebug.Assert(IsEmpty, "variant"); // The setter can only be called once as VariantClear might be needed otherwise
                VariantType = VarEnum.VT_DISPATCH;
                if (value == null)
                    _typeUnion._unionTypes._dispatch = IntPtr.Zero;
                else
                    _typeUnion._unionTypes._dispatch = Marshal.GetIDispatchForObject(value);
            }
        }


        // *** END GENERATED CODE ***

        internal IntPtr AsByRefVariant
        {
            get {
                BCLDebug.Assert(VariantType == (VarEnum.VT_BYREF | VarEnum.VT_VARIANT), "variant");
                return _typeUnion._unionTypes._pvarVal;
            }
        }
        
        #endregion
    }
}
