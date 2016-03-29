
Public Class Student
    Inherits Person

    Public PapersTaken As String = "Maths,Chemistry"

    Sub GetStudentDetails()
        CalculateAge()
        MsgBox("Name" & FirstName & " " & LastName & " Age:" & Age & " PapersTaken" & PapersTaken)
    End Sub

End Class
