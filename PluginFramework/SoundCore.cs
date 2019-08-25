using System;
using System.Collections.Generic;
using SFML.Audio;

namespace SimpleGame
{
    public static partial  class SoundCore
    {

        public const string CONTENT_PATH = @"..\Data";
        public static SortedDictionary<string, Sound> Sounds = new SortedDictionary<string, Sound>();
        public static Random rnd = new Random();
        public static void Load()
        {

           
            Sounds.Add("Glitch", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\hum2.wav")));
            Sounds.Add("Destroy", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\destroy.wav")));
            Sounds.Add("Error", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\glitch.wav")));
            Sounds.Add("Wrong", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\wrong.wav")));

            Sounds.Add("Shop Buy", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shop_buy.wav")));
            Sounds.Add("Shop Already", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shop_already.wav")));
            Sounds.Add("Shop NotEnough", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shop_notenough.wav")));
            Sounds.Add("Shop Open", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shop_open.wav")));
            Sounds.Add("Shop Close", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shop_close.wav")));

            Sounds["Glitch"].Loop = true;
            Sounds["Glitch"].Volume = 50;

            Sounds.Add("Coin", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\coin.wav")));
            Sounds["Glitch"].Volume = 50;



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
            Sounds.Add("Whee", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\whee.wav")));

            Sounds.Add("On", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\bootup.wav")));
            Sounds.Add("Off", new Sound(new SoundBuffer($"{CONTENT_PATH}\\sounds\\Main\\shutdown.wav")));

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

            Sounds.Add("Speaker2", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Under\\speak_flowey.wav")));
            Sounds.Add("Speaker3", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Main\\azazel.wav")));
            Sounds.Add("Speaker4", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Main\\buer.wav")));
            Sounds.Add("Speaker7", new Sound(new SoundBuffer($"{CONTENT_PATH}\\Sounds\\Main\\asmodeus.wav")));

            Sounds["Speaker3"].Volume = 50;
        }

        public static void Play(string soundname)
        {
            if(soundname == "Speaker7") {
                switch(rnd.Next(0,3)) {
                    case 0:
                        Sounds["Speaker7"].Pitch = 0.9f;
                        break;
                    case 1:
                        Sounds["Speaker7"].Pitch = 1f;
                        break;
                    case 2:
                        Sounds["Speaker7"].Pitch = 1f;
                        break;
                }
            }
            Sounds[soundname].Play();
        }
      

    }
}
