
'
' Copyright ?2002-2004 Rui Godinho Lopes <rui@ruilopes.com>
' All rights reserved.
'
' This source file(s) may be redistributed unmodified by any means
' PROVIDING they are not sold for profit without the authors expressed
' written consent, and providing that this notice and the authors name
' and all copyright notices remain intact.
'
' Any use of the software in source or binary forms, with or without
' modification, must include, in the user documentation ("About" box and
' printed documentation) and internal comments to the code, notices to
' the end user as follows:
'
' "Portions Copyright ?2002-2004 Rui Godinho Lopes"
'
' An email letting me know that you are using it would be nice as well.
' That's not much to ask considering the amount of work that went into
' this.
'
' THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
' EXPRESS OR IMPLIED. USE IT AT YOUT OWN RISK. THE AUTHOR ACCEPTS NO
' LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
'

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices



' class that exposes needed win32 gdi functions.
Class Win32
    Public Enum Bool
        [False] = 0
        [True]
    End Enum


    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Point
        Public x As Int32
        Public y As Int32

        Public Sub New(x As Int32, y As Int32)
            Me.x = x
            Me.y = y
        End Sub
    End Structure


    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Size
        Public cx As Int32
        Public cy As Int32

        Public Sub New(cx As Int32, cy As Int32)
            Me.cx = cx
            Me.cy = cy
        End Sub
    End Structure


    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure ARGB
        Public Blue As Byte
        Public Green As Byte
        Public Red As Byte
        Public Alpha As Byte
    End Structure


    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure BLENDFUNCTION
        Public BlendOp As Byte
        Public BlendFlags As Byte
        Public SourceConstantAlpha As Byte
        Public AlphaFormat As Byte
    End Structure


    Public Const ULW_COLORKEY As Int32 = &H1
    Public Const ULW_ALPHA As Int32 = &H2
    Public Const ULW_OPAQUE As Int32 = &H4

    Public Const AC_SRC_OVER As Byte = &H0
    Public Const AC_SRC_ALPHA As Byte = &H1


    Public Declare Auto Function UpdateLayeredWindow Lib "user32.dll" (hwnd As IntPtr, hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, hdcSrc As IntPtr, ByRef pprSrc As Point, _
        crKey As Int32, ByRef pblend As BLENDFUNCTION, dwFlags As Int32) As Bool

    Public Declare Auto Function GetDC Lib "user32.dll" (hWnd As IntPtr) As IntPtr

    <DllImport("user32.dll", ExactSpelling:=True)> _
    Public Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As Integer
    End Function

    Public Declare Auto Function CreateCompatibleDC Lib "gdi32.dll" (hDC As IntPtr) As IntPtr

    Public Declare Auto Function DeleteDC Lib "gdi32.dll" (hdc As IntPtr) As Bool

    <DllImport("gdi32.dll", ExactSpelling:=True)> _
    Public Shared Function SelectObject(hDC As IntPtr, hObject As IntPtr) As IntPtr
    End Function

    Public Declare Auto Function DeleteObject Lib "gdi32.dll" (hObject As IntPtr) As Bool
End Class



''' <para>Your PerPixel form should inherit this class</para>
''' <author><name>Rui Godinho Lopes</name><email>rui@ruilopes.com</email></author>
Class PerPixelAlphaForm
    Inherits Form
    Public Sub New()
        ' This form should not have a border or else Windows will clip it.
        FormBorderStyle = FormBorderStyle.None
    End Sub


    ''' <para>Changes the current bitmap.</para>
    Public Sub SetBitmap(bitmap As Bitmap)
        SetBitmap(bitmap, 255)
    End Sub


    ''' <para>Changes the current bitmap with a custom opacity level.  Here is where all happens!</para>
    Public Sub SetBitmap(bitmap As Bitmap, opacity As Byte)
        If bitmap.PixelFormat <> PixelFormat.Format32bppArgb Then
            Throw New ApplicationException("The bitmap must be 32ppp with alpha-channel.")
        End If

        ' The ideia of this is very simple,
        ' 1. Create a compatible DC with screen;
        ' 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
        ' 3. Call the UpdateLayeredWindow.

        Dim screenDc As IntPtr = Win32.GetDC(IntPtr.Zero)
        Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDc)
        Dim hBitmap As IntPtr = IntPtr.Zero
        Dim oldBitmap As IntPtr = IntPtr.Zero

        Try
            hBitmap = bitmap.GetHbitmap(Color.FromArgb(0))
            ' grab a GDI handle from this GDI+ bitmap
            oldBitmap = Win32.SelectObject(memDc, hBitmap)

            Dim size As New Win32.Size(bitmap.Width, bitmap.Height)
            Dim pointSource As New Win32.Point(0, 0)
            Dim topPos As New Win32.Point(Left, Top)
            Dim blend As New Win32.BLENDFUNCTION()
            blend.BlendOp = Win32.AC_SRC_OVER
            blend.BlendFlags = 0
            blend.SourceConstantAlpha = opacity
            blend.AlphaFormat = Win32.AC_SRC_ALPHA

            Win32.UpdateLayeredWindow(Handle, screenDc, topPos, size, memDc, pointSource, _
                0, blend, Win32.ULW_ALPHA)
        Finally
            Win32.ReleaseDC(IntPtr.Zero, screenDc)
            If hBitmap <> IntPtr.Zero Then
                Win32.SelectObject(memDc, oldBitmap)
                'Windows.DeleteObject(hBitmap); // The documentation says that we have to use the Windows.DeleteObject... but since there is no such method I use the normal DeleteObject from Win32 GDI and it's working fine without any resource leak.
                Win32.DeleteObject(hBitmap)
            End If
            Win32.DeleteDC(memDc)
        End Try
    End Sub


    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80000
            ' This form has to have the WS_EX_LAYERED extended style
            Return cp
        End Get
    End Property
End Class