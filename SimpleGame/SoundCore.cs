using System;
using System.Collections.Generic;
using SFML.Audio;

namespace SimpleGame
{
    public static class SoundCore
    {

        public const string CONTENT_PATH = @"Content\";
        public static SortedDictionary<string, Sound> Sounds = new SortedDictionary<string, Sound>();
        public static void Load()
        {
            Sounds.Add("Amb", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\World\\wsound_move.wav")));
            Sounds["Amb"].Volume = 66;
            Sounds.Add("Alarm", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\World\\wsound_warn.ogg")));
            Sounds.Add("Motion", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\World\\wsound_motion.ogg")));
            Sounds.Add("Glitch", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\World\\wsound_glitch_1.ogg")));

            Sounds.Add("TeleportUse", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_4.wav"))); 
            Sounds.Add("TeleportReload", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\MaxMana.wav")));
            Sounds.Add("PlayerFall", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Dig_2.wav")));
            Sounds.Add("PlayerKilled", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Player_Killed.wav")));
            Sounds.Add("PlayerJump", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Jump_1.wav")));

            Sounds.Add("BombLaunched", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_14.wav")));
            Sounds.Add("MagicShot", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Jump_1.wav")));
            Sounds.Add("NoClip", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_15.wav")));
            Sounds.Add("NoClip_End", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_29.wav")));
            Sounds.Add("Drink", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_3.wav")));
            Sounds.Add("Eat", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Feedback\\Item_2.wav")));



            Sounds.Add("Phone", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\phone.wav")));

            Sounds.Add("Star Fall", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\star_fall.wav")));
            Sounds.Add("Star Falling", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\star_falling.wav")));
            Sounds.Add("Pressed", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\clicked.wav")));
            Sounds.Add("Beep", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\beep.wav")));
            Sounds.Add("Warning", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\warning.wav")));

            Sounds.Add("Explode", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\explode.wav")));

            //Sounds.Add("", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\.wav")));
            Sounds.Add("Boss Attack", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\boss_attack.wav")));
            Sounds.Add("Yeeah", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\yeah.wav")));
            Sounds.Add("Dzink", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\dzink.wav")));
            Sounds.Add("Transformation", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\transform.wav")));
            Sounds.Add("Start Fight", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\fight.wav")));
            Sounds.Add("Object Flying", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\fly.wav")));
            Sounds.Add("PlayerHeal", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\heal.wav")));
            Sounds.Add("Attack", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\kick.wav")));
            Sounds.Add("Damage Taken", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\damage.wav")));
            Sounds.Add("Item Spawned", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\item_spawned.wav")));
            Sounds.Add("Item Used", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\item_use.wav")));
            Sounds.Add("Object Spawn", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\spawned.wav")));
            Sounds.Add("Menu Beep", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\menu_beep.wav")));
            Sounds.Add("Menu Select", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\menu_selected.wav")));

            Sounds.Add("Speaker0", new Sound(new SoundBuffer($"{CONTENT_PATH}\\speaker.wav")));
            Sounds.Add("Speaker2", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\speak_flowey.wav")));
            Sounds.Add("Speaker3", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\speak_angry.wav")));


        }

        public static void Play(string soundname)
        {
            if (Program.stopAll)
                return;
            Sounds[soundname].Play();
        }
        public static void Play(string subfoldier, string ext, string soundname,int minstack, int maxstack, int volume)
        {
            if (Program.stopAll)
                return;
            try
            {
                List<Sound> sounds = new List<Sound>(); // Tink
                for (int i = minstack; i < maxstack; i++)
                {
                    Sound snd = new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\{subfoldier}\\{soundname}{i}.{ext}"));
                    snd.Volume = volume;
                    sounds.Add(snd);
                }
                sounds[Program.Random.Next(0, sounds.Count)].Play();
            }
            catch(Exception e)
            {
                Program.speakerEnabed = false;
                Program.speakerSay(60, Voice.Default, e.Message);
            }
        }

    }
}
