Module MainModule

    Sub Out(content As String)
        Console.WriteLine(content)
    End Sub

    Sub Out2(content As String)
        Console.Write(content)
    End Sub

    Function Read() As String
        Out2(">")
        Return Console.ReadLine()
    End Function

    Sub Main()
Retry:
        Try
            Out(System.Windows.Forms.Application.ProductName & " v" & System.Windows.Forms.Application.ProductVersion)
            Out("Copyright (C) 2017 Elepover.")
            Out("")
            Dim originalEncoding As String = ""
            Dim targetEncoding As String = ""
            Dim realOriginalEncoding As System.Text.Encoding
            Dim realTargetEncoding As System.Text.Encoding
            Dim dir As String = ""
            Dim query As String = ""
            Dim count As Integer = 0
            Dim detailed As String = ""
            Dim detailedOutput As Boolean = False
            Dim listout As ObjectModel.ReadOnlyCollection(Of String)
            Out("==> Successfully initialized.")
            Out("==> Enter target directory:")
            dir = Read()
            Out("==> Enter filename type, wildcard supported.")
            query = Read()
            Out(":: Searching...")
            listout = My.Computer.FileSystem.GetFiles(dir, FileIO.SearchOption.SearchTopLevelOnly, query)
            Out("==> Search complete. " & listout.Count & " matching file(s).")
            '0 ascii, 1 bigendianunicode, 2 unicode, 3 utf32, 4 utf7, 5 utf8
            Out("==> Select source encoding: ")
            Out("==> 0: ASCII, 1: Big Endian Unicode, 2: Unicode, 3: UTF-32, 4: UTF-7, 5: UTF-8, 6: Default.")
            originalEncoding = Read()
            Out("==> Select target encoding: ")
            Out("==> 0: ASCII, 1: Big Endian Unicode, 2: Unicode, 3: UTF-32, 4: UTF-7, 5: UTF-8, 6: Default.")
            targetEncoding = Read()
            Out("==> Detailed output? (y/N)")
            detailed = Read()
            Out(":: Processing...")
            If detailed.ToLower = "y" Then
                detailedOutput = True
            Else
                detailedOutput = False
            End If
            Select Case originalEncoding
                Case "0"
                    realOriginalEncoding = System.Text.Encoding.ASCII
                Case "1"
                    realOriginalEncoding = System.Text.Encoding.BigEndianUnicode
                Case "2"
                    realOriginalEncoding = System.Text.Encoding.Unicode
                Case "3"
                    realOriginalEncoding = System.Text.Encoding.UTF32
                Case "4"
                    realOriginalEncoding = System.Text.Encoding.UTF7
                Case "5"
                    realOriginalEncoding = System.Text.Encoding.UTF8
                Case "6"
                    realOriginalEncoding = System.Text.Encoding.Default
                Case Else
                    realOriginalEncoding = System.Text.Encoding.Default
            End Select
            Select Case targetEncoding
                Case "0"
                    realTargetEncoding = System.Text.Encoding.ASCII
                Case "1"
                    realTargetEncoding = System.Text.Encoding.BigEndianUnicode
                Case "2"
                    realTargetEncoding = System.Text.Encoding.Unicode
                Case "3"
                    realTargetEncoding = System.Text.Encoding.UTF32
                Case "4"
                    realTargetEncoding = System.Text.Encoding.UTF7
                Case "5"
                    realTargetEncoding = System.Text.Encoding.UTF8
                Case "6"
                    realTargetEncoding = System.Text.Encoding.Default
                Case Else
                    realTargetEncoding = System.Text.Encoding.Default
            End Select

            Out(":: Converting...")
            For Each f As String In listout
                Dim reader As New IO.StreamReader(f, realOriginalEncoding)
                Dim txt As String = reader.ReadToEnd()
                reader.Close()
                Dim writer As New IO.StreamWriter(f, False, realTargetEncoding) With {.AutoFlush = True}
                writer.Write(txt)
                writer.Close()
                count += 1
                If detailedOutput Then Out("==> [" & count & "/" & listout.Count & "] has been processed.")
            Next
            Out("==> Program complete!")
            Out("==> Press any key to leave.")
            Console.ReadKey(True)
        Catch ex As Exception
            Out("==> EncodingConverter has encountered an error: " & ex.ToString)
            Out("==> Would you like to retry? (Y/n)")
            Dim choice As String = Read()
            If choice?.ToLower = ("n") Then
                End
            Else
                GoTo Retry
            End If
        End Try
    End Sub

End Module
