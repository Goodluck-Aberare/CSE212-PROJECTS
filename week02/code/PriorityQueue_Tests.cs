using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None (requirement check)
    public void TestPriorityQueue_EmptyDequeue()
    {
        var priorityQueue = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue items with different priorities, highest priority should be dequeued
    // Expected Result: Item with highest priority returned
    // Defect(s) Found: Original code did not remove item from queue
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
        Assert.AreEqual("[Low (Pri:1), Medium (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Multiple items with same highest priority
    // Expected Result: First item with that priority dequeued (FIFO)
    // Defect(s) Found: Original loop skipped last element and did not respect FIFO
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstHigh", 5);
        priorityQueue.Enqueue("SecondHigh", 5);
        priorityQueue.Enqueue("Low", 1);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("FirstHigh", result);
        Assert.AreEqual("[SecondHigh (Pri:5), Low (Pri:1)]", priorityQueue.ToString());
    }
}