using System;
using System.Text;

namespace Concordance
{
    // A BST implementation to serve as the main data structure that builds the concordance 
    class BinarySearchTree
    {
        // Inner Node class
        private class BinarySearchTreeNode
        {
            public String item;
            public LinkedList lineRef;
            public BinarySearchTreeNode left, right;

            public BinarySearchTreeNode(String item) // O(1)
            {
                this.item = item;
                left = right = null;
                lineRef = new LinkedList();
            }

            // returns a string representation of the current node only
            public override string ToString() // O(n) [as "n" means the number of nodes] 
            {
                return "'" + item + "' appears in lines: " + lineRef.ToString();
            }
        }

        // Fields
        private BinarySearchTreeNode root;
        private uint totalWordCount, uniqueWordCount; // for statistical purposes

        //Cosntructors
        public BinarySearchTree() // O(1)
        {
            root = null;
            totalWordCount = uniqueWordCount = 0;
        }

        //Methods
        private void Insert(BinarySearchTreeNode root, String item, uint line) // Average/Best case: O(lg(n)), Worst case: O(n) [as "n" means the number of nodes in the tree]
        {
            if (item.CompareTo(root.item) < 0) // item comes alphabetically BEFORE root.item
            {
                if (root.left == null)
                {
                    root.left = new BinarySearchTreeNode(item);
                    root.left.lineRef.Insert(line);
                    uniqueWordCount++;
                }
                else
                    Insert(root.left, item, line); // Recursion.
            }

            else if (item.CompareTo(root.item) > 0) // item comes alphabetically AFTER root.item
            {
                if (root.right == null)
                {
                    root.right = new BinarySearchTreeNode(item);
                    root.right.lineRef.Insert(line);
                    uniqueWordCount++;
                }
                else
                    Insert(root.right, item, line); // Recursion.
            }

            else // item EQUALS root.item           
                root.lineRef.Insert(line);
        }

        public void Insert(String item, uint line) // This function either initializes the tree (O(1)) or activates the above (recursive) version of Insert ( O(lg(n))/O(n) )
        {
            //initialize the root if it is empty
            if (root == null)
            {
                root = new BinarySearchTreeNode(item.ToLower());
                root.lineRef.Insert(line);
                uniqueWordCount++;
            }

            else
                Insert(root, item.ToLower(), line); // toLower makes sure we don't count words twice.

            totalWordCount++;
        }

        // recursively appends the tree's entire string representation
        private void TraverseInOrder(StringBuilder sb, BinarySearchTreeNode root) // O(n*m) as "n" means the number of nodes in the tree (words) and "m" means the number of nodes in the LinkedList for that word
        {
            if (root != null)
            {
                TraverseInOrder(sb, root.left);
                sb.Append(root.ToString() + "\n");
                TraverseInOrder(sb, root.right);
            }
        }

        // returns the tree's string represatation along a quick summary of the word counts
        public override string ToString() // O(n) as "n" means the number of nodes in the tree
        {
            StringBuilder sb = new StringBuilder();
            TraverseInOrder(sb, root); // O(n)
            sb.Append("\n* * * * *\n\nTotal word count: " + totalWordCount + "\nUnique word count: " + uniqueWordCount); // O(1)

            return sb.ToString();
        }
    }
}