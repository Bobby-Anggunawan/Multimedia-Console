using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace mmc
{

    //Class yang menampung semua fungsi untuk memproses audio
    class Audio
    {

        public static bool Get_Audio(string filePath)       //Mengambil audio dalam video dan di simpan dalam directori ini dengan nama Audio.wav
        {

            string param = String.Join("", "\"", filePath, "\"");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"..\..\ffmpeg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-i " + param + @" -vn audio.wav";

            startInfo.RedirectStandardInput = true;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    StreamWriter myStreamWriter = exeProcess.StandardInput;
                    myStreamWriter.Write("y");

                    myStreamWriter.Close();
                    exeProcess.WaitForExit();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static void Get_Audio_And_Play(string filePath)  //Memanggil fungsi Get_Audio dan menjalankan audio ini.
        {
            bool success = Get_Audio(filePath);

            if (success)
            {
                SoundPlayer audio = new SoundPlayer("audio.wav");
                audio.Play();
            }
            else {
                Console.Write("Audio gagal dijalankan");
            }
        }




    }
}
