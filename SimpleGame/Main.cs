using System;
using System.Text;

using static SimpleGame.Misc;
using static SimpleGame.GAME;
using static SimpleGame.Tile;
using static SimpleGame.Program;
using System.Collections;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace SimpleGame {
    public enum CallType : int {
        CL_AddNewTileType,
        CL_GetTileType,
        CL_GetSystemTileTypeName,
        CL_PlaceTileAsSystemName,
        CL_InitChunkCopy,
        CL_UpdateWorldDraw,
        CL_SetCursorPosition,
        CL_WindowsMessageBox,
        CL_ByteString,
        CL_StringByte,
        CL_TileStructPtr,
        CL_TSPtrToTileStruct,
        CL_GameError,
    };
    static internal partial class GAME {
        public const bool strue = true;
        public const bool sfalse = false;
        public class schar : IEnumerable, IEnumerator {
            public schar() { }
           
            public schar(char[] str) { _chars = str; }
            public schar(string str) { _chars = str.ToCharArray(); }
            private char[] _chars;
            public int Length => _chars.Length;
            public char this[int index] { get { return _chars[index]; } set { _chars[index] = value; } }

            private int position = -1;
            public object Current
            {
                get
                {
                    if (position == -1 || position >= Length)
                        throw new InvalidOperationException();
                    return this[position];
                }
            }
            public IEnumerator GetEnumerator() {
                return _chars.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() {
                return _chars.GetEnumerator();
            }
            public bool MoveNext() {
                if (position < Length - 1) {
                    position++;
                    return true;
                } else
                    return false;
            }
            public void Reset() {
                position = -1;
            }
            public override string ToString() {
                return new string(_chars);
            }

            public static implicit operator char[](schar targ) {
                return targ._chars;
            }

            public static implicit operator string (schar targ) {
                return new string(targ._chars);
            }
        }
        public class sboolean {
            private int _toggle;

            public sboolean() { }
            public sboolean(int bl) { _toggle = bl; }
            public sboolean(bool bl) { _toggle = bl ? 1 : 0; }
            public sboolean(sboolean bl) { _toggle = bl._toggle; }
            public static implicit operator bool(sboolean targ) {
                return (targ._toggle == 1);
            }
            public static implicit operator int(sboolean targ) {
                return targ._toggle;
            }
            public static implicit operator sboolean(bool targ) {
                return new sboolean(targ);
            }
            public static implicit operator sboolean(int targ) {
                return new sboolean(targ);
            }
        }
        public class stimer {
            private int _sec = 30;
            private int _min = 0;
            private int _hrs = 0;
            private sboolean _enabled;
            private int _state = -1;
            public stimer(int state, int Hours = 0, int Min = 0, int Sec = 30) {
                _sec = Sec;
                _min = Min;
                _hrs = Hours;
                _enabled = strue;
                _state = state;
                Thread _thread = new Thread(_actioning);
                _thread.Start();
            }
            public void Pause() {
                if (_state != 1)
                _state = 2;
            }

            public void Stop() {
                _state = -1;
                _sec = 0;
                _min = 0;
                _hrs = 0;
                _enabled = 0;
            }
            public void Start() {
                _state = 1;
            }
            private void _actioning() {
                while(_enabled) {
                    if (_state == -1)
                        return;
                    if (_state == 2)
                        continue;
                    if (Program.Pause)
                        continue;

                    _update();
                    Thread.Sleep(1000);
                }
            }
            public int Seconds { get; private set; }
            public int Minutes { get; private set; }
            public int Hours { get; private set; }
            public string Lable { get {
                    string result = "";
                    string prev = "";
                    if (Hours > 0) {

                        result += Hours + prev + ":";
                    }
                    if (Minutes > 0) {
                        result += Minutes + prev + ":";
                    }
                    if (Seconds >= 0) {
                        if (Seconds < 10)
                            prev = "0";
                        result += prev + Seconds;
                    }
                    if (!_enabled)
                        return "DISABLED";
                    return result; } }

            public event Action TimeEnd;
            private void _update() {
                if (_enabled) {


                    if (_sec < 0) {
                        if (_min > 0) {


                            _sec = 59;
                            _min--;
                        }
                    }
                


                    if (_min < 0) {
                        if (_hrs > 0)
                            _min = 59;
                        _hrs--;
                        }
                    }

                    if (_hrs < 0)
                        _hrs = 0;

                Seconds = _sec;
                Minutes = _min;
                Hours = _hrs;

                if (_sec <= 0 && _min <= 0 && _hrs <= 0) {
                    Stop();
                    TimeEnd?.Invoke();
                    return;
                }
                _sec--;
            }

            }
        public class srender {
            List<IRenderable> targets = new List<IRenderable>();
            private int _slp = 0;
            private sboolean _enabled;
            public int Sleep { get { return _slp; } set { if (value < 0) value = 0; _slp = value; }  }
            public void Add(IRenderable render) {
                if (!render.Rendered)
                    targets.Add(render);
            }
            public void Disable() {
                _enabled = 0;
            }
            public void Enable() {
                _enabled = 1;
                Thread thrd = new Thread(_render);
                thrd.Start();
            }
            private void _render() {
                while (_enabled) {
                    for (int i = 0; i < targets.Count; i++) {
                        targets[i].Update();
                        if (targets[i].Rendered == 1)
                            targets.Remove(targets[i]);
                    }
                    Thread.Sleep(_slp);
                }
            }
        }
        public static unsafe object game(int calltype, params object[] args) {
      
            try {
                switch (calltype) {
                    #region Добаввление новой плитки в игру     *для модов           > return void
                    case (int)CallType.CL_AddNewTileType:
                        TileStruct[] _localtiles = Tiles;

                        for (int i = 0; i < _localtiles.Length; i++) {
                            if (_localtiles[i].TileName == ((TileStruct)args[0]).TileName)
                                return 0;
                        }

                        Tiles = new TileStruct[_localtiles.Length + 1];
                        Tiles[0] = ((TileStruct)args[0]);
                        for (int i = 1; i < Tiles.Length; i++) {
                            Tiles[i] = _localtiles[i - 1];
                        }

                        break;
                    #endregion

                    #region Получить структуру плитки по Id плитки                   > return TileStruct
                    case (int)CallType.CL_GetTileType:
                        for (int i = 0; i < Tiles.Length; i++) {
                            if (Tiles[i].Id == args[0].ToString()) {
                                return Tiles[i];
                            }
                        }
                        return new TileStruct() {
                            Id = "-1"
                        };
                    #endregion

                    #region Получить полное название плитки     *world_wall и т.п.   > return string
                    case (int)CallType.CL_GetSystemTileTypeName:
                        if (((TileStruct)args[0]).TileName == null)
                            return "error";
                        return ((TileStruct)args[0]).TileName;
                    #endregion

                    #region Поставить плитку используя ее полное имя                 > return boolean
                    case (int)CallType.CL_PlaceTileAsSystemName:
                        string ID = "";
                        sboolean Twice = false;
                        sboolean Single = false;
                        for (int i = 0; i < 0; i++) {
                            if (Tiles[i].TileName == args[0].ToString()) {
                                if (Tiles[i].Id != ID && Single && Tiles[i].TileName == args[0].ToString())
                                    Twice = true;
                                if (Tiles[i].Id == null)
                                    continue;
                                ID = Tiles[i].Id;
                                Single = true;
                                continue;
                            }
                        }
                        if (Twice)
                            throw new Exception($"Error: Two or more not mirrored tiles have a {args[0].ToString()} name!");
                        if (ID != "") {
                            Program.World.setTile(((ivector)args[1]).X, ((ivector)args[1]).Y, ID);
                            return true;
                        }
                        return false;
                    #endregion

                    #region Обновить копию чанков                                    > return void
                    case (int)CallType.CL_InitChunkCopy:
                        Program.World.chunks_copy = new Tile[World.CHUNK_X][];
                        for (int chunk = 0; chunk < World.CHUNK_X; chunk++) {
                            Program.World.chunks_copy[chunk] = new Tile[World.CHUNK_Y];
                        }
                        break;
                    #endregion

                    #region Стереть все нарисованные обЪекты в мире кроме фона       > return void
                    case (int)CallType.CL_UpdateWorldDraw:
                        for (int x = 0; x < World.CHUNK_X; x++) {
                            for (int y = 0; y < World.CHUNK_Y; y++) {
                                string tileid = Program.World.getTile(x, y);
                                if (tileid == NoneId || tileid == BackgroundId)
                                    continue;

                                game(6, new ivector(x, y));
                                Console.Write(" ");
                            }
                        }
                        break;
                    #endregion

                    #region Задать положение курсора в консоли                       > return void
                    case (int)CallType.CL_SetCursorPosition:
                        Console.SetCursorPosition(((ivector)args[0]).X, ((ivector)args[0]).Y);
                        break;
                    #endregion

                    #region Отправить диалоговое окно                                > return void
                    case (int)CallType.CL_WindowsMessageBox:
                        System.Windows.Forms.MessageBox.Show(args[0].ToString());
                        break;
                    #endregion

                    #region Конвертация string в byte[] и наоборот                   >return byte[] & string
                    case (int)CallType.CL_ByteString:

                        byte[] bytes = Encoding.UTF8.GetBytes(args[0].ToString());
                        return bytes;
                    case (int)CallType.CL_StringByte:

                        string str = Encoding.UTF8.GetString((byte[])args[0]);
                        return str;
                    #endregion

                    #region БРЕД                                *скучно было          >return TileStruct & TSP
                    case (int)CallType.CL_TileStructPtr:
                        TSP prep;
                        TileStruct tile = (TileStruct)args[0];
                        prep.BackColor = (int)tile.BackColor;
                        prep.ForeColor = (int)tile.ForeColor;
                        prep.CollusionEnabled = tile.CollusionEnabled ? 1 : 0;
                        prep.IsVisible = tile.IsVisible ? 1 : 0;
                        prep.Triggering = tile.Triggering ? 1 : 0;

                        prep.Id = BitConverter.ToInt32((byte[])game(8, tile.Id), 0);
                        prep.Char = BitConverter.ToInt32((byte[])game(8, tile.Char), 0);
                        prep.TileName = BitConverter.ToInt32((byte[])game(8, tile.TileName), 0);

                        return prep;

                    case (int)CallType.CL_TSPtrToTileStruct:
                        TSP cvrt = (TSP)args[0];

                        TileStruct returns = (TileStruct)args[0];
                        returns.Trigger = null;
                        returns.BackColor = (ConsoleColor)cvrt.BackColor;
                        returns.ForeColor = (ConsoleColor)cvrt.ForeColor;
                        returns.CollusionEnabled = cvrt.CollusionEnabled == 1 ? true : false;
                        returns.CollusionEnabled = cvrt.IsVisible == 1 ? true : false;
                        returns.Triggering = false;

                        returns.Id = (string)game(9, BitConverter.GetBytes(cvrt.Id));
                        returns.Char = (string)game(9, BitConverter.GetBytes(cvrt.Char));
                        returns.TileName = (string)game(9, BitConverter.GetBytes(cvrt.TileName));
                        return returns;
                        #endregion

                        case (int)CallType.CL_GameError:
                        break;
                    default:
                        game((int)CallType.CL_GameError, "UNKNOWN CALL ID - " + calltype);
                        break;
                }
                
                return 0;
            } catch(ArgumentException e) {
                game((int)CallType.CL_GameError, "METHOD CALL - ONE OF ARGS WAS EMPTY");
                return 1;
            }
            catch {
                game((int) CallType.CL_GameError, "METHOD CALL - UNKNOWN ERROR");
                return 1;
            }
}

        public static unsafe string ToString(char*[] ptr) {
            return ptr.ToString();
        }

        public static unsafe void GAME_AddNewTileType(TileStruct @struct) => game((int)CallType.CL_AddNewTileType, @struct);
        public static unsafe TileStruct GAME_GetTileType(string Id) => (TileStruct)game((int)CallType.CL_GetTileType, Id);
        public static unsafe string GAME_GetSystemTileTypeName(TileStruct @struct) => (string)game((int)CallType.CL_GetSystemTileTypeName, @struct);
        public static unsafe bool GAME_PlaceTileAsSystemName(string sysName, ivector pos) => (bool)game((int)CallType.CL_PlaceTileAsSystemName, sysName, pos);
        public static unsafe void GAME_InitChunkCopy() => game((int)CallType.CL_InitChunkCopy);
        public static unsafe void GAME_CursorPosition(ivector pos) => game((int)CallType.CL_SetCursorPosition, pos);
        public static unsafe void GAME_UpdateWorldDraw() => game((int)CallType.CL_UpdateWorldDraw);
        public static unsafe int rand(int min, int max) => Program.Random.Next(min, Program.Random.Next(min + 1, max + 1));

    }
}
