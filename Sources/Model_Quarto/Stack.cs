using System.Collections.ObjectModel;

namespace Model
{
    public class Stack
    {
        private ObservableCollection<int> stack { get; }

        public Stack(ObservableCollection<int> stack) {  this.stack = stack; }

        public bool IsEmpty() {  return stack.Count == 0; }

        

    }
}
