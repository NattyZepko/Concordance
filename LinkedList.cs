using System.Text;

namespace Concordance
{
    // A linked list implementation for line number referencing
    class LinkedList
    {
        // Inner Node Class implementation
        private class Node
        {
            public uint item;
            public Node next;

            public Node(uint item) // O(1)
            {
                this.item = item;
                next = null;
            }
        }

        // Fields
        private Node first, last;

        // Constructors
        public LinkedList() // O(1)
        {
            first = last = null;
        }

        // Methods
        public void Insert(uint item) // O(1)
        {
            if (first == null)
                first = last = new Node(item);

            else
            {
                last.next = new Node(item);
                last = last.next;
            }
        }

        public override string ToString() // O(n) [as "n" means the number of Nodes]
        {
            StringBuilder sb = new StringBuilder();
            Node temp = first;
            while (temp != null)
            {
                sb.Append(temp.item);
                uint count = 1;
                uint currentItem = temp.item;
                temp = temp.next;
                while (temp != null && temp.item == currentItem) // inner loop shortents the length of outer loop, thus it remains O(n).
                {
                    count++;
                    temp = temp.next;
                }

                if (count > 1)
                    sb.Append(string.Format(" ({0} times)", count));

                sb.Append(", ");
            }

            sb.Remove(sb.Length - 3, 2);

            return sb.ToString();
        }
    }
}