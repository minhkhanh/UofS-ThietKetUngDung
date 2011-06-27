using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Data;
using Microsoft.Xna.Framework.Media;

namespace PQ
{
    public static class MusicManager
    {
        static Dictionary<string, Song> _listSong;
        public static void LoadSoundResource(ContentManager content)
        {
            _listSong = new Dictionary<string, Song>();
            DataSet vdDataSet = new DataSet();
            string database = @"Content\Musics\MusicLists.xml";
            vdDataSet.ReadXml(database);
            DataRow[] drs = vdDataSet.Tables["Music"].Select();
            foreach (DataRow dr in drs)
            {
                string str = dr["name"].ToString();
                Song song = content.Load<Song>(@"Musics\" + str);
                _listSong.Add(str, song);
            }
            MediaPlayer.Volume = 1f;
        }
        public static void Stop()
        {
            MediaPlayer.Stop();
        }
        public static void Play(string name)
        {
            if (_listSong.ContainsKey(name))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_listSong[name]);
            }
        }
        //static float fVolume = 1f;
        public static float Volume
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value; }
        }
    }
}
