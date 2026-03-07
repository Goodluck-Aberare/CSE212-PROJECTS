public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        //  MyPLAN:
        // 1. Create a new array of doubles with size equal to 'length'.
        // 2. Use a loop that runs from 0 up to length-1.
        // 3. For each index i, calculate (i+1) * number.
        //    - Example: if number=3 and i=0 → (0+1)*3 = 3
        //               if i=1 → (1+1)*3 = 6
        // 4. Store the result in the array at position i.
        // 5. After the loop finishes, return the array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (i + 1) * number;
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// For example, if the data is List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and amount is 3  
    /// then the list after the function runs should be List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // MyPLAN:
        // 1. Understand rotation: rotating right by 'amount' means the last 'amount' elements move to the front.
        //    Example: {1,2,3,4,5} rotated right by 2 → {4,5,1,2,3}.
        // 2. Use List slicing with GetRange:
        //    - Get the last 'amount' elements: data.GetRange(data.Count - amount, amount).
        //    - Get the first part: data.GetRange(0, data.Count - amount).
        // 3. Clear the original list (or overwrite it).
        // 4. Add the last part first, then add the first part.
        // 5. The list is now rotated in place.

        List<int> lastPart = data.GetRange(data.Count - amount, amount);
        List<int> firstPart = data.GetRange(0, data.Count - amount);

        data.Clear();
        data.AddRange(lastPart);
        data.AddRange(firstPart);
    }
}
