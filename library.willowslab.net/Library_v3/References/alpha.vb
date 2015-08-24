
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
Imports System.Windows.Forms



''' <para>Our test form for this sample application.  The bitmap will be displayed in this window.</para>
Class MyPerPixelAlphaForm
    Inherits PerPixelAlphaForm
    Public Sub New()
        TopMost = True
        ShowInTaskbar = False
    End Sub


    ' Let Windows drag this form for us
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H84 Then
            'WM_NCHITTEST
            m.Result = CType(2, IntPtr)
            ' HTCLIENT
            Return
        End If
        MyBase.WndProc(m)
    End Sub
End Class



'''<para>The "controller" dialog box.</para>
Class MyForm
    Inherits Form
    Public Sub New()
        Font = New Font("tahoma", 8)
        Text = "perpixelalpha# - Sample application"
        FormBorderStyle = FormBorderStyle.FixedDialog
        MinimizeBox = False
        MaximizeBox = False
        ClientSize = New Size(350, 160)
        StartPosition = FormStartPosition.CenterScreen

        AllowDrop = True
        ' Because we want to be a drop target of windows explorer files.
        InitializeComponent()
    End Sub


    '''<para>Constructs and initializes all child controls of this dialog box.</para>
    Private Sub InitializeComponent()
        ' Label with to display current opacity level
        Dim Label1 As New Label()
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(4, 8)
        Label1.Text = "1. Drag&&Drop an image file from windows explorer into this window."
        Controls.Add(Label1)

        Dim Label2 As New Label()
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(4, 38)
        Label2.Text = "2. Play with the opacity level [0..255]:"
        Controls.Add(Label2)

        ' Label with to display current opacity level
        LabelValue = New Label()
        LabelValue.AutoSize = True
        LabelValue.Location = New System.Drawing.Point(195, 38)
        LabelValue.Text = "255"

        Controls.Add(LabelValue)


        ' Trackbar to change opacity level
        Track = New TrackBar()

        Track.Location = New System.Drawing.Point(18, 58)
        Track.Size = New System.Drawing.Size(310, 0)
        Track.BeginInit()
        Track.Maximum = 255
        Track.TickFrequency = 5
        Track.TickStyle = TickStyle.TopLeft
        Track.Value = 255
        Track.EndInit()


        Controls.Add(Track)


        Dim Label3 As New Label()
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(4, 108)
        Label3.Text = "3. Drag the layered window arround you desktop!"
        Controls.Add(Label3)


        ' Label with two links to me! :)
        Dim Link As New LinkLabel()

        Link.Location = New System.Drawing.Point(4, 140)
        Link.Size = New System.Drawing.Size(250, 80)
        Link.Text = "by Rui Lopes <rui@ruilopes.com>"
        Link.Links.Add(3, 9, "http://www.ruilopes.com")
        Link.Links.Add(14, 16, "mailto:rui@ruilopes.com")


        Controls.Add(Link)


        ' TestForm will containt the per-pixel-alpha dib
        TestForm = New MyPerPixelAlphaForm()
        TestForm.Show()
    End Sub


    '''<para>Frees our bitmap.</para>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso bitmap IsNot Nothing Then
                bitmap.Dispose()
                bitmap = Nothing
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    '''<para>Accepts only Drops of windows explorer files.</para>
    Protected Overrides Sub OnDragEnter(e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
        MyBase.OnDragEnter(e)
    End Sub


    '''<para>Just loads the dropped file from windows explorer.</para>
    Protected Overrides Sub OnDragDrop(e As DragEventArgs)
        Dim files As String() = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files IsNot Nothing Then
            If files.Length = 1 Then
                SetPerPixelBitmapFilename(files(0))
            Else
                MessageBox.Show(Me, "Please, drop only one image file.", "Too many files dropped", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
            End If
        End If
        MyBase.OnDragDrop(e)
    End Sub


    '''<para>Just load a image file and display it on our test form.</para>
    Private Sub SetPerPixelBitmapFilename(fileName As String)
        Dim newBitmap As Bitmap

        Try

            newBitmap = TryCast(Image.FromFile(fileName), Bitmap)

            TestForm.SetBitmap(newBitmap, CByte(Track.Value))
        Catch e As ApplicationException
            MessageBox.Show(Me, e.Message, "Error with bitmap.", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return
        Catch e As Exception
            MessageBox.Show(Me, e.Message, "Could not open image file.", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return
        End Try

        If bitmap IsNot Nothing Then
            bitmap.Dispose()
        End If
        bitmap = newBitmap
    End Sub


    '''<para>Change the opacity level of our test form.</para>
    Private Sub Track_ValueChanged(sender As Object, e As EventArgs)
        Dim opacity As Byte = CByte(Track.Value)
        LabelValue.Text = opacity.ToString()
        LabelValue.Refresh()
        ' We need this because on slow computers (mine!) the windows takes some time to update our label.
        If bitmap IsNot Nothing Then
            TestForm.SetBitmap(bitmap, opacity)
        End If
    End Sub


    '''<para>Start the computer browser in the specified uri.</para>
    Private Sub Link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        e.Link.Visited = True
        Using System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
        End Using
    End Sub


    Private LabelValue As Label
    ' label with current opacity level
    Private Track As TrackBar
    ' trackbar to chabge opacity level
    Private TestForm As MyPerPixelAlphaForm
    ' our test form
    Private bitmap As Bitmap
    ' bitmap that is currently displaying on our test form
End Class



' Our Great Application!
Class TheApp
    <STAThread> _
    Public Shared Sub Main()
        Application.Run(New MyForm())
    End Sub
End Class
