Public Class Form1







    Private Sub btnPerson_Click(sender As Object, e As EventArgs) Handles btnPerson.Click

        Dim objPerson As New Person

        objPerson.FirstName = "John"
        objPerson.LastName = "Doe"
        objPerson.BirthYear = 1980

        objPerson.GetDetails()

    End Sub

    Private Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click


        Dim objStudent As New Student

        objStudent.FirstName = "John"
        objStudent.LastName = "Doe"
        objStudent.BirthYear = 1980


        objStudent.GetStudentDetails()


    End Sub
End Class
