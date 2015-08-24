''' <summary>
''' Provides methods to convert from a color space to an other.
''' </summary>
Public NotInheritable Class ColorHelper

    Private Sub New()
        '
    End Sub

#Region "Color processing"

    ''' <summary>
    ''' Gets the given color based on a color and an alpha.
    ''' </summary>
    ''' <param name="c">Color applying the alpha channel.</param>
    ''' <param name="alpha">Alpha channel value.</param>
    Public Shared Function GetAlphaColor(ByVal c As Color, ByVal alpha As Integer) As Color

        If (c <> Color.Transparent And (alpha > -1 And alpha < 256)) Then
            Return Color.FromArgb(alpha, c)
        End If

        Return Color.Empty

    End Function


    ''' <summary>
    ''' Blends two colors.
    ''' </summary>
    ''' <param name="c1">First color to blend</param>
    ''' <param name="c2">Second color to blend</param>
    ''' <param name="ratio">Blend ratio. 0.5 will give even blend, 1.0 will return color1, 0.0 will return color2 and so on.</param>
    Public Shared Function GetBlendColor(ByVal c1 As Color, ByVal c2 As Color, ByVal ratio As Double) As Color

        Dim r As Single = CSng(ratio)
        Dim ir As Single = 1.0F - r

        Dim rgb1() As Single = {CSng(c1.R) / 255.0F, _
                                CSng(c1.G) / 255.0F, _
                                CSng(c1.B) / 255.0F}

        Dim rgb2() As Single = {CSng(c2.R) / 255.0F, _
                                CSng(c2.G) / 255.0F, _
                                CSng(c2.B) / 255.0F}

        Return Color.FromArgb(CInt(rgb1(0) * r + rgb2(0) * ir), _
                              CInt(rgb1(1) * r + rgb2(1) * ir), _
                              CInt(rgb1(2) * r + rgb2(2) * ir))

    End Function


    ''' <summary>
    ''' Makes an even blend between two colors.
    ''' </summary>
    ''' <param name="c1">First color to blend</param>
    ''' <param name="c2">Second color to blend</param>
    Public Shared Function GetBlendColor(ByVal c1 As Color, ByVal c2 As Color) As Color
        Return GetBlendColor(c1, c2, 0.5)
    End Function

    ''' <summary>
    ''' Gets the "distance" between two colors.
    ''' RGB colors must be normalized (eg. values in [0.0, 1.0]).
    ''' </summary>
    ''' <param name="r1">First color red component.</param>
    ''' <param name="g1">First color green component.</param>
    ''' <param name="b1">First color blue component.</param>
    ''' <param name="r2">Second color red component.</param>
    ''' <param name="g2">Second color green component.</param>
    ''' <param name="b2">Second color blue component.</param>
    Public Shared Function GetColorDistance(ByVal r1 As Double, ByVal g1 As Double, ByVal b1 As Double, ByVal r2 As Double, ByVal g2 As Double, ByVal b2 As Double) As Double

        Dim a As Double = r2 - r1
        Dim b As Double = g2 - g1
        Dim c As Double = b2 - b1

        Return Math.Sqrt(a * a + b * b + c * c)

    End Function

    ''' <summary>
    ''' Gets the "distance" between two colors.
    ''' RGB colors must be normalized (eg. values in [0.0, 1.0]).
    ''' </summary>
    ''' <param name="color1">First color [r,g,b]</param>
    ''' <param name="color2">Second color [r,g,b]</param>
    Public Shared Function GetColorDistance(ByVal color1() As Double, ByVal color2() As Double) As Double
        Return GetColorDistance(color1(0), color1(1), color1(2), color2(0), color2(1), color2(2))
    End Function

    ''' <summary>
    ''' Gets the "distance" between two colors.
    ''' </summary>
    ''' <param name="color1">First color.</param>
    ''' <param name="color2">Second color.</param>
    Public Shared Function GetColorDistance(ByVal color1 As Color, ByVal color2 As Color) As Double

        Dim rgb1() As Double = {CDbl(color1.R) / 255.0F, _
                                CDbl(color1.G) / 255.0F, _
                                CDbl(color1.B) / 255.0F}

        Dim rgb2() As Double = {CDbl(color2.R) / 255.0F, _
                                CDbl(color2.G) / 255.0F, _
                                CDbl(color2.B) / 255.0F}

        Return GetColorDistance(rgb1, rgb2)

    End Function


#End Region

#Region "Light Spectrum processing"

    ''' <summary>
    ''' Gets visible colors (color wheel).
    ''' </summary>
    ''' <param name="alpha">
    ''' The alpha value used for each colors.
    ''' </param>
    Public Shared Function GetWheelColors(ByVal alpha As Integer) As Color()

        Dim temp As Color
        Dim colorCount As Integer = 6 * 256
        Dim Colors(colorCount) As Color

        For i As Integer = 0 To colorCount - 1
            temp = HSBtoColor((CDbl(i) * 255.0) / CDbl(colorCount), 255.0, 255.0)
            Colors(i) = Color.FromArgb(alpha, temp.R, temp.G, temp.B)
        Next

        Return Colors

    End Function

    ''' <summary>
    ''' Gets visible spectrum colors.
    ''' </summary>
    ''' <param name="alpha">The alpha value used for each colors.</param>
    Public Shared Function GetSpectrumColors(ByVal alpha As Integer) As Color()

        Dim Colors(256 * 6) As Color

        For i As Integer = 0 To 255
            Colors(i) = Color.FromArgb(alpha, 255, i, 0)
            Colors(i + 256) = Color.FromArgb(alpha, 255 - i, 255, 0)
            Colors(i + 256 * 2) = Color.FromArgb(alpha, 0, 255, i)
            Colors(i + 256 * 3) = Color.FromArgb(alpha, 0, 255 - i, 255)
            Colors(i + 256 * 4) = Color.FromArgb(alpha, i, 0, 255)
            Colors(i + 256 * 5) = Color.FromArgb(alpha, 255, 0, 255 - i)
        Next

        Return Colors

    End Function

    ''' <summary>
    ''' Gets visible spectrum colors.
    ''' </summary>
    Public Shared Function GetSpectrumColors() As Color()
        Return GetSpectrumColors(255)
    End Function

#End Region

#Region "Hex Color Conversion"

    ''' <summary>
    ''' Converts a Hex color to a .net Color.
    ''' </summary>
    ''' <param name="hexColor">The desired hexadecimal color to convert.</param>
    Public Shared Function HEXtoColor(ByVal hexColor As String) As Color

        If hexColor Is String.Empty Then Return Color.Empty

        hexColor = hexColor.Trim

        If hexColor.Chars(0) = "#" Then hexColor = hexColor.Substring(1)
        If hexColor.Length < 6 Then hexColor = "000000".Substring(0, 6 - hexColor.Length) + hexColor

        Return Color.FromArgb(CInt("&h" + hexColor.Substring(0, 2)), _
                              CInt("&h" + hexColor.Substring(2, 2)), _
                              CInt("&h" + hexColor.Substring(4, 2)))

    End Function

    ''' <summary>
    ''' Converts a RGB color format to an hexadecimal color.
    ''' </summary>
    ''' <param name="r">The Red value.</param>
    ''' <param name="g">The Green value.</param>
    ''' <param name="b">The Blue value.</param>
    Public Shared Function RGBtoHEX(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As String
        Return String.Format("#{0:x2}{1:x2}{2:x2}", r, g, b).ToUpper
    End Function

    ''' <summary>
    ''' Converts a .Net Color format to an hexadecimal color.
    ''' </summary>
    ''' <param name="c">The .net color to convert.</param>
    Public Shared Function ColorToHEX(ByVal c As Color) As String
        Return RGBtoHEX(c.R, c.G, c.B)
    End Function

#End Region

#Region "HSB Convert"

#Region "HSBtoRGB"

    ''' <summary>
    ''' Converts HSB to RGB.
    ''' </summary>
    ''' <param name="hsb">The HSB structure to convert.</param>
    Public Shared Function HSBtoRGB(ByVal HSB As HSB) As RGB
        Return HSBtoRGB(HSB.Hue, HSB.Saturation, HSB.Brightness)
    End Function

    ''' <summary>
    ''' Converts HSB to RGB.
    ''' </summary>
    ''' <param name="H">Hue value. (must be between 0 and 360)</param>
    ''' <param name="S">Saturation value (must be between 0 and 100).</param>
    ''' <param name="b">Brigthness value (must be between 0 and 100).</param>
    Public Shared Function HSBtoRGB(ByVal h As Integer, ByVal s As Integer, ByVal b As Integer) As RGB
        Return HSBtoRGB(CDbl(h), CDbl(s / 100.0), CDbl(b / 100.0))
    End Function

    ''' <summary> 
    ''' Converts HSB to a RGB.
    ''' </summary>
    ''' <param name="h">Hue value (must be between 0 and 360).</param>
    ''' <param name="s">Saturation value (must be between 0 and 1).</param>
    ''' <param name="b">Brightness value (must be between 0 and 1).</param>
    Public Shared Function HSBtoRGB(ByVal h As Double, ByVal s As Double, ByVal b As Double) As RGB

        Dim red As Double = 0.0
        Dim green As Double = 0.0
        Dim blue As Double = 0.0

        If (s = 0) Then

            red = b
            green = b
            blue = b

        Else

            ' the color wheel consists of 6 sectors. Figure out which sector you're in.
            Dim sectorPos As Double = h / 60.0
            Dim sectorNumber As Integer = CInt(Math.Floor(sectorPos))
            ' get the fractional part of the sector
            Dim fractionalSector As Double = sectorPos - sectorNumber

            ' calculate values for the three axes of the color. 
            Dim p As Double = b * (1.0 - s)
            Dim q As Double = b * (1.0 - (s * fractionalSector))
            Dim t As Double = b * (1.0 - (s * (1 - fractionalSector)))

            ' assign the fractional colors to r, g, and b based on the sector the angle is in.
            Select Case sectorNumber
                Case 0
                    red = b
                    green = t
                    blue = p
                Case 1
                    red = q
                    green = b
                    blue = p
                Case 2
                    red = p
                    green = b
                    blue = t
                Case 3
                    red = p
                    green = q
                    blue = b
                Case 4
                    red = t
                    green = p
                    blue = b
                Case 5
                    red = b
                    green = p
                    blue = q
            End Select

        End If

        'Return New RGB(cint((Math.Ceiling(T(0) * 255.0)), _
        '               cint((Math.Ceiling(T(1) * 255.0)), _
        '               cint((Math.Ceiling(T(2) * 255.0)))

        Return New RGB(CInt(Double.Parse(String.Format("{0:0.00}", red * 255.0))), _
                       CInt(Double.Parse(String.Format("{0:0.00}", green * 255.0))), _
                       CInt(Double.Parse(String.Format("{0:0.00}", blue * 255.0))))


    End Function

#End Region

#Region "HSBtoColor"

    ''' <summary>
    ''' Converts HSB to .Net Color.
    ''' </summary>
    ''' <param name="hsb">the HSB structure to convert.</param>
    Public Shared Function HSBtoColor(ByVal HSB As HSB) As Color
        Return HSBtoColor(HSB.Hue, HSB.Saturation, HSB.Brightness)
    End Function

    ''' <summary> 
    ''' Converts HSB to a .Net Color.
    ''' </summary>
    ''' <param name="h">Hue value (must be between 0 and 360).</param>
    ''' <param name="s">Saturation value (must be between 0 and 1).</param>
    ''' <param name="b">Brightness value (must be between 0 and 1).</param>
    Public Shared Function HSBtoColor(ByVal h As Double, ByVal s As Double, ByVal b As Double) As Color
        Dim rgb As RGB = HSBtoRGB(h, s, b)
        Return Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue)
    End Function

#End Region

#End Region

#Region "RGB Convert"

#Region "RGBtoHSB"

    ''' <summary> 
    ''' Converts .Net Color to HSB.
    ''' </summary> 
    Public Shared Function RGBtoHSB(ByVal c As Color) As HSB
        Return RGBtoHSB(c.R, c.G, c.B)
    End Function

    ''' <summary> 
    ''' Converts RGB to HSB.
    ''' </summary> 
    Public Shared Function RGBtoHSB(ByVal rgb As RGB) As HSB
        Return RGBtoHSB(rgb.Red, rgb.Green, rgb.Blue)
    End Function

    ''' <summary> 
    ''' Converts RGB to HSB.
    ''' </summary> 
    Public Shared Function RGBtoHSB(ByVal red As Integer, ByVal green As Integer, ByVal blue As Integer) As HSB

        Dim h As Double = 0.0
        Dim s As Double = 0.0

        ' normalizes red-green-blue values
        Dim nRed As Double = CDbl(red) / 255.0
        Dim nGreen As Double = CDbl(green) / 255.0
        Dim nBlue As Double = CDbl(blue) / 255.0

        Dim max As Double = Math.Max(nRed, Math.Max(nGreen, nBlue))
        Dim min As Double = Math.Min(nRed, Math.Min(nGreen, nBlue))

        ' Hue
        If (max = nRed) And (nGreen >= nBlue) Then
            If (max - min = 0) Then
                h = 0.0
            Else
                h = 60 * (nGreen - nBlue) / (max - min)
            End If
        ElseIf (max = nRed) And (nGreen < nBlue) Then
            h = 60 * (nGreen - nBlue) / (max - min) + 360
        ElseIf (max = nGreen) Then
            h = 60 * (nBlue - nRed) / (max - min) + 120
        ElseIf (max = nBlue) Then
            h = 60 * (nRed - nGreen) / (max - min) + 240
        End If

        ' Saturation
        If (max = 0) Then
            s = 0.0
        Else
            s = 1.0 - (min / max)
        End If

        Return New HSB(h, s, max)

    End Function

#End Region
#End Region

End Class
