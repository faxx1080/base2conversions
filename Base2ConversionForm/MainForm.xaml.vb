' Author: Frank Migliorino

Imports Base2Conversion
Class MainForm

    Dim cbxStrings As String() = New String() _
    {"Bits", "Bytes", "Kilobytes (KB)", "Megabytes (MB)", "Gigabytes (GB)", "Terabytes", "Petabytes"}

    Private Base2Conversion As New Base2Conversion.Base2Conversion

    Sub ProcessNumbers(ByVal inputType As String, ByVal outputType As String, ByRef txtIn As System.Windows.Controls.TextBox, ByRef txtOut As System.Windows.Controls.TextBox)
        Dim temp As String = ""
        If rdbLong.IsChecked Then temp = "Long"
        If rdbDecimal.IsChecked Then temp = "Decimal"
        If rdbDouble.IsChecked Then temp = "Double"

        Select Case temp
            Case "Long"
                Dim endVal As Long
                endVal = Base2Conversion.Convert(CType(txtIn.Text, Long), inputType, outputType)

                txtOut.Text = endVal

            Case "Decimal"
                Dim endVal As Decimal
                endVal = Base2Conversion.Convert(CType(txtIn.Text, Decimal), inputType, outputType)

                txtOut.Text = If(Not sldDecimalPlaces.Value = 11, Decimal.Round(endVal, decimals:=sldDecimalPlaces.Value), endVal)
                lblDecimalPlacesOut.Content = If(Not sldDecimalPlaces.Value = 11, sldDecimalPlaces.Value, "Max")
            Case "Double"
                Dim endVal As Double
                endVal = Base2Conversion.Convert(CType(txtIn.Text, Double), inputType, outputType)

                txtOut.Text = If(Not sldDecimalPlaces.Value = 11, Math.Round(d:=endVal, decimals:=sldDecimalPlaces.Value), endVal)
                lblDecimalPlacesOut.Content = If(Not sldDecimalPlaces.Value = 11, sldDecimalPlaces.Value, "Max")
        End Select


    End Sub

    Sub ConvertVals(ByVal inputType As String, ByVal outputType As String, ByRef txtIn As System.Windows.Controls.TextBox, ByRef txtOut As System.Windows.Controls.TextBox)
        If cbxInputType.Text <> "" And cbxOutputType.Text <> "" And txtIn.Text <> "" And IsNumeric(txtIn.Text) Then
            ProcessNumbers(inputType, outputType, txtIn, txtOut)
        End If
    End Sub
    Private Sub btnConvert_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnConvert.Click
        Try
            ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
        Catch ex As Exception

        End Try
    End Sub
    'reversed
    'UPDATE: not anymore
    Private Sub cbxOutputType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cbxOutputType.SelectionChanged
        Try
            ConvertVals(cbxInputType.Text, e.AddedItems.Item(0).content, txtIn, txtOut)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cbxInputType_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cbxInputType.SelectionChanged
        Try
            ConvertVals(e.AddedItems.Item(0).content, cbxOutputType.Text, txtIn, txtOut)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rdbDouble_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdbDouble.Click, rdbDecimal.Click, rdbLong.Click
        Try
            ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
        Catch ex As Exception

        End Try
        If CType(sender, RadioButton).Name.Contains("Long") Then
            sldDecimalPlaces.IsEnabled = False
            lblDecimalPlacesOut.Content = 0
        Else
            sldDecimalPlaces.IsEnabled = True
            lblDecimalPlacesOut.Content = If(Not sldDecimalPlaces.Value = 11, sldDecimalPlaces.Value, "Max")
        End If
    End Sub
    'reversed
    'UPDATE: Not anymore
    Private Sub txtOut_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtOut.KeyUp
        Try
            If txtOut.Text - Math.Truncate(Double.Parse(txtOut.Text)) > 0 Then
                rdbLong.IsEnabled = False
                If rdbLong.IsChecked Then rdbDecimal.IsChecked = True
            Else
                rdbLong.IsEnabled = True
            End If
            ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtIn_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtIn.KeyUp
        Try
            If txtIn.Text - Math.Truncate(Double.Parse(txtIn.Text)) > 0 Then
                rdbLong.IsEnabled = False
                If rdbLong.IsChecked Then rdbDecimal.IsChecked = True
            Else
                rdbLong.IsEnabled = True
            End If
            ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSwap_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSwap.Click
        Dim tempStrOut = txtOut.Text
        Dim tempStrTypeIn = cbxInputType.Text
        Dim tempStrTypeOut = cbxOutputType.Text 'temp vars used here to avoid any auto-activating methods
        cbxInputType.Text = tempStrTypeOut
        cbxOutputType.Text = tempStrTypeIn
        txtIn.Text = tempStrOut
        ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
    End Sub

    Private Sub sldDecimalPlaces_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of System.Double)) Handles sldDecimalPlaces.ValueChanged
        ConvertVals(cbxInputType.Text, cbxOutputType.Text, txtIn, txtOut)
        If Not rdbLong.IsChecked Then lblDecimalPlacesOut.Content = If(Not sldDecimalPlaces.Value = 11, sldDecimalPlaces.Value, "Max")
    End Sub

    Private Sub MainForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        sldDecimalPlaces.Value = 11
    End Sub
End Class
