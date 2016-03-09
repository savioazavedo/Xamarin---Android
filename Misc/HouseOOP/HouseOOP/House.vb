



Public Class House

    Public Address As String
    Public ExteriorColor As String
    Public InteriorColor As String

    Public No_of_Bedrooms As Integer
    Dim No_of_Bathrooms As Integer




    Public isLawn As Boolean = True

    Public Sub New()
        No_of_Bedrooms = 0
        No_of_Bathrooms = 0
    End Sub

    Public Sub New(Bed As Integer, Bath As Integer)
        No_of_Bedrooms = Bed
        No_of_Bathrooms = Bath
    End Sub

    Public Sub New(Bed As Integer, houseAddress As String)
        No_of_Bedrooms = Bed
        No_of_Bathrooms = getNoofBathrooms()
        Address = houseAddress
    End Sub

    Public Sub GetHouseDetails()
        MsgBox("House Details:" & Address & vbCrLf & "Bedrooms:" & No_of_Bedrooms & " " & "Bathrooms:" & No_of_Bathrooms)
    End Sub

    Public Function getNoofBathrooms() As Double

        If No_of_Bedrooms <= 2 Then
            No_of_Bathrooms = 1
        End If

        If No_of_Bedrooms > 2 And No_of_Bedrooms <= 4 Then
            No_of_Bathrooms = 2
        End If

        If No_of_Bedrooms = 5 Then
            No_of_Bathrooms = 3
        End If

        Return No_of_Bathrooms
    End Function

End Class
