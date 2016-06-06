Imports System.IO

<DebuggerStepThrough()> Public Class IO
    Public Shared Function BracketFileget(ByVal pathname As String) As List(Of Queue(Of String))
        Dim total As New List(Of Queue(Of String))
        Try
            Dim line As String
            Using sr As New StreamReader(pathname)
                Dim current As New Queue(Of String)
                While sr.Peek <> -1
                    line = sr.ReadLine

                    'ignore blank lines and lines that start with -
                    If line = "" Then Continue While
                    If line.StartsWith("-") Then Continue While

                    If line.StartsWith("[") Then
                        'remove brackets
                        line = line.Remove(0, 1)
                        line = line.Remove(line.Length - 1, 1)

                        'if current is filled, add to total
                        If current.Count > 0 Then total.Add(current)

                        'start new current with bracketstring as header
                        current = New Queue(Of String)
                        current.Enqueue(line)
                    Else
                        If line <> "" Then current.Enqueue(line)
                    End If
                End While

                'add last entry
                If current.Count > 0 Then total.Add(current)
            End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
        Return total
    End Function
End Class
