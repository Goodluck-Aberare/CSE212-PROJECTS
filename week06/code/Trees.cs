public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.  
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Insert the middle element of the current range into the BST, then recurse left and right.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Base case: if the range is invalid, stop
        if (first > last)
        {
            return;
        }

        // Find the middle index
        int mid = (first + last) / 2;

        // Insert the middle value into the BST
        bst.Insert(sortedNumbers[mid]);

        // Recursively insert the middle of the left half
        InsertMiddle(sortedNumbers, first, mid - 1, bst);

        // Recursively insert the middle of the right half
        InsertMiddle(sortedNumbers, mid + 1, last, bst);
    }
}