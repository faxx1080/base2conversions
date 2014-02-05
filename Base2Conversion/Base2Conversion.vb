' Author: Frank Migliorino

Public Class Base2Conversion

    'Frank Migliorino - 1/24/2010
    'Values obtained from http://www.onlineconversion.com/computer_base2.htm

    'Notes
    '1/8388608      1/1048576        1/1024          1/1
    'Bit (2^(−23))  Byte (2^(−20))	 KB (2^(−10))	MB (2^0)
    '1024/1         1048576/1        1073741824/1	
    'GB (2^10)	    Terabyte (2^20)  Petabyte (2^30)	

    'So,

    '1 MB ->  1024 KB :

    'x * (2^(z−y))
    'x = input value
    'y = output exponent
    'z = input exponent


    ''' <summary>
    ''' The exponental values of prefixes. Ex: BitExp = -23 since 1 Bit = 2^-23 MB.
    ''' </summary>
    ''' <remarks></remarks>
    Enum PrefixExpValues
        BitExp = -23
        ByteExp = -20
        KilobyteExp = -10
        MegabyteExp = 0
        GigabyteExp = 10
        TerabyteExp = 20
        PetabyteExp = 30
    End Enum

    Dim cbxStrings As String() = New String() _
    {"Bits", "Bytes", "Kilobytes (KB)", "Megabytes (MB)", "Gigabytes (GB)", "Terabytes", "Petabytes"}

    ''' <summary>
    ''' Returns a Long converted from one 2-based prefix to another.
    ''' </summary>
    ''' <param name="value">Original Long to be converted</param>
    ''' <param name="inputType">The original type of number (ex. byte, Kilobyte, etc..)</param>
    ''' <param name="outputType">The type to convert the number to.</param>
    ''' <param name="err">Returns any error messages, blank if none.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Long, ByVal inputType As PrefixExpValues, ByVal outputType As PrefixExpValues, ByRef err As String) As Long
        Try
            Return value * (2 ^ (inputType - outputType))
        Catch ex As OverflowException
            err = ex.Message
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Returns a Double converted from one 2-based prefix to another.
    ''' </summary>
    ''' <param name="value">Original Double to be converted</param>
    ''' <param name="inputType">The original type of number (ex. byte, Kilobyte, etc..</param>
    ''' <param name="outputType">The type to convert the number to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Double, ByVal inputType As PrefixExpValues, ByVal outputType As PrefixExpValues) As Double
        Return value * (2 ^ (inputType - outputType))
    End Function

    ''' <summary>
    ''' Returns a Decimal converted from one 2-based prefix to another.
    ''' </summary>
    ''' <param name="value">Original Decimal to be converted</param>
    ''' <param name="inputType">The original type of number (ex. byte, Kilobyte, etc..</param>
    ''' <param name="outputType">The type to convert the number to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Decimal, ByVal inputType As PrefixExpValues, ByVal outputType As PrefixExpValues, ByRef err As String) As Decimal
        Try
            Return value * (2 ^ (inputType - outputType))
        Catch ex As OverflowException
            Err = ex.Message
            Return -1
        End Try
    End Function

    Sub TypeFromString(ByRef inputType As String, ByRef outputType As String)
        If inputType = cbxStrings(0) Then
            inputType = PrefixExpValues.BitExp
        ElseIf inputType = cbxStrings(1) Then
            inputType = PrefixExpValues.ByteExp
        ElseIf inputType = cbxStrings(2) Then
            inputType = PrefixExpValues.KilobyteExp
        ElseIf inputType = cbxStrings(3) Then
            inputType = PrefixExpValues.MegabyteExp
        ElseIf inputType = cbxStrings(4) Then
            inputType = PrefixExpValues.GigabyteExp
        ElseIf inputType = cbxStrings(5) Then
            inputType = PrefixExpValues.TerabyteExp
        ElseIf inputType = cbxStrings(6) Then
            inputType = PrefixExpValues.PetabyteExp
        End If

        If outputType = cbxStrings(0) Then
            outputType = PrefixExpValues.BitExp
        ElseIf outputType = cbxStrings(1) Then
            outputType = PrefixExpValues.ByteExp
        ElseIf outputType = cbxStrings(2) Then
            outputType = PrefixExpValues.KilobyteExp
        ElseIf outputType = cbxStrings(3) Then
            outputType = PrefixExpValues.MegabyteExp
        ElseIf outputType = cbxStrings(4) Then
            outputType = PrefixExpValues.GigabyteExp
        ElseIf outputType = cbxStrings(5) Then
            outputType = PrefixExpValues.TerabyteExp
        ElseIf outputType = cbxStrings(6) Then
            outputType = PrefixExpValues.PetabyteExp
        End If
    End Sub



    Function Convert(ByVal value As Long, ByVal inputType As String, ByVal outputType As String, ByRef err As String) As Long
        TypeFromString(inputType, outputType)
        Try
            Return value * (2 ^ (inputType - outputType))
        Catch ex As OverflowException
            err = ex.Message
            Return -1
        End Try
    End Function

    Function Convert(ByVal value As Double, ByVal inputType As String, ByVal outputType As String) As Double
        TypeFromString(inputType, outputType)
        Return value * (2 ^ (inputType - outputType))
    End Function

    Function Convert(ByVal value As Decimal, ByVal inputType As String, ByVal outputType As String, ByRef err As String) As Decimal
        TypeFromString(inputType, outputType)
        Try
            Return value * (2 ^ (inputType - outputType))
        Catch ex As OverflowException
            err = ex.Message
            Return -1
        End Try
    End Function
End Class
