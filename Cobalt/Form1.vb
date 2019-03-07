Imports System
Imports System.IO

Public Class Form1
    Dim Cpt As Integer = 0
    Dim first As Boolean = True
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Files(100) As String
        Dim Result As String = ""
        Dim Final As String
        'Dim itemindex As Int32 = Array.IndexOf(Files, "item test")
        'Dim itemname As String = Files(itemindex)
        Label1.BackColor = Color.FromArgb(23, 25, 31)
        ListBox1.BackColor = Color.FromArgb(23, 25, 31)
        TrackBar1.BackColor = Color.FromArgb(23, 25, 31)
        'ListBox1.Items.AddRange(Directory.GetFiles(My.Computer.FileSystem.SpecialDirectories.MyMusic))

        Try
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.SpecialDirectories.MyMusic)

                Files = foundFile.Split("\\")


                For Each element As String In Files
                    Result = element
                Next

                If (Result.EndsWith(".mp3")) Then
                    Final = Result.Replace(".mp3", "")
                    ListBox1.Items.Add(Final)
                End If



            Next
        Catch ex As Exception
            MsgBox("Cobalt couldn't find any songs in your default Music directory.", vbCritical)
        End Try
        TrackBar1.Minimum = 0
        Timer1.Interval = 100

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

        AxWindowsMediaPlayer1.Ctlcontrols.play()


            Timer1.Start()

        PictureBox7.Visible = True
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        AxWindowsMediaPlayer1.Ctlcontrols.pause()
        PictureBox7.Visible = False
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        AxWindowsMediaPlayer1.URL = My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3"
        PictureBox7.Visible = True
        Label1.Text = ListBox1.SelectedItem
        Dim file As TagLib.File = TagLib.File.Create(My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3")
        PictureBox8.Visible = True
        If file.Tag.Pictures.Length >= 1 Then
            Dim bin As Byte() = DirectCast(file.Tag.Pictures(0).Data.Data, Byte())
            PictureBox8.Image = Image.FromStream(New MemoryStream(bin)).GetThumbnailImage(180, 180, Nothing, System.IntPtr.Zero)

        Else
            PictureBox8.Visible = False
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        ListBox1.SelectedIndex += +1
        AxWindowsMediaPlayer1.URL = My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3"
        Dim file As TagLib.File = TagLib.File.Create(My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3")
        PictureBox8.Visible = True
        If file.Tag.Pictures.Length >= 1 Then
            Dim bin As Byte() = DirectCast(file.Tag.Pictures(0).Data.Data, Byte())
            PictureBox8.Image = Image.FromStream(New MemoryStream(bin)).GetThumbnailImage(180, 180, Nothing, System.IntPtr.Zero)

        Else
            PictureBox8.Visible = False
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        ListBox1.SelectedIndex += -1
        Dim file As TagLib.File = TagLib.File.Create(My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3")
        PictureBox8.Visible = True
        If file.Tag.Pictures.Length >= 1 Then
            Dim bin As Byte() = DirectCast(file.Tag.Pictures(0).Data.Data, Byte())
            PictureBox8.Image = Image.FromStream(New MemoryStream(bin)).GetThumbnailImage(180, 180, Nothing, System.IntPtr.Zero)

        Else
            PictureBox8.Visible = False
        End If
        AxWindowsMediaPlayer1.URL = My.Computer.FileSystem.SpecialDirectories.MyMusic + "\" + ListBox1.SelectedItem + ".mp3"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            TrackBar1.Maximum = CInt(AxWindowsMediaPlayer1.currentMedia.duration.ToString)

            TrackBar1.Value = CInt(AxWindowsMediaPlayer1.Ctlcontrols.currentPosition)
        Catch ex2 As Exception
            Timer1.Stop()
            If first Then
                MsgBox("No song to play", vbCritical)
                first = False
            End If

        End Try

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        AxWindowsMediaPlayer1.Ctlcontrols.pause()
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = CDbl(TrackBar1.Value)
        System.Threading.Thread.Sleep(300)
        AxWindowsMediaPlayer1.Ctlcontrols.play()
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim Search As String = TextBox1.Text
        Dim IndexItem As Integer = ListBox1.FindString(Search)
        If (IndexItem <> -1) Then
            ListBox1.SetSelected(IndexItem, True)
            'Label1.Text = Search
        Else
            MsgBox("Couldn't find song", vbCritical)
        End If
    End Sub
End Class
