Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports stdole

Friend Class OlePictureHelper

   ' Methods

   <DllImport("oleaut32.dll", EntryPoint:="OleCreatePictureIndirect", ExactSpelling:=True, PreserveSig:=False)> _
   Friend Shared Function OleCreateIPictureIndirect(<MarshalAs(UnmanagedType.AsAny)> ByVal pictdesc As Object, ByRef iid As System.Guid, ByVal fOwn As Boolean) As IPicture
   End Function

   Friend Shared Function OlePictureFromImage(ByVal image As Object) As IPictureDisp
      Dim bmp As Bitmap = TryCast(image, Bitmap)
      If (Not bmp Is Nothing) Then
         Return TryCast(OlePictureHelper.OleCreateIPictureIndirect(New PICTDESCbmp(bmp), (OlePictureHelper.picture_guid), True), IPictureDisp)
      End If
      Dim icon As Icon = TryCast(image, Icon)
      If (Not icon Is Nothing) Then
         Return TryCast(OlePictureHelper.OleCreateIPictureIndirect(New PICTDESCbmp(icon), (OlePictureHelper.picture_guid), True), IPictureDisp)
      End If
      Return Nothing
   End Function

   ' Fields

   Private Shared picture_guid As System.Guid = GetType(IPicture).GUID
   ' Nested Types
   <StructLayout(LayoutKind.Sequential)> _
  Friend Class PICTDESCbmp
      Friend cbSizeOfStruct As Integer
      Friend picType As Integer
      Friend hbitmap As IntPtr
      Friend hpalette As IntPtr
      Friend unused As Integer
      Public Sub New(ByVal bitmap As Bitmap)
         Me.cbSizeOfStruct = Marshal.SizeOf(GetType(PICTDESCbmp))
         Me.picType = 1
         Me.hbitmap = IntPtr.Zero
         Me.hpalette = IntPtr.Zero
         Me.hbitmap = bitmap.GetHbitmap
      End Sub

      Public Sub New(ByVal icon As Icon)
         Me.cbSizeOfStruct = Marshal.SizeOf(GetType(PICTDESCbmp))
         Me.picType = 3
         Me.hbitmap = IntPtr.Zero
         Me.hpalette = IntPtr.Zero
         Me.hbitmap = icon.Handle
      End Sub
   End Class

   Private Enum PICTYPE
      ' Fields
      PICTYPE_BITMAP = 1
      PICTYPE_ENHMETAFILE = 4
      PICTYPE_ICON = 3
      PICTYPE_METAFILE = 2
      PICTYPE_NONE = 0
      PICTYPE_UNINITIALIZED = -1
   End Enum

End Class
