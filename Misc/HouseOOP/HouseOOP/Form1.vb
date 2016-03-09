Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim MyHouse As New House()

        MyHouse.Address = TextBox1.Text
        MyHouse.ExteriorColor = TextBox2.Text
        MyHouse.InteriorColor = TextBox3.Text

        'MyHouse.No_of_Bedrooms = 4
        'MsgBox(MyHouse.getNoofBathrooms())

        MyHouse.GetHouseDetails()


    End Sub
End Class
