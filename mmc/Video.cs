using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Media;
using WMPLib;       //Digunakan untuk mendapat durasi video dengan menggunakan API window media player


namespace mmc
{

    //Class yang menampung semua fungsi untuk memproses video
    class Video
    {
        public static int Durasi_Video(string filePath)     //Mendapat durasi video dalam detik
        {
            var player = new WindowsMediaPlayer();
            var clip = player.newMedia(filePath);

            return (int)clip.duration;
        }


        public static void Play_Video(string filePath, int fps = 5)
        {
            string param = "\"" + filePath + "\"";
            Essential.Execute_FFMPEG("-i " + param + " -vf fps=" + fps + @" Frame\%d.jpg -hide_banner", true);

            //========================

            DateTime start_play = DateTime.Now;
            Audio.Get_Audio_And_Play(filePath);
            int durasi_video = Durasi_Video(filePath);

            DateTime play_now = DateTime.Now;
            int idx;

            while (Essential.TimeSpan_To_Seconds(play_now - start_play) < durasi_video)
            {
                play_now = DateTime.Now;

                idx = (int)Essential.TimeSpan_To_MiliSeconds(play_now - start_play) / (1000 / fps);
                idx++;



                try
                {
                    Image.Draw_Image(@"Frame\" + idx + ".jpg");
                }
                catch {
                    break;
                }

            }
            DirectoryInfo dir = new DirectoryInfo("Frame");
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }

        }


        public static void Play_My_Video_Format(string filePath)
        {

            List<Color> alist = new List<Color>();
            List<List<List<Color>>> frame = new List<List<List<Color>>>();


            //info file
            int panjang_asli;
            int lebar_asli;
            int Frame_Per_Seconds;
            TimeSpan durasi;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {

                //get audio
                long aul = reader.ReadInt64();

                using (BinaryWriter writer = new BinaryWriter(File.Open("audio.wav", FileMode.Create)))
                {
                    for (long x = 0; x < aul; x++)
                    {
                        writer.Write(reader.ReadByte());
                    }
                }


                //get frame

                panjang_asli = reader.ReadInt32();
                lebar_asli = reader.ReadInt32();
                Frame_Per_Seconds = reader.ReadInt32();
                durasi = TimeSpan.FromSeconds((double)reader.ReadInt32());

                for (int x = 0; x < Essential.TimeSpan_To_Seconds(durasi) * Frame_Per_Seconds; x++)
                {

                    for (int y = 0; y < lebar_asli; y++)
                    {
                        for (int z = 0; z < panjang_asli; z++)
                        {
                            alist.Add(Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte()));
                        }
                    }
                }

            }


            SoundPlayer audio = new SoundPlayer("audio.wav");
            audio.Play();
            List<List<Color>> lebar = new List<List<Color>>();
            List<Color> panjang = new List<Color>();
            foreach (var a in alist)
            {
                panjang.Add(a);
                if (panjang.Count == panjang_asli)
                {
                    lebar.Add(panjang);
                    panjang = new List<Color>();
                }
                if (lebar.Count == lebar_asli)
                {
                    frame.Add(lebar);
                    lebar = new List<List<Color>>();
                }
            }

            int idx_play = 0;
            DateTime start_play = DateTime.Now;
            DateTime play_now = DateTime.Now;
            while (play_now - start_play < durasi)
            {
                play_now = DateTime.Now;
                idx_play = (int)Essential.TimeSpan_To_MiliSeconds(play_now - start_play) / (1000 / Frame_Per_Seconds);

                for (int x = 0; x < Essential.lebar_layar; x++)
                {
                    for (int y = 0; y < Essential.panjang_layar; y++)
                    {
                        try
                        {
                            Console.Write("\x1b[48;2;{0};{1};{2}m  ", frame[idx_play][x * 50 / Essential.lebar_layar][y * 100 / Essential.panjang_layar].R, frame[idx_play][x * 50 / Essential.lebar_layar][y * 100 / Essential.panjang_layar].G, frame[idx_play][x * 50 / Essential.lebar_layar][y * 100 / Essential.panjang_layar].B);
                        }
                        catch
                        {
                        }
                    }
                    Console.WriteLine();
                }
                Console.SetCursorPosition(0, 0);
            }


        }


        public static void Convert_To_Tarsier(string filePath, string tujuan, int Frame_Per_Seconds, int lebar, int panjang)
        {
            //get all frame
            List<Color> frame = new List<Color>();

            int lebar_asli;
            int panjang_asli;

            int jlh_titik_kebawah = 0;
            int jlh_titik_kesamping = 0;
            TimeSpan waktu_saat_ini = new TimeSpan(0, 0, 0, 0, 0);
            TimeSpan penjumlahan = new TimeSpan(0, 0, 0, 0, 1000 / Frame_Per_Seconds);
            string now;

            string param = "\"" + filePath + "\"";
            Essential.Execute_FFMPEG("-i " + param + " -vf fps=" + Frame_Per_Seconds + @" Frame\%d.jpg -hide_banner", true);


            using (BinaryWriter writer = new BinaryWriter(File.Open(tujuan, FileMode.Create)))
            {

                //audio
                Audio.Get_Audio(filePath);
                FileInfo audio = new FileInfo("audio.wav");
                writer.Write(audio.Length);

                List<byte> alist = new List<byte>();


                using (BinaryReader reader = new BinaryReader(File.Open("audio.wav", FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        alist.Add(reader.ReadByte());
                    }
                }

                foreach (var a in alist)
                {
                    writer.Write(a);
                }

                //write frame and its data
                for (int x = 0; x < Durasi_Video(filePath) * Frame_Per_Seconds; x++)
                {
                    try
                    {
                        now = Convert.ToString(waktu_saat_ini);
                        now = now.Substring(0, 12);
                    }
                    catch
                    {
                        now = Convert.ToString(waktu_saat_ini);
                        now = String.Join("", now, ".000");
                    }

                    int idx = (int)Essential.TimeSpan_To_MiliSeconds(waktu_saat_ini) / (1000 / Frame_Per_Seconds);
                    idx++;
                    Bitmap gambar = new Bitmap(@"Frame\" + idx + ".jpg");
                    gambar.RotateFlip(RotateFlipType.Rotate90FlipX);

                    if (x == 0)
                    {
                        lebar_asli = gambar.Height;
                        panjang_asli = gambar.Width;
                        jlh_titik_kebawah = panjang_asli / lebar;
                        jlh_titik_kesamping = lebar_asli / panjang;

                        writer.Write(panjang);
                        writer.Write(lebar);
                        writer.Write(Frame_Per_Seconds);
                        writer.Write(Durasi_Video(filePath));
                    }

                    for (int y = 0; y < lebar; y++)
                    {
                        for (int z = 0; z < panjang; z++)
                        {


                            int j = (y * jlh_titik_kebawah);
                            int k = (z * jlh_titik_kesamping);

                            frame.Add(gambar.GetPixel(j, k));


                            //===================================================
                        }
                    }

                    waktu_saat_ini = waktu_saat_ini + penjumlahan;
                }

                foreach (var a in frame)
                {
                    writer.Write(a.R);
                    writer.Write(a.G);
                    writer.Write(a.B);
                }
            }


            //masukin semua frame dalam list
            //===========================================================

        }


    }
}
