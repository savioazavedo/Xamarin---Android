



Public Class Person

    Public FirstName As String
    Public LastName As String
    Protected Age As Integer
    Public BirthYear As Integer


    Protected Sub CalculateAge()
        Age = Year(Date.UtcNow) - BirthYear
    End Sub

    Public Sub GetDetails()
        CalculateAge()
        MsgBox("Name" & FirstName & " " & LastName & " Age:" & Age)
    End Sub

End Class
