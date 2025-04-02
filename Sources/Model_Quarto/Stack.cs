using System.Collections.ObjectModel;

namespace Model
{
    public class Stack
    {
        private ObservableCollection<Piece> stack { get; set; }

        public Stack(ObservableCollection<Piece> stack)
        {
            this.stack = stack;
        }

        public bool IsEmpty() {  return stack.Count == 0; }

    }
}
