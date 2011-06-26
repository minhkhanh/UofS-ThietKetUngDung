using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Data;

namespace PQ
{
    public class SoundManager
    {
        Dictionary<string, SoundEffect> _listSoundEffect;
        public void LoadSoundResource(ContentManager content)
        {
            _listSoundEffect = new Dictionary<string, SoundEffect>();
            DataSet vdDataSet = new DataSet();
            string database = @"Content\Sounds\SoundLists.xml";
            vdDataSet.ReadXml(database);
            DataRow[] drs = vdDataSet.Tables["Sound"].Select();
            foreach (DataRow dr in drs)
            {
                string str = dr["name"].ToString();
                SoundEffect sound = content.Load<SoundEffect>(@"Sounds\"+str);
                _listSoundEffect.Add(str, sound);
            }
        }
        public void Play(string name)
        {
            if (_listSoundEffect.ContainsKey(name))
            {
                _listSoundEffect[name].Play(fVolume, 0, 0);
            }
        }
        float fVolume = 1f;
        public float Volume
        {
            get { return fVolume; }
            set { fVolume = value; }
        }
    }
}
