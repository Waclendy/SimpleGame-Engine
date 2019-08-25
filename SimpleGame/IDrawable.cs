using System.Collections.Generic;

namespace SimpleGame {
    public interface IDrawable {
        void Draw();
        void Update();
    }
    public class Drawing<T> where T : IDrawable {
        List<T> objects = new List<T>();
        public void Add(T obj) {
            objects.Add(obj);
        }
        public void Remove(T obj) {
            objects.Remove(obj);
        }
        public void Draw() {
            foreach (T obj in objects)
                obj.Draw();
        }
        public void Update() {
            foreach (T obj in objects)
                obj.Update();
        }
    }
}
